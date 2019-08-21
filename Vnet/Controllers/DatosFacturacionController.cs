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
    public class DatosFacturacionController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: DatosFacturacion
        public ActionResult Index()
        {
            return View();
        }

        // GET: DatosFacturacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatosFacturacion datosFacturacion = db.DatosFacturaciones.Find(id);
            if (datosFacturacion == null)
            {
                return HttpNotFound();
            }
            return View(datosFacturacion);
        }

        // GET: DatosFacturacion/Create
        public ActionResult Create()
        {
            ViewBag.IdSuscripcion = new SelectList(db.Suscripciones, "IdSuscripcion", "IdSuscripcion");
            return View();
        }

        // POST: DatosFacturacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDatosFacturacion,Nombre,Apellido,DNI,CUIT,RazonSocial,TipoIva,IdSuscripcion")] DatosFacturacion datosFacturacion)
        {
            if (ModelState.IsValid)
            {
                db.DatosFacturaciones.Add(datosFacturacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdSuscripcion = new SelectList(db.Suscripciones, "IdSuscripcion", "IdSuscripcion");
            return View(datosFacturacion);
        }

        // GET: DatosFacturacion/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.localidades = db.Localidades.ToList();
            ViewBag.provincias = db.Provincias.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatosFacturacion datosFacturacion = db.DatosFacturaciones.Find(id);
            if (datosFacturacion == null)
            {
                return HttpNotFound();
            }
            SuscripcionVM datosfac=new SuscripcionVM();
            datosfac.IdDatosFacturacion=datosFacturacion.IdDatosFacturacion;
            datosfac.NombreFacturacion=datosFacturacion.Nombre;
            datosfac.ApellidoFacturacion=datosFacturacion.Apellido;
            datosfac.DNIFacturacion=datosFacturacion.DNI;
            datosfac.CUITFacturacion=datosFacturacion.CUIT;
            datosfac.RazonSocialFacturacion=datosFacturacion.RazonSocial;
            datosfac.TipoIva=datosFacturacion.TipoIva;

            datosfac.IdLocalidadFacturacion=datosFacturacion.Domicilio.IdLocalidad;
            datosfac.LocalidadFacturacion=datosFacturacion.Domicilio.Localidad;
            datosfac.IdProvinciaFacturacion=datosFacturacion.Domicilio.Localidad.IdProvincia;
            datosfac.CalleFacturacion=datosFacturacion.Domicilio.Calle;
            datosfac.CalleLateral1Facturacion=datosFacturacion.Domicilio.CalleLateral1;
            datosfac.CalleLateral2Facturacion = datosFacturacion.Domicilio.CalleLateral2;
            datosfac.AlturaFacturacion=datosFacturacion.Domicilio.Altura;
            datosfac.DepartamentoFacturacion=datosFacturacion.Domicilio.Departamento;
            datosfac.PisoFacturacion=datosFacturacion.Domicilio.Piso;
            datosfac.BarrioFacturacion=datosFacturacion.Domicilio.Barrio;
            datosfac.ObservacionesDomicilioFacturacion=datosFacturacion.Domicilio.Observaciones;
            return View(datosfac);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDatosFacturacion,NombreFacturacion,ApellidoFacturacion,DNIFacturacion,CUITFacturacion,RazonSocialFacturacion,TipoIva,CalleFacturacion,AlturaFacturacion,PisoFacturacion,DepartamentoFacturacion,CalleLateral1Facturacion,CalleLateral2Facturacion,BarrioFacturacion,IdLocalidadFacturacion,ObservacionesDomicilioFacturacion")] SuscripcionVM suscriptorVM)
        {
            ViewBag.localidades = db.Localidades.ToList();
            ViewBag.provincias = db.Provincias.ToList();
            if (suscriptorVM.IdDatosFacturacion == null)
            {
                return HttpNotFound();
            }

            DatosFacturacion suscriptor = db.DatosFacturaciones.Find(suscriptorVM.IdDatosFacturacion);

            if (suscriptor == null)
            {
                return HttpNotFound();
            }
            //Mapeo suscriptor
            suscriptor.Nombre = suscriptorVM.NombreFacturacion;
            suscriptor.Apellido = suscriptorVM.ApellidoFacturacion;
            suscriptor.DNI = suscriptorVM.DNIFacturacion.Value;
            suscriptor.CUIT = suscriptorVM.CUITFacturacion;
            suscriptor.RazonSocial = suscriptorVM.RazonSocialFacturacion;
            suscriptor.TipoIva = suscriptorVM.TipoIva;
            db.Entry(suscriptor).State = EntityState.Modified;
            db.SaveChanges();
            //Mapeo Domicilio suscriptor
            Domicilio domicilio = db.Domicilios.Find(suscriptor.IdDomicilio);
            domicilio.Calle = suscriptorVM.CalleFacturacion;
            domicilio.Altura = suscriptorVM.AlturaFacturacion;
            domicilio.Piso = suscriptorVM.PisoFacturacion;
            domicilio.Departamento = suscriptorVM.DepartamentoFacturacion;
            domicilio.CalleLateral1 = suscriptorVM.CalleLateral1Facturacion;
            domicilio.CalleLateral2 = suscriptorVM.CalleLateral2Facturacion;
            domicilio.Barrio = suscriptorVM.BarrioFacturacion;
            domicilio.IdLocalidad = suscriptorVM.IdLocalidadFacturacion.Value;
            domicilio.Observaciones = suscriptorVM.ObservacionesDomicilioFacturacion;
            db.Entry(domicilio).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Details", new { id = suscriptor.IdDatosFacturacion });
        }


        // GET: DatosFacturacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatosFacturacion datosFacturacion = db.DatosFacturaciones.Find(id);
            if (datosFacturacion == null)
            {
                return HttpNotFound();
            }
            return View(datosFacturacion);
        }

        // POST: DatosFacturacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DatosFacturacion datosFacturacion = db.DatosFacturaciones.Find(id);
            db.DatosFacturaciones.Remove(datosFacturacion);
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
