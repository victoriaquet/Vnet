using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using SNC2017.Helpers;
using SNC2017.Models;
using SNC2017.ViewModels;

namespace SNC2017.Controllers
{

    public class CasosController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: Casos
        [Authorize(Roles = "Administrador,Distribuidor")]
        public ActionResult Index()
        {
            //var casos = db.Casos.Include(c => c.AreaCaso).Include(c => c.EstadoCaso).Include(c => c.SeguimientoCaso).Include(c => c.Suscriptor).Include(c => c.TipoCaso);
            var listaCasos = new List<Caso>();
            if (User.IsInRole("Distribuidor"))
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                listaCasos = db.Casos.ToList().FindAll(x => x.AsuntoCaso.AreaCaso.Descripcion.ToLower().Trim() == "Distribución".ToLower().Trim() && x.Suscripcion.Canillita != null && x.Suscripcion.Canillita.IdDistribuidor == Int32.Parse(userManager.FindById(User.Identity.GetUserId()).Identificador));
            }
            else
            {
                listaCasos = db.Casos.ToList();
            }

            foreach (var caso in listaCasos)
            {
                if (caso.EstadoCaso.Color.Contains('#'))
                {
                    caso.EstadoCaso.Color = GenerateRgba.GenerateRgbaColor(caso.EstadoCaso.Color);
                }

            }
            return View(listaCasos);
        }

        public ActionResult ConfiguracionCasos()
        {
            return View();
        }

        // GET: Casos/Details/5
        [Authorize(Roles = "Administrador,Distribuidor")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caso caso = db.Casos.Find(id);
            if (caso == null)
            {
                return HttpNotFound();
            }

            try
            {
                ViewBag.areadelasunto = db.AreaCasos.Find(caso.AsuntoCaso.IdAreaCaso).Descripcion;
            }
            catch (Exception e)
            {
                ViewBag.areadelasunto = "No se pudo obtener el área";
            }

            return View(caso);
        }

        // GET: Casos/Create
        public ActionResult Create()
        {
            //var suscs = db.Suscripciones.ToList()
            //    .Select(susc => new SelectListItem()
            //    {
            //        Value = susc.IdSuscripcion.ToString(),
            //        Text = susc.Suscriptor.Nombre+"|"+ susc.Suscriptor.Apellido+ "|" +
            //               susc.Suscriptor.DNI + "|"+ susc.IdSuscripcion
            //    });

            ViewBag.Suscripciones = new List<Suscripcion>();
            ViewBag.IdAreaCaso = db.AreaCasos.ToList();
            ViewBag.IdEstadoCaso = db.EstadoCasos.ToList();
            ViewBag.IdTipoCaso = db.TipoCasos.ToList();
            return View();
        }

        /// <summary>
        /// Recupera las suscripciones que no están en los estados 4 y 5 (baja y rechazada)
        /// </summary>
        /// <returns></returns>
        public string GetSuscripcionCaso()
        {

            
            string constring = ConfigurationManager.ConnectionStrings["SNCContext"].ToString();
            SqlConnection objConn = new SqlConnection(constring);
            objConn.Open();


            SqlDataAdapter daSusc
                = new SqlDataAdapter("SELECT s.IdSuscripcion,s.NumeroSuscripcion,s1.DNI,s1.Nombre, s1.Apellido, s1.NumeroSuscriptor FROM Suscripcions s INNER JOIN Suscriptors s1 ON s.IdSuscriptor = s1.IdSuscriptor inner join EstadoSuscripcionHistorials e ON e.IdSuscripcion = s.IdSuscripcion where e.EstadoSuscripcion <> 4 and e.EstadoSuscripcion <> 5", objConn);

            DataSet dsSusc = new DataSet("Suscripciones");
            daSusc.FillSchema(dsSusc, SchemaType.Source, "dbo.Susc");
            daSusc.Fill(dsSusc, "dbo.Susc");

            DataTable tblSuscNo4Ni5 = dsSusc.Tables["dbo.Susc"]; //no es estado 4 (baja) ni 5 (rechazada)
            List<dynamic> nld = new List<dynamic>();
            foreach (DataRow tlr in tblSuscNo4Ni5.Rows)
            {
                dynamic elem = new ExpandoObject();
                elem.IdSuscripcion = tlr["IdSuscripcion"].ToString();
                elem.NroSusc = tlr["IdSuscripcion"].ToString();
                elem.DNI = tlr["DNI"].ToString();
                elem.Nombre = tlr["Nombre"].ToString() +" "+ tlr["Apellido"];
               
                nld.Add(elem);
           

            }
            objConn.Close();

            return JsonConvert.SerializeObject(nld);
           
        }


        //JavaScriptSerializer j = new JavaScriptSerializer();
        //var data = afiliados.Select(x => new { NroAfiliado = x.NroAfiliado, Nombres = x.Nombres, IdOS = idObraSocial });


        //    return JsonConvert.SerializeObject(data);


        // POST: Casos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Caso casoVM, string[] lineaDiasFecha, string[] lineaDiasObs)
        {

            Caso caso = new Caso();
            caso.DiasDevoluciones = new List<DiaDevolucionCaso>();

            caso.IdAsuntoCaso = casoVM.IdAsuntoCaso;
            caso.IdEstadoCaso = db.EstadoCasos.ToList().Find(e => e.Descripcion == "Abierto").IdEstadoCaso;
            caso.IdSuscripcion = casoVM.IdSuscripcion;
            caso.IdTipoCaso = casoVM.IdTipoCaso;
            caso.Descripcion = casoVM.Descripcion;
            caso.FechaCarga = DateTime.Now;
            //caso.HistorialCaso = casoVM.FechaCarga;

            caso.Observacion = casoVM.Observacion;
            //caso.FechaCierre = casoVM.FechaCierre; no lleva fecha cierre ya que el estado por defecto es abierto.
            caso.FechaVencimiento = casoVM.FechaVencimiento;
            caso.UsuarioCarga = User.Identity.Name;


            if (lineaDiasFecha != null)
            {
                foreach (var item in lineaDiasFecha)
                {
                    DiaDevolucionCaso devolucionCaso = new DiaDevolucionCaso();
                    devolucionCaso.Fecha = Convert.ToDateTime(item);
                    devolucionCaso.FechaModificacion = DateTime.Now;
                    devolucionCaso.Observaciones = lineaDiasObs[lineaDiasFecha.ToList().IndexOf(item)];
                    devolucionCaso.UsuarioModificacion = User.Identity.Name;
                    devolucionCaso.Caso = caso;
                    db.DiaDevolucionCasos.Add(devolucionCaso);
                    caso.DiasDevoluciones.Add(devolucionCaso);
                }
            }

            db.Casos.Add(caso);
            db.SaveChanges();
            return RedirectToAction("Index");

        }


        // GET: Casos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caso caso = db.Casos.Find(id);
            if (caso == null)
            {
                return HttpNotFound();
            }

            if (caso.IdEstadoCaso == 2) // si el caso está cerrado no puede editarse
            {

                TempData["msg"] = "No se puede editar un caso cerrado.";
                return RedirectToAction("Index", "Casos");

            }

            if (caso.UsuarioCarga != User.Identity.Name)
            {

                TempData["msg"] = "No tiene permisos para realizar " +
                                  "esta acción por no ser el creador " +
                                  "del caso.";
                return RedirectToAction("Index", "Casos");
            }

            if (caso.EstadoCaso.Color.Contains('#'))
            {
                caso.EstadoCaso.Color = GenerateRgba.GenerateRgbaColor(caso.EstadoCaso.Color);
            }

            ViewBag.Suscripciones = db.Suscripciones.ToList();
            ViewBag.IdAreaCaso = db.AreaCasos.ToList();
            ViewBag.IdEstadoCaso = db.EstadoCasos.ToList();
            ViewBag.IdTipoCaso = db.TipoCasos.ToList();
            ViewBag.MotivosCierreCaso = db.MotivoCierreCasos.ToList();

            return View(caso);
        }

        // POST: Casos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Caso caso, string[] lineaDiasFecha, string[] lineaDiasObs, string DescripcionHistorial)
        {

            Caso casoOriginal = db.Casos.Find(caso.IdCaso);
            if (casoOriginal != null)
            {
                //CONTROLAR QUE TOODOS LOS DATOS ESTEN BIEN Y MAPEAR CASO EN CASOORIGINAL!!

                #region //LOGTXT
                //En observaciones historial (comentarios de la modificación)
                //adjunto todos los datos anteriores al cambio
                var diasimplicados = "";
                if (casoOriginal.DiasDevoluciones.Any())
                {
                    foreach (var dimp in casoOriginal.DiasDevoluciones)
                    {
                        diasimplicados = diasimplicados + dimp.Fecha + " " + dimp.Observaciones + " ";
                    }

                }
                var datosAnteriores = "Detalles previos a la edición del " + DateTime.Now.ToString() + " : ";
                try
                {
                    datosAnteriores = datosAnteriores + "<<descripción>>" + casoOriginal.Descripcion;
                }
                catch (Exception e)
                {

                }
                try
                {
                    datosAnteriores = datosAnteriores + "<<observaciones>>" + casoOriginal.Observacion;
                }
                catch (Exception e)
                {

                }
                try
                {
                    datosAnteriores = datosAnteriores + "<<motivo cierre>>" + casoOriginal.MotivoCierreCaso.Descripcion;
                }
                catch (Exception e)
                {

                }
                try
                {
                    datosAnteriores = datosAnteriores + "<<asunto>>" + casoOriginal.AsuntoCaso.Descripcion;
                }
                catch (Exception e)
                {

                }
                try
                {
                    datosAnteriores = datosAnteriores + "<<estado>>" + casoOriginal.EstadoCaso.Descripcion;
                }
                catch (Exception e)
                {

                }
                try
                {
                    datosAnteriores = datosAnteriores + "<<Motivo cierre>>" + casoOriginal.MotivoCierreCaso.Descripcion;
                }
                catch (Exception e)
                {

                }
                try
                {
                    datosAnteriores = datosAnteriores + "<<tipo caso>>" + casoOriginal.TipoCaso.Descripcion;
                }
                catch (Exception e)
                {

                }
                try
                {
                    datosAnteriores = datosAnteriores + "<<fecha vencimiento>>" + casoOriginal.FechaVencimiento;
                }
                catch (Exception e)
                {

                }
                try
                {
                    datosAnteriores = datosAnteriores + "<<días implicados>>" + diasimplicados;
                }
                catch (Exception e)
                {

                }
                #endregion //





                casoOriginal.Descripcion = caso.Descripcion;
                HistorialCaso historialCaso = new HistorialCaso
                {
                    Fecha = DateTime.Now,
                    EstadoCasoHistorial = db.EstadoCasos.Find(casoOriginal.IdEstadoCaso),
                    UsuarioModificacion = User.Identity.Name,
                    ObservacionesHistorial = DescripcionHistorial,
                    FechaCarga = DateTime.Now,
                    LogTxt = datosAnteriores
                };
                db.HistorialCasos.Add(historialCaso);

                casoOriginal.IdTipoCaso = caso.IdTipoCaso;
                casoOriginal.IdEstadoCaso = caso.IdEstadoCaso;
                casoOriginal.IdAsuntoCaso = caso.IdAsuntoCaso;

                if (caso.FechaVencimiento != casoOriginal.FechaVencimiento)
                {
                    casoOriginal.FechaVencimiento = caso.FechaVencimiento;
                }
                EstadoCaso estadocasotesteo = db.EstadoCasos.Find(caso.IdEstadoCaso);
                if (estadocasotesteo.Descripcion == "Cerrado")
                {

                    casoOriginal.FechaCierre = DateTime.Now;
                    casoOriginal.IdMotivoCierreCaso = caso.IdMotivoCierreCaso;
                }

                if (casoOriginal.HistorialCaso == null)
                {
                    casoOriginal.HistorialCaso = new List<HistorialCaso>();

                }
                casoOriginal.HistorialCaso.Add(historialCaso);
                IList<DiaDevolucionCaso> daysForDelete =
                    casoOriginal.DiasDevoluciones != null ? casoOriginal.DiasDevoluciones.ToList() : new List<DiaDevolucionCaso>();

                foreach (var daytodelete in daysForDelete)
                {
                    db.DiaDevolucionCasos.Remove(daytodelete);

                }
                db.SaveChanges();
                if (lineaDiasFecha != null)
                {
                    foreach (var item in lineaDiasFecha)
                    {
                        DiaDevolucionCaso devolucionCaso = new DiaDevolucionCaso();
                        devolucionCaso.Fecha = Convert.ToDateTime(item);
                        devolucionCaso.FechaModificacion = DateTime.Now;
                        devolucionCaso.Observaciones = lineaDiasObs[lineaDiasFecha.ToList().IndexOf(item)];
                        devolucionCaso.UsuarioModificacion = User.Identity.Name;
                        db.DiaDevolucionCasos.Add(devolucionCaso);
                        db.SaveChanges();
                        casoOriginal.DiasDevoluciones.Add(devolucionCaso);
                    }
                }


                db.Entry(casoOriginal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }




            ViewBag.IdAsuntoCaso = new SelectList(db.AreaCasos, "IdAreaCaso", "Descripcion", caso.IdAsuntoCaso);
            ViewBag.IdEstadoCaso = new SelectList(db.EstadoCasos, "IdEstadoCaso", "Descripcion", caso.IdEstadoCaso);

            ViewBag.IdSuscripcion = new SelectList(db.Suscripciones, "IdSuscripcion", "Nombre", caso.IdSuscripcion);
            ViewBag.IdTipoCaso = new SelectList(db.TipoCasos, "IdTipoCaso", "Descripcion", caso.IdTipoCaso);
            return View(caso);
        }

        // GET: Casos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caso caso = db.Casos.Find(id);
            if (caso == null)
            {
                return HttpNotFound();
            }
            return View(caso);
        }

        // POST: Casos/Delete/5
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Caso caso = db.Casos.Find(id);
            if (caso.DiasDevoluciones.Any())
            {
                var diasDevoluciones = db.DiaDevolucionCasos.ToList().FindAll(d => d.Caso.IdCaso == id);
                foreach (var devolucion in diasDevoluciones)
                {
                    db.DiaDevolucionCasos.Remove(devolucion);
                }
            }
            db.Casos.Remove(caso);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
