using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Rotativa;
using Rotativa.Options;
using SNC2017.Models;
using SNC2017.ViewModels;

namespace SNC2017.Controllers
{
    public class HojaDeRutasController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: HojaDeRutas
        public ActionResult Index()
        {
            var hojasderutas=new List<HojaDeRuta>();
            if (User.IsInRole("Distribuidor"))
            {
                hojasderutas = db.HojaDeRutas.ToList().FindAll(x => x.Canillita.IdDistribuidor == Int32.Parse(db.Users.ToList().Find(z => z.Id == User.Identity.GetUserId()).Identificador));
            }
            else
            {
                hojasderutas = db.HojaDeRutas.ToList();
            }
            var hojaderutatemporalvm = new HojaRutaTemporalVM();
            hojaderutatemporalvm.HojaDeRutaAyer = hojasderutas.FindAll(x => x.FechaEntrega == DateTime.Now.AddDays(-1).Date);
            hojaderutatemporalvm.HojaDeRutaHoy = hojasderutas.FindAll(x => x.FechaEntrega == DateTime.Now.Date);
            hojaderutatemporalvm.HojaDeRutaMañana = hojasderutas.FindAll(x => x.FechaEntrega == DateTime.Now.AddDays(+1).Date);
            hojaderutatemporalvm.HojaDeRutaTodas = hojasderutas;

            return View(hojaderutatemporalvm);
        }
        

        // GET: HojaDeRutas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HojaDeRuta hojaDeRuta = db.HojaDeRutas.Find(id);
            if (hojaDeRuta == null)
            {
                return HttpNotFound();
            }
            return View(hojaDeRuta);
        }

        // GET: HojaDeRutas/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            ViewBag.IdCanillita = new SelectList(db.Canillitas, "IdCanillita", "Nombre");
            return View();
        }
        // GET: HojaDeRutas/Create
        public ActionResult Imprimir()
        {
            return View();
        }
        // GET: HojaDeRutas/Create
        public ActionResult ImprimirPost([Bind(Include = "FechaEntrega")] HojaDeRuta hojaDeRuta)
        {
            var fecha = hojaDeRuta.FechaEntrega;
            var hojasderutas = new List<HojaDeRuta>();
            if (User.IsInRole("Distribuidor"))
            {
                if (db.HojaDeRutas.ToList().FindAll(x => x.Canillita.IdDistribuidor == Int32.Parse(db.Users.ToList().Find(z => z.Id == User.Identity.GetUserId()).Identificador) & x.FechaEntrega == fecha).Count == 0) { return Json("No existen hojas de rutas generadas para la fecha: " + fecha.ToShortDateString()); }
                hojasderutas = db.HojaDeRutas.ToList().FindAll(x => x.Canillita.IdDistribuidor == Int32.Parse(db.Users.ToList().Find(z => z.Id == User.Identity.GetUserId()).Identificador) & x.FechaEntrega == fecha);
            }
            else
            {
                if (db.HojaDeRutas.ToList().FindAll(x => x.FechaEntrega == fecha).Count == 0) { return Json("No existen hojas de rutas generadas para la fecha: " + fecha.ToShortDateString()); }
                hojasderutas = db.HojaDeRutas.ToList();
            }
            
            string footer = "";
            MemoryStream ms = new MemoryStream();
            var a = new ViewAsPdf();
            a = new ViewAsPdf();
            a.ViewName = "ImprimirPost";
            a.Model = hojasderutas;
            a.FileName = "HR-" + fecha.ToShortDateString() + ".pdf";
            a.PageMargins = new Margins(5, 5, 5, 5);
            footer = "--footer-center \"Generado: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Página: [page]/[toPage]\"" + " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            a.CustomSwitches = footer;
            var pdfBytes = a.BuildPdf(ControllerContext);
            ms = new MemoryStream(pdfBytes);
            return File(ms, "application/pdf", a.FileName);

        }
        public static string RemoveExcessCharacters(string value, int maxLen)
        {
            return (value.Length > maxLen) ? value.Substring(0, maxLen) : value;
        }
        public ActionResult ArchivoServinor()
        {
            return View();
        }
        public ActionResult ArchivoServinorPost([Bind(Include = "FechaEntrega")] DateTime FechaEntrega)
        {
            var hojasderutas = new List<HojaDeRuta>();

            if (User.IsInRole("Distribuidor"))
            {
                hojasderutas = db.HojaDeRutas.ToList().FindAll(x => x.FechaEntrega == FechaEntrega && x.Canillita.IdDistribuidor == Int32.Parse(db.Users.ToList().Find(z => z.Id == User.Identity.GetUserId()).Identificador));
            }
            else
            {
                hojasderutas = db.HojaDeRutas.ToList().FindAll(x => x.FechaEntrega == FechaEntrega);
            }
            MemoryStream ms = new MemoryStream();
            TextWriter tw = new StreamWriter(ms);
            int cant = 0;
            if (hojasderutas.Count == 0) { return Json("No existen hojas de rutas generadas para la fecha: "+FechaEntrega.ToShortDateString()); }

            try
            {
                foreach (var h in hojasderutas)
                {
                    cant = cant + h.Lineas.Count;
                }
                foreach (var hr in hojasderutas)
                {
                    foreach (var linea in hr.Lineas)
                    {
                        string codigodistribuidor = hr.Canillita.Distribuidor.Codigo.PadLeft(6, '0');
                        string nombredistribuidor = RemoveExcessCharacters(hr.Canillita.Distribuidor.Nombre.ToUpper(), 50).PadRight(50);
                        string codigocanillita = hr.Canillita.NroCanillita.ToString().PadLeft(6, '0');
                        string nombrecanillita = RemoveExcessCharacters(hr.Canillita.Nombre.ToUpper(), 50).PadRight(50);
                        string ciudaddistribuidor = RemoveExcessCharacters(hr.Canillita.Distribuidor.Localidad.Nombre.ToUpper(), 50).PadRight(50);

                        //colocar en lineas codigo producto, nombre producto, numero de suscripcion
                        string codigoproducto = linea.CodigoProducto;
                        string nombreproducto = RemoveExcessCharacters(linea.NombreProducto.ToUpper(), 50).PadRight(50);
                        string numerosuscripcion = linea.NumeroSuscripcion.PadLeft(6, '0');
                        string direccion = RemoveExcessCharacters(linea.Domicilio.ToUpper(), 150).PadRight(150);
                        string observaciondireccion = "".PadRight(50);
                        if (linea.ObservacionLimpia != null)
                        {
                            observaciondireccion = RemoveExcessCharacters(linea.ObservacionLimpia.ToUpper(),50).PadRight(50);
                        }

                        string localidaddireccion = RemoveExcessCharacters(linea.Localidad.ToUpper(), 50).PadRight(50);
                        string cantejemplares = "001";
                        string entregafecha = FechaEntrega.ToShortDateString();
                        string primeraentrega = "NO";
                        string activa = "ACTIVA".PadRight(50);
                        if (linea.PrimeraEntrega)
                        {
                            primeraentrega = "SI";
                        }
                        string ultimaentrega = "NO";
                        if (linea.UltimaEntrega)
                        {
                            ultimaentrega = "SI";
                        }
                       
                        string fechasuspension = "          ";
                        string cantidadentregas = cant.ToString();
                        tw.WriteLine(codigodistribuidor + ";" + nombredistribuidor + ";" + codigocanillita + ";" + nombrecanillita + ";" + ciudaddistribuidor + ";" + codigoproducto + ";" + nombreproducto + ";" + numerosuscripcion + ";" + direccion + ";" + observaciondireccion + ";" + localidaddireccion + ";" + cantejemplares + ";" + entregafecha + ";" + primeraentrega + ";" + ultimaentrega +";"+ activa+ ";" + fechasuspension + ";" + cantidadentregas);


                    }
                }

                tw.WriteLine("");
                tw.Flush();
                ms.Close();
                return File(ms.GetBuffer(), "text/plain", "SERVINOR " + FechaEntrega.ToString("dd-MM-yyyy") + ".txt");
            }
            catch (Exception e)
            {
                return Json("Ha ocurrido un error.");
            }
           

        }

        // POST: HojaDeRutas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Create([Bind(Include = "IdHojaDeRuta,IdCanillita,FechaEntrega,FechaGeneracion")] HojaDeRuta hojaDeRuta)
        {
            //esta seria la fecha de entrega que ingresa el usuario
            var fecha = hojaDeRuta.FechaEntrega;
            if (db.HojaDeRutas.ToList().FindAll(x => x.FechaEntrega == fecha).Count > 0) { return Json("No es posible generar hojas de ruta. Ya se han generado previamente las hojas de ruta para la fecha: " + fecha.ToShortDateString()); }

            //obtengo el dia de la semana seleccionado Lun Mar Mier Jue Vie Sab Dom
            var dia = (int)fecha.DayOfWeek;
            var d = dia;
            if (dia == 0) { d = 7; }
            //Obtengo el listado de canillitas activos
            var canillitasactivos = db.Canillitas.ToList().FindAll(x => x.Activo);
            //obtengo el listado de suscripciones activas
            //por ahora se obtiene primero el listado de las suscripciones que poseen estado de sucripcion en el historial.
            //tira error si la cantidad era 0
            var suscripcionesconestadosuscripcion = db.Suscripciones.ToList().FindAll(x =>
                x.EstadoSuscripcionHistorial.Count>0);
            var suscripcionesactivas= suscripcionesconestadosuscripcion.FindAll(x =>
                x.EstadoSuscripcionHistorial.LastOrDefault().EstadoSuscripcion == EstadoSuscripcion.Activa &
                x.IdCanillita != null);

            //recorro los canillitas activos para filtrar las suscripciones
            foreach (var c in canillitasactivos)
            {
                //inicializo los datos basicos de la hoja de ruta - una por cada canillita
                var hojaruta = new HojaDeRuta();
                hojaruta.IdCanillita = c.IdCanillita;
                hojaruta.FechaEntrega = fecha;
                hojaruta.FechaGeneracion = DateTime.Now;
                db.HojaDeRutas.Add(hojaruta);
                db.SaveChanges();
                //bool para eliminar la hoja si es que no tiene entregas en el dia seleccionado
                var poseeentregas = false;
                //obtengo las sucripciones que tienen asignado ese canillita en algun dia de entrega
                var suscripcionescanillita = suscripcionesactivas.ToList().FindAll(x => x.DiaEntregas.ToList().Any(a=>a.IdCanillita==c.IdCanillita & (int)a.NombreDia==d));
                int numeroorden = 1;

                //selecciono las suscripciones que estan habilitadas para el dia solicitado
                foreach (var s in suscripcionescanillita)
                {
                   
                        poseeentregas = true;
                        //cada suscripcion es sera una linea en la hoja de ruta del canillita
                        if (s.TipoSuscripcion == TipoSuscripcion.Normal | s.TipoSuscripcion==TipoSuscripcion.Cortesia)
                        {
                            #region SuscripcionNormal
                            var linea = new Linea();
                            var codigoproducto = "";
                            var nombreproducto = "";
                            var numerosuscripcion = "";
                            linea.NumeroOrden = numeroorden;
                            numeroorden++;
                            linea.NumeroSuscriptor = s.Suscriptor.NumeroSuscriptor;
                            linea.NombreSuscriptor = s.Suscriptor.Apellido + ", " + s.Suscriptor.Nombre;
                            linea.UltimaEntrega = false;
                            try
                            {
                                //ACA SE TRAE EL DOMICILIO DEL DIA DE ENTREGA
                                var dentrega = s.DiaEntregas.ToList().Find(x => d == (int)x.NombreDia);
                                linea.Domicilio = dentrega.Domicilio.Calle + " " + dentrega.Domicilio.Altura + "/" + dentrega.Domicilio.Barrio;
                                linea.Localidad = dentrega.Domicilio.Localidad.Nombre;
                                linea.Observacion = dentrega.Domicilio.Observaciones;
                                linea.ObservacionLimpia = dentrega.Domicilio.Observaciones;
                            }
                            catch (Exception e)
                            {
                                //ACA SE TRAE EL DOMICILIO POR DEFECTO DEL SUSCRIPTOR
                                linea.Domicilio = s.Suscriptor.Domicilio.Calle + " " + s.Suscriptor.Domicilio.Altura + "/" + s.Suscriptor.Domicilio.Barrio;
                                linea.Localidad = s.Suscriptor.Domicilio.Localidad.Nombre;
                                linea.Observacion = s.Suscriptor.Domicilio.Observaciones;
                                linea.ObservacionLimpia = s.Suscriptor.Domicilio.Observaciones;
                            }
                            linea.Lunes = s.Lunes;
                            linea.Martes = s.Martes;
                            linea.Miercoles = s.Miercoles;
                            linea.Jueves = s.Jueves;
                            linea.Viernes = s.Viernes;
                            linea.Sabado = s.Sabado;
                            linea.Domingo = true;
                            linea.NumeroSuscripcion = s.NumeroSuscripcion.ToString();
                            linea.CodigoProducto = /*s.Oferta.CodigoProductoServinor*/"000001";
                            linea.NombreProducto = /*s.Oferta.NombreProductoServinor*/"DIARIO NORTE";
                            //almaceno la relacion de suscripcion con la hoja de ruta
                            var suscripcionhojaruta = new SuscripcionHojaDeRuta();
                            if (s.SuscripcionHojaDeRutas.Count == 0)
                            {
                                linea.Observacion = "*1° ENTREGA*-" + linea.Observacion;}
                            suscripcionhojaruta.IdSuscripcion = s.IdSuscripcion;
                            suscripcionhojaruta.IdHojaDeRuta = hojaruta.IdHojaDeRuta;
                            db.SuscripcionHojaDeRutas.Add(suscripcionhojaruta);
                            db.SaveChanges();
                            //agrego la linea de la suscripcion a la hoja de ruta
                            linea.IdHojaDeRuta = hojaruta.IdHojaDeRuta;
                            db.Lineas.Add(linea);
                            db.SaveChanges();
                            #endregion
                        }
                        else
                        {
                            if (s.TipoSuscripcion == TipoSuscripcion.Coorporativo)
                        {
                            #region suscriptorprincipal
                            var suscripcionhojarutaprinc = new SuscripcionHojaDeRuta();
                            suscripcionhojarutaprinc.IdSuscripcion = s.IdSuscripcion;
                            suscripcionhojarutaprinc.IdHojaDeRuta = hojaruta.IdHojaDeRuta;
                            db.SuscripcionHojaDeRutas.Add(suscripcionhojarutaprinc);
                            db.SaveChanges();

                            var lineaprin = new Linea();
                            lineaprin.NumeroOrden = numeroorden;
                            numeroorden++;
                            lineaprin.NumeroSuscriptor = s.Suscriptor.NumeroSuscriptor;
                            lineaprin.NombreSuscriptor = s.Suscriptor.Apellido + ", " + s.Suscriptor.Nombre;
                            lineaprin.UltimaEntrega = false;

                            try
                            {
                                //ACA SE TRAE EL DOMICILIO DEL DIA DE ENTREGA
                                var dentregaprin = s.DiaEntregas.ToList().Find(x => d == (int)x.NombreDia);
                                lineaprin.Domicilio = dentregaprin.Domicilio.Calle + " " + dentregaprin.Domicilio.Altura + "/" + dentregaprin.Domicilio.Barrio;
                                lineaprin.Localidad = dentregaprin.Domicilio.Localidad.Nombre;
                                lineaprin.Observacion = dentregaprin.Domicilio.Observaciones;
                                lineaprin.ObservacionLimpia = dentregaprin.Domicilio.Observaciones;

                            }
                            catch (Exception e)
                            {
                                //ACA SE TRAE EL DOMICILIO POR DEFECTO DEL SUSCRIPTOR
                                lineaprin.Domicilio = s.Suscriptor.Domicilio.Calle + " " + s.Suscriptor.Domicilio.Altura + "/" + s.Suscriptor.Domicilio.Barrio;
                                lineaprin.Localidad = s.Suscriptor.Domicilio.Localidad.Nombre;
                                lineaprin.Observacion = s.Suscriptor.Domicilio.Observaciones;
                                lineaprin.ObservacionLimpia = s.Suscriptor.Domicilio.Observaciones;

                            }
                            lineaprin.Lunes = s.Lunes;
                            lineaprin.Martes = s.Martes;
                            lineaprin.Miercoles = s.Miercoles;
                            lineaprin.Jueves = s.Jueves;
                            lineaprin.Viernes = s.Viernes;
                            lineaprin.Sabado = s.Sabado;
                            lineaprin.Domingo = true;
                            lineaprin.NumeroSuscripcion = s.NumeroSuscripcion.ToString();
                            lineaprin.CodigoProducto = /*s.Oferta.CodigoProductoServinor*/"000001";
                            lineaprin.NombreProducto = /*s.Oferta.NombreProductoServinor*/"DIARIO NORTE";
                            //agrego la linea de la suscripcion a la hoja de ruta
                            lineaprin.IdHojaDeRuta = hojaruta.IdHojaDeRuta;
                            db.Lineas.Add(lineaprin);
                            db.SaveChanges();


                            #endregion

                            #region SuscripcionCoorporativa
                            //almaceno la relacion de suscripcion con la hoja de ruta
                            var suscripcionhojaruta = new SuscripcionHojaDeRuta();
                                suscripcionhojaruta.IdSuscripcion = s.IdSuscripcion;
                                suscripcionhojaruta.IdHojaDeRuta = hojaruta.IdHojaDeRuta;
                                db.SuscripcionHojaDeRutas.Add(suscripcionhojaruta);
                                db.SaveChanges();
                                foreach (var t in s.SuscriptorSecundarios)
                                {
                                    var linea = new Linea();
                                    linea.NumeroOrden = numeroorden;
                                    numeroorden++;
                                    linea.NumeroSuscriptor = t.Suscriptor.NumeroSuscriptor;
                                    linea.NombreSuscriptor = t.Suscriptor.Apellido + ", " + t.Suscriptor.Nombre;
                                    linea.UltimaEntrega = false;
                                try
                                    {
                                        //ACA SE TRAE EL DOMICILIO DEL DIA DE ENTREGA
                                        var dentrega = t.DiaEntregas.ToList().Find(x => d == (int)x.NombreDia);
                                        linea.Domicilio = dentrega.Domicilio.Calle + " " + dentrega.Domicilio.Altura + "/" + dentrega.Domicilio.Barrio;
                                        linea.Localidad = dentrega.Domicilio.Localidad.Nombre;
                                        linea.Observacion = dentrega.Domicilio.Observaciones;
                                        linea.ObservacionLimpia = dentrega.Domicilio.Observaciones;
                                }
                                    catch (Exception e)
                                    {
                                        //ACA SE TRAE EL DOMICILIO POR DEFECTO DEL SUSCRIPTOR
                                        linea.Domicilio = s.Suscriptor.Domicilio.Calle + " " + s.Suscriptor.Domicilio.Altura + "/" + s.Suscriptor.Domicilio.Barrio;
                                        linea.Localidad = s.Suscriptor.Domicilio.Localidad.Nombre;
                                        linea.Observacion = s.Suscriptor.Domicilio.Observaciones;
                                        linea.ObservacionLimpia = s.Suscriptor.Domicilio.Observaciones;
                                }
                                    linea.Lunes = s.Lunes;
                                    linea.Martes = s.Martes;
                                    linea.Miercoles = s.Miercoles;
                                    linea.Jueves = s.Jueves;
                                    linea.Viernes = s.Viernes;
                                    linea.Sabado = s.Sabado;
                                    linea.Domingo = true;
                                    linea.NumeroSuscripcion = s.NumeroSuscripcion.ToString();
                                    linea.CodigoProducto = s.Oferta.CodigoProductoServinor;
                                    linea.NombreProducto = s.Oferta.NombreProductoServinor;
                                //agrego la linea de la suscripcion a la hoja de ruta
                                linea.IdHojaDeRuta = hojaruta.IdHojaDeRuta;
                                    db.Lineas.Add(linea);
                                    db.SaveChanges();
                                }
                                #endregion
                            }

                        }
                }
                //si no posee entregas se elimina la hoja de ruta
                if (!poseeentregas) {
                    db.HojaDeRutas.Remove(hojaruta);
                    db.SaveChanges();
                }
            }
            
                return RedirectToAction("Index");
            
        }

        // GET: HojaDeRutas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HojaDeRuta hojaDeRuta = db.HojaDeRutas.Find(id);
            if (hojaDeRuta == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCanillita = new SelectList(db.Canillitas, "IdCanillita", "Nombre", hojaDeRuta.IdCanillita);
            return View(hojaDeRuta);
        }

        // POST: HojaDeRutas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdHojaDeRuta,IdCanillita,FechaEntrega,FechaGeneracion")] HojaDeRuta hojaDeRuta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hojaDeRuta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCanillita = new SelectList(db.Canillitas, "IdCanillita", "Nombre", hojaDeRuta.IdCanillita);
            return View(hojaDeRuta);
        }

        // GET: HojaDeRutas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HojaDeRuta hojaDeRuta = db.HojaDeRutas.Find(id);
            if (hojaDeRuta == null)
            {
                return HttpNotFound();
            }
            return View(hojaDeRuta);
        }

        // POST: HojaDeRutas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HojaDeRuta hojaDeRuta = db.HojaDeRutas.Find(id);
            db.HojaDeRutas.Remove(hojaDeRuta);
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
