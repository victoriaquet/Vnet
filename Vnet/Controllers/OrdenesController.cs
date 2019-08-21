using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SNC2017.Models;

namespace SNC2017.Controllers
{
    public class OrdenesController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: Ordenes
        public ActionResult Index()
        {
            var Ordenes = db.Ordenes.Include(o => o.Suscripcion);
            return View(Ordenes.ToList());
        }

        // GET: Ordenes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orden orden = db.Ordenes.Find(id);
            if (orden == null)
            {
                return HttpNotFound();
            }
            return View(orden);
        }

        // GET: Ordenes/Create
        public ActionResult Create()
        {
            ViewBag.IdSuscripcion = new SelectList(db.Suscripciones, "IdSuscripcion", "IdSuscripcion");
            return View();
        }

        // POST: Ordenes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdOrden,Numero,Fecha,Monto,SuscripcionInicio,SuscripcionFin,FechaLibro,NroLegalLetra,NroLegalPuntoVenta,NroLegalNumero,ReciboNroLegalLetra,ReciboNroLegalPuntoVenta,ReciboNroLegalNumero,Observaciones,IdSuscripcion,EstadoOrden")] Orden orden)
        {
            if (ModelState.IsValid)
            {
                db.Ordenes.Add(orden);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdSuscripcion = new SelectList(db.Suscripciones, "IdSuscripcion", "IdSuscripcion", orden.IdSuscripcion);
            return View(orden);
        }

        // GET: Ordenes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orden orden = db.Ordenes.Find(id);
            if (orden == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdSuscripcion = new SelectList(db.Suscripciones, "IdSuscripcion", "IdSuscripcion", orden.IdSuscripcion);
            return View(orden);
        }

        // POST: Ordenes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdOrden,Numero,Fecha,Monto,SuscripcionInicio,SuscripcionFin,FechaLibro,NroLegalLetra,NroLegalPuntoVenta,NroLegalNumero,ReciboNroLegalLetra,ReciboNroLegalPuntoVenta,ReciboNroLegalNumero,Observaciones,IdSuscripcion,EstadoOrden")] Orden orden)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orden).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdSuscripcion = new SelectList(db.Suscripciones, "IdSuscripcion", "IdSuscripcion", orden.IdSuscripcion);
            return View(orden);
        }

        // GET: Ordenes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orden orden = db.Ordenes.Find(id);
            if (orden == null)
            {
                return HttpNotFound();
            }
            return View(orden);
        }

        // POST: Ordenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orden orden = db.Ordenes.Find(id);
            db.Ordenes.Remove(orden);
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
