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
    public class EstadosSuscripcionHistorialController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: EstadosSuscripcionHistorial
        public ActionResult Index()
        {
            return View(db.EstadoSuscripcionHistoriales.ToList());
        }

        // GET: EstadosSuscripcionHistorial/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoSuscripcionHistorial estadoSuscripcionHistorial = db.EstadoSuscripcionHistoriales.Find(id);
            if (estadoSuscripcionHistorial == null)
            {
                return HttpNotFound();
            }
            return View(estadoSuscripcionHistorial);
        }

        // GET: EstadosSuscripcionHistorial/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadosSuscripcionHistorial/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEstadoSuscripcionHistorial,FechaModificacion,Observaciones")] EstadoSuscripcionHistorial estadoSuscripcionHistorial)
        {
            if (ModelState.IsValid)
            {
                db.EstadoSuscripcionHistoriales.Add(estadoSuscripcionHistorial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estadoSuscripcionHistorial);
        }

        // GET: EstadosSuscripcionHistorial/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoSuscripcionHistorial estadoSuscripcionHistorial = db.EstadoSuscripcionHistoriales.Find(id);
            if (estadoSuscripcionHistorial == null)
            {
                return HttpNotFound();
            }
            return View(estadoSuscripcionHistorial);
        }

        // POST: EstadosSuscripcionHistorial/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEstadoSuscripcionHistorial,FechaModificacion,Observaciones")] EstadoSuscripcionHistorial estadoSuscripcionHistorial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estadoSuscripcionHistorial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estadoSuscripcionHistorial);
        }

        // GET: EstadosSuscripcionHistorial/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoSuscripcionHistorial estadoSuscripcionHistorial = db.EstadoSuscripcionHistoriales.Find(id);
            if (estadoSuscripcionHistorial == null)
            {
                return HttpNotFound();
            }
            return View(estadoSuscripcionHistorial);
        }

        // POST: EstadosSuscripcionHistorial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EstadoSuscripcionHistorial estadoSuscripcionHistorial = db.EstadoSuscripcionHistoriales.Find(id);
            db.EstadoSuscripcionHistoriales.Remove(estadoSuscripcionHistorial);
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
