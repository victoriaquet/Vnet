using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SNC2017.Models;
using SNC2017.ViewModels;

namespace SNC2017.Controllers
{
    public class CuentasBancariasController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: CuentasBancarias
        public ActionResult Index()
        {
            return View(db.CuentaBancarias.ToList());
        }
        [HttpPost]
        public JsonResult EliminarCta(int? id)
        {
            var status = false;
            CuentaBancaria cuentabancaria = new CuentaBancaria();
            cuentabancaria = db.CuentaBancarias.Find(id);

            db.CuentaBancarias.Remove(cuentabancaria);
            db.SaveChanges();
            status = true;
            return Json(status, JsonRequestBehavior.AllowGet);

        }
        // GET: CuentasBancarias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentaBancaria cuentaBancaria = db.CuentaBancarias.Find(id);
            if (cuentaBancaria == null)
            {
                return HttpNotFound();
            }
            return View(cuentaBancaria);
        }
        // GET: CuentasBancarias/edit
        public ActionResult Edit(int? id, int? idsuscriptor)
        {
            Suscriptor suscriptor = db.Suscriptores.Find(idsuscriptor);
            ViewBag.localidades = db.Localidades.ToList();
            ViewBag.provincias = db.Provincias.ToList();
            ViewBag.idsuscriptor = suscriptor.IdSuscriptor;
            ViewBag.nombresuscriptor = suscriptor.Nombre;
            ViewBag.apellidosuscriptor = suscriptor.Apellido;
            ViewBag.numerosuscriptor = suscriptor.NumeroSuscriptor;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentaBancaria cuentabancaria = db.CuentaBancarias.Find(id);
            if (cuentabancaria == null)
            {
                return HttpNotFound();
            }
            return View(cuentabancaria);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCuentaBancaria,CBU,NombreTitular,IdDomicilio,DNITitular,DomicilioTitular,CUITTitular,AliasCuenta")] CuentaBancaria cuentaBancaria, int IdSuscriptor)
        {
          
            //cuentaBancaria.DomicilioTitular.Localidad.IdLocalidad = cuentaBancaria.DomicilioTitular.IdLocalidad;
            //Domicilio domicilio = new Domicilio();
            //domicilio = cuentaBancaria.DomicilioTitular;
            db.Entry(cuentaBancaria).State = EntityState.Modified;
            db.SaveChanges();
           
            //db.Entry(domicilio).State = EntityState.Modified;
            //db.SaveChanges();
            return RedirectToAction("Details", "Suscriptores", new { id = IdSuscriptor });



        }
        // GET: CuentasBancarias/Create
        public ActionResult Create(int? idsuscriptor)
        {
            if (idsuscriptor != null)
            {
                Suscriptor suscriptor = db.Suscriptores.Find(idsuscriptor);
          
                ViewBag.idsuscriptor = suscriptor.IdSuscriptor;
                ViewBag.nombresuscriptor = suscriptor.Nombre;
                ViewBag.apellidosuscriptor = suscriptor.Apellido;
                ViewBag.numerosuscriptor = suscriptor.NumeroSuscriptor;
            }
            ViewBag.localidades = db.Localidades.ToList();
            ViewBag.provincias = db.Provincias.ToList();

            return View();
        }

        // POST: TarjetasPago/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSuscriptor,CBU,NombreTitular,IdLocalidad,IdProvincia,CUITTitular,AliasCuenta,DNITitular,CalleSuscriptor,AlturaSuscriptor,PisoSuscriptor,DepartamentoSuscriptor,CalleLateral1Suscriptor,CalleLateral2Suscriptor,BarrioSuscriptor,ObservacionesDomicilioSuscriptor")] SuscriptorVM suscriptorVm)
        {
            Suscriptor suscriptor = db.Suscriptores.Find(suscriptorVm.IdSuscriptor);
            //Domicilio domicilio = new Domicilio
            //{
            //    Calle = suscriptorVm.CalleSuscriptor,
            //    Altura = suscriptorVm.AlturaSuscriptor,
            //    Piso = suscriptorVm.PisoSuscriptor,
            //    Departamento = suscriptorVm.DepartamentoSuscriptor,
            //    CalleLateral1 = suscriptorVm.CalleLateral1Suscriptor,
            //    CalleLateral2 = suscriptorVm.CalleLateral2Suscriptor,
            //    Barrio = suscriptorVm.BarrioSuscriptor,
            //    IdLocalidad = suscriptorVm.IdLocalidad,
            //    Observaciones = suscriptorVm.ObservacionesDomicilioSuscriptor
            //};
            //db.Domicilios.Add(domicilio);
            //db.SaveChanges();

            CuentaBancaria cuentabancaria = new CuentaBancaria();
            cuentabancaria.CBU = suscriptorVm.CBU;
            cuentabancaria.NombreTitular = suscriptorVm.NombreTitular;
            cuentabancaria.DNITitular = suscriptorVm.DNITitular;
            cuentabancaria.CUITTitular = suscriptorVm.CUITTitular;
            cuentabancaria.AliasCuenta = suscriptorVm.AliasCuenta;
            //cuentabancaria.IdDomicilio = domicilio.IdDomicilio;
         
            db.CuentaBancarias.Add(cuentabancaria);
            db.SaveChanges();

            suscriptor.CuentasBancarias.Add(cuentabancaria);
            db.Entry(suscriptor).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", "Suscriptores", new { id = suscriptor.IdSuscriptor });



        }

    

        // GET: CuentasBancarias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentaBancaria cuentaBancaria = db.CuentaBancarias.Find(id);
            if (cuentaBancaria == null)
            {
                return HttpNotFound();
            }
            return View(cuentaBancaria);
        }

        // POST: CuentasBancarias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CuentaBancaria cuentaBancaria = db.CuentaBancarias.Find(id);
            db.CuentaBancarias.Remove(cuentaBancaria);
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
