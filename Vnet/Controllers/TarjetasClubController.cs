using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SNC2017.Models;

namespace SNC2017.Controllers
{
    public class TarjetasClubController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: TarjetasClub
        public ActionResult Index()
        {
            var TarjetaClubes = db.TarjetaClubes.Include(t => t.Suscripcion);
            return View(TarjetaClubes.ToList());
        }
        [Authorize(Roles = "Administrador,Comercio")]
        public ActionResult BuscarTarjeta()
        {
            return View();
        }

        // GET: TarjetasClub/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TarjetaClub tarjetaClub = db.TarjetaClubes.Find(id);
            if (tarjetaClub == null)
            {
                return HttpNotFound();
            }
            return View(tarjetaClub);
        }
        public ActionResult TarjetaClubImpresa(string[] idtcimp, string urlact)
        {
            foreach (var item in idtcimp)
            {
                int id = Int32.Parse(item);
                TarjetaClub tarjetaClub = db.TarjetaClubes.Find(id);
                tarjetaClub.EstadoTarjetaClub = EstadoTarjetaClub.Impresa;
                tarjetaClub.FechaImpresion=DateTime.Now;
                db.Entry(tarjetaClub).State = EntityState.Modified;
                db.SaveChanges();
                }

            TempData["ok"] = "success";
            return Redirect(urlact);
        }
        public ActionResult TarjetaClubPentregar(string[] idtcimp, string urlact, int ubicacion)
        {
            
            foreach (var item in idtcimp)
            {
                int id = Int32.Parse(item);
                TarjetaClub tarjetaClub = db.TarjetaClubes.Find(id);
                tarjetaClub.EstadoTarjetaClub = EstadoTarjetaClub.ListaParaEntregar;
                tarjetaClub.FechaParaEntregar = DateTime.Now;
                tarjetaClub.UbicacionTarjetaClub = (UbicacionTarjetaClub)ubicacion;
                db.Entry(tarjetaClub).State = EntityState.Modified;
                db.SaveChanges();
            }

            TempData["ok"] = "success";
            return Redirect(urlact);
        }
        public ActionResult TarjetaClubEntregadas(string[] idtcent, string urlact)
        {

            foreach (var item in idtcent)
            {
                int id = Int32.Parse(item);
                TarjetaClub tarjetaClub = db.TarjetaClubes.Find(id);
                tarjetaClub.EstadoTarjetaClub = EstadoTarjetaClub.Entregada;
                tarjetaClub.FechaEntrega=DateTime.Now;
                db.Entry(tarjetaClub).State = EntityState.Modified;
                db.SaveChanges();
            }

            TempData["ok"] = "success";
            return Redirect(urlact);
        }

        // GET: TarjetasClub/Create
        public ActionResult Create(int id)
        {
            ViewBag.error = false;
         
            Suscripcion suscripcion = db.Suscripciones.Find(id);
            if (suscripcion.Oferta.CantTarjetasAdicionales + 1 == suscripcion.TarjetaClub.Count(x=>x.EstadoTarjetaClub!=EstadoTarjetaClub.Baja))
            {
                ViewBag.error = true;
            }
            TarjetaClub tarjetaClub = new TarjetaClub();
            tarjetaClub.IdSuscripcion = id;
            return View(tarjetaClub);
        }

        // POST: TarjetasClub/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TarjetaClub tarjetaClub)
        {
            Suscripcion suscripcion = db.Suscripciones.Find(tarjetaClub.IdSuscripcion);
            if (suscripcion.Oferta.CantTarjetasAdicionales + 1 == suscripcion.TarjetaClub.Count(x=>x.EstadoTarjetaClub!=EstadoTarjetaClub.Baja))
            {
                return RedirectToAction("Create", new {id = tarjetaClub.IdSuscripcion});
            }

            TarjetaClub tc=new TarjetaClub();
            tc = tarjetaClub;
            tc.FechaSolicitudImpresion = DateTime.Now;
            tc.EstadoTarjetaClub = EstadoTarjetaClub.DerivadaAImpresion;
            tc.Numero = db.TarjetaClubes.Count() + 1;
            if (ModelState.IsValid)
            {
                db.TarjetaClubes.Add(tarjetaClub);
                db.SaveChanges();
                return RedirectToAction("Details", new {id=tarjetaClub.IdTarjetaClub} );
            }

            return View(tarjetaClub);
        }

        // POST: TarjetasClub/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCoorporativa(int idsuscripciontc,string[] suscriptoressecundarios)
        {
            Suscripcion suscripcion = db.Suscripciones.Find(idsuscripciontc);
            try
            {
                if (suscriptoressecundarios != null)
                {
                    foreach (var sc in suscriptoressecundarios)

                    {
                        //Se agregan los suscriptores secundarios
                        //y los dias de entrega de cada suscriptor,por defecto se cargan los de la empresa
                        SuscriptorSecundario suscriptorsecundario = new SuscriptorSecundario();
                        suscriptorsecundario.IdSuscriptor = Int32.Parse(sc);
                        suscriptorsecundario.DiaEntregas = new List<DiaEntrega>();
                        foreach (var diaentregasecundario in suscripcion.DiaEntregas)
                        {
                            DiaEntrega diasecundario = new DiaEntrega();
                            Domicilio domiciliosecundario = new Domicilio();
                            domiciliosecundario.Calle = diaentregasecundario.Domicilio.Calle;
                            domiciliosecundario.Altura = diaentregasecundario.Domicilio.Altura;
                            domiciliosecundario.Piso = diaentregasecundario.Domicilio.Piso;
                            domiciliosecundario.Departamento = diaentregasecundario.Domicilio.Departamento;
                            domiciliosecundario.CalleLateral1 = diaentregasecundario.Domicilio.CalleLateral1;
                            domiciliosecundario.CalleLateral2 = diaentregasecundario.Domicilio.CalleLateral2;
                            domiciliosecundario.Barrio = diaentregasecundario.Domicilio.Barrio;
                            domiciliosecundario.IdLocalidad = diaentregasecundario.Domicilio.IdLocalidad;
                            domiciliosecundario.Observaciones = diaentregasecundario.Domicilio.Observaciones;
                            domiciliosecundario.TituloDom = diaentregasecundario.Domicilio.TituloDom;

                            db.Domicilios.Add(domiciliosecundario);
                            db.SaveChanges();
                            diasecundario.IdDomicilio = domiciliosecundario.IdDomicilio;
                            diasecundario.Habilitado = diaentregasecundario.Habilitado;
                            diasecundario.NombreDia = diaentregasecundario.NombreDia;
                            db.DiaEntregas.Add(diasecundario);
                            db.SaveChanges();
                            suscriptorsecundario.DiaEntregas.Add(diasecundario);
                            db.SaveChanges();


                        }
                        //se guarda la tarjetaclub secundarias
                        Suscriptor suscsec = new Suscriptor();
                        suscsec = db.Suscriptores.Find(Int32.Parse(sc));
                        var tarjetaClub = new TarjetaClub();
                        tarjetaClub.Nombres = suscsec.Nombre;
                        tarjetaClub.Apellido = suscsec.Apellido;
                        tarjetaClub.DNI = suscsec.DNI;
                        if (SuscripcionesController.IsValidSqlDateTime(suscsec.FechaNacimiento)) { tarjetaClub.FechaNacimiento = suscsec.FechaNacimiento; }
                        tarjetaClub.Email = suscsec.Email;
                        tarjetaClub.TelefonoMovilArea = suscsec.TelefonoMovilArea;
                        tarjetaClub.TelefonoMovilNumero = suscsec.TelefonoMovilNumero;
                        tarjetaClub.FechaSolicitudImpresion = DateTime.Now;
                        tarjetaClub.EstadoTarjetaClub = EstadoTarjetaClub.DerivadaAImpresion;
                        tarjetaClub.Numero = db.TarjetaClubes.Count() + 1;
                        tarjetaClub.TipoTarjetaClub = TipoTarjetaClub.Adicional;
                        tarjetaClub.IdSuscripcion = idsuscripciontc;
                        db.TarjetaClubes.Add(tarjetaClub);
                        db.SaveChanges();
                        //fin guardar tarjetaclub

                        //se guarda el suscriptorsecundario con la relacion a tarjetaclub
                        //y se lo agrega a la suscripcion
                        suscriptorsecundario.IdTarjetaClub = tarjetaClub.IdTarjetaClub;
                        db.SuscriptorSecundarios.Add(suscriptorsecundario);
                        db.SaveChanges();
                        suscripcion.SuscriptorSecundarios.Add(suscriptorsecundario);
                        db.SaveChanges();
                        //fin guardar suscriptorsecundario

                    }
                }

            }
            catch (Exception e)
            {

            }

            return RedirectToAction("Details","Suscripciones", new { id=idsuscripciontc });
        }

        // GET: TarjetasClub/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.error = false;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TarjetaClub tarjetaClub = db.TarjetaClubes.Find(id);
            if (tarjetaClub == null)
            {
                return HttpNotFound();
            }

            if (tarjetaClub.EstadoTarjetaClub == EstadoTarjetaClub.DerivadaAImpresion |
                tarjetaClub.EstadoTarjetaClub == EstadoTarjetaClub.Pendiente)
            {
                return View(tarjetaClub);
            }
            ViewBag.error = true;
            return View(tarjetaClub);
        }

        // POST: TarjetasClub/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TarjetaClub tarjetaClub)
        {
            TarjetaClub tc = db.TarjetaClubes.Find(tarjetaClub.IdTarjetaClub);
            if (tc.EstadoTarjetaClub == EstadoTarjetaClub.DerivadaAImpresion |
                tc.EstadoTarjetaClub == EstadoTarjetaClub.Pendiente)
            {
                tc.Nombres = tarjetaClub.Nombres;
                tc.Apellido = tarjetaClub.Apellido;
                tc.Precio = tarjetaClub.Precio;
                tc.Email = tarjetaClub.Email;
                tc.FechaNacimiento = tarjetaClub.FechaNacimiento;
                tc.DNI = tarjetaClub.DNI;
                tc.TelefonoMovilArea = tarjetaClub.TelefonoMovilArea;
                tc.TelefonoMovilNumero = tarjetaClub.TelefonoMovilNumero;
                tc.TipoSexo = tarjetaClub.TipoSexo;

                db.Entry(tc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Suscripciones", new { id = tc.IdSuscripcion });
            }
            return RedirectToAction("Edit", "TarjetasClub", new { id = tc.IdTarjetaClub });
            
        }
        [Authorize(Roles = "Administrador,Comercio")]
        public ActionResult ObtenerTarjetaClub(int? numero)
        {
           
            TarjetaClub tarjetaClub = db.TarjetaClubes.FirstOrDefault(x=>x.Numero==numero);
           
            return View(tarjetaClub);
        }

        public string GetTarjetasClub(string term)
        {
            term = term.ToLower();
            var tarjetasclub = db.TarjetaClubes.ToList().FindAll(x=>(x.Numero.ToString() + " " + x.Apellido.ToString().ToLower()+" "+x.Nombres.ToString().ToLower()).Contains(term));


            var data = tarjetasclub.Select(x => new { id = x.Numero, text = x.Numero + " " + x.Apellido.ToUpper()+" "+x.Nombres.ToUpper() });


            return JsonConvert.SerializeObject(data);
        }

        // GET: TarjetasClub/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TarjetaClub tarjetaClub = db.TarjetaClubes.Find(id);
            if (tarjetaClub == null)
            {
                return HttpNotFound();
            }
            return View(tarjetaClub);
        }

        // POST: TarjetasClub/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TarjetaClub tarjetaClub = db.TarjetaClubes.Find(id);
            db.TarjetaClubes.Remove(tarjetaClub);
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
