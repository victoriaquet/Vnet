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
    public class TarjetasPagoController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: TarjetasPago
        public ActionResult Index()
        {
            return View(db.TarjetaPagos.ToList());
        }

        // GET: TarjetasPago/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TarjetaPago tarjetaPago = db.TarjetaPagos.Find(id);
            if (tarjetaPago == null)
            {
                return HttpNotFound();
            }
            return View(tarjetaPago);
        }
        [HttpPost]
        public JsonResult EliminarTarjeta(int? id)
        {
            var status = false;
            TarjetaPago tarjetapago = new TarjetaPago();
            tarjetapago = db.TarjetaPagos.Find(id);

            db.TarjetaPagos.Remove(tarjetapago);
            db.SaveChanges();
            status = true;
            return Json(status, JsonRequestBehavior.AllowGet);

        }
        // GET: TarjetasPago/Create
        public ActionResult Create(int? idsuscriptor)
        {
            Suscriptor suscriptor = db.Suscriptores.Find(idsuscriptor);
            ViewBag.idsuscriptor = suscriptor.IdSuscriptor;
            ViewBag.nombresuscriptor = suscriptor.Nombre;
            ViewBag.apellidosuscriptor = suscriptor.Apellido;
            ViewBag.numerosuscriptor = suscriptor.NumeroSuscriptor;
            return View();
        }

        // POST: TarjetasPago/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSuscriptor,Numero,NombreExacto,MesVencimiento,AñoVencimiento,CodigoSeguridad,TipoPeriodoEfectivo,FechaDesde,FechaHasta,TipoTarjeta")] SuscriptorVM suscriptorVm)
        {
            Suscriptor suscriptor = db.Suscriptores.Find(suscriptorVm.IdSuscriptor);
            TarjetaPago tarjetaPago=new TarjetaPago();
            tarjetaPago.Numero = suscriptorVm.Numero;
            tarjetaPago.NombreExacto = suscriptorVm.NombreExacto;
            tarjetaPago.MesVencimiento = suscriptorVm.MesVencimiento;
            tarjetaPago.AñoVencimiento = suscriptorVm.AñoVencimiento;
            tarjetaPago.CodigoSeguridad = suscriptorVm.CodigoSeguridad;
            
            tarjetaPago.TipoTarjeta = suscriptorVm.TipoTarjeta;
            db.TarjetaPagos.Add(tarjetaPago);
            db.SaveChanges();

           suscriptor.Tarjetas.Add(tarjetaPago);
            db.Entry(suscriptor).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details","Suscriptores",new{id=suscriptor.IdSuscriptor});
          

           
        }

        // GET: TarjetasPago/Edit/5
        public ActionResult Edit(int? id, int? idsuscriptor)
        {
           Suscriptor suscriptor = db.Suscriptores.Find(idsuscriptor);
                ViewBag.idsuscriptor = suscriptor.IdSuscriptor;
                ViewBag.nombresuscriptor = suscriptor.Nombre;
                ViewBag.apellidosuscriptor = suscriptor.Apellido;
                ViewBag.numerosuscriptor = suscriptor.NumeroSuscriptor;
          
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TarjetaPago tarjetaPago = db.TarjetaPagos.Find(id);
            if (tarjetaPago == null)
            {
                return HttpNotFound();
            }
            return View(tarjetaPago);
        }

        // POST: TarjetasPago/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSuscriptor,IdTarjetaPago,Numero,NombreExacto,MesVencimiento,AñoVencimiento,CodigoSeguridad,TipoPeriodoEfectivo,FechaDesde,FechaHasta,TipoTarjeta")] SuscriptorVM suscriptorVm)
        {
            Suscriptor suscriptor = db.Suscriptores.Find(suscriptorVm.IdSuscriptor);
            TarjetaPago tarjetaPago = db.TarjetaPagos.Find(suscriptorVm.IdTarjetaPago);
            tarjetaPago.Numero = suscriptorVm.Numero;
            tarjetaPago.NombreExacto = suscriptorVm.NombreExacto;
            tarjetaPago.MesVencimiento = suscriptorVm.MesVencimiento;
            tarjetaPago.AñoVencimiento = suscriptorVm.AñoVencimiento;
            tarjetaPago.CodigoSeguridad = suscriptorVm.CodigoSeguridad;
            
            tarjetaPago.TipoTarjeta = suscriptorVm.TipoTarjeta;
            db.Entry(tarjetaPago).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", "Suscriptores", new { id = suscriptor.IdSuscriptor });
        }

        // GET: TarjetasPago/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TarjetaPago tarjetaPago = db.TarjetaPagos.Find(id);
            if (tarjetaPago == null)
            {
                return HttpNotFound();
            }
            return View(tarjetaPago);
        }

        // POST: TarjetasPago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TarjetaPago tarjetaPago = db.TarjetaPagos.Find(id);
            db.TarjetaPagos.Remove(tarjetaPago);
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
