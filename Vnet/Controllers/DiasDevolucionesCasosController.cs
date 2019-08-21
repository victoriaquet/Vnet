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
    public class DiasDevolucionesCasosController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: DiasDevolucionesCasos
        public ActionResult Index()
        {
            
            return View();
        }

        // GET: DiasDevolucionesCasos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiaDevolucionCaso diaDevolucionCaso = db.DiaDevolucionCasos.Find(id);
            if (diaDevolucionCaso == null)
            {
                return HttpNotFound();
            }
            return View(diaDevolucionCaso);
        }

        // GET: DiasDevolucionesCasos/Create
        public ActionResult Create()
        {
            ViewBag.IdCaso = new SelectList(db.Casos, "IdCaso", "Descripcion");
            return View();
        }

        // POST: DiasDevolucionesCasos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDiaDevolucionCaso,Fecha,Observaciones,FechaModificacion,IdCaso")] DiaDevolucionCaso diaDevolucionCaso)
        {
            if (ModelState.IsValid)
            {
                db.DiaDevolucionCasos.Add(diaDevolucionCaso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

 
            return View(diaDevolucionCaso);
        }

        // GET: DiasDevolucionesCasos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiaDevolucionCaso diaDevolucionCaso = db.DiaDevolucionCasos.Find(id);
            if (diaDevolucionCaso == null)
            {
                return HttpNotFound();
            }
    
            return View(diaDevolucionCaso);
        }

        // POST: DiasDevolucionesCasos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDiaDevolucionCaso,Fecha,Observaciones,FechaModificacion,IdCaso")] DiaDevolucionCaso diaDevolucionCaso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diaDevolucionCaso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
     
            return View(diaDevolucionCaso);
        }

        // GET: DiasDevolucionesCasos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiaDevolucionCaso diaDevolucionCaso = db.DiaDevolucionCasos.Find(id);
            if (diaDevolucionCaso == null)
            {
                return HttpNotFound();
            }
            return View(diaDevolucionCaso);
        }

        // POST: DiasDevolucionesCasos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiaDevolucionCaso diaDevolucionCaso = db.DiaDevolucionCasos.Find(id);
            db.DiaDevolucionCasos.Remove(diaDevolucionCaso);
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
