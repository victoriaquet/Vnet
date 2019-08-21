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
    public class EstadosCasosController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: EstadosCasos
        public ActionResult Index()
        {
            return View(db.EstadoCasos.ToList());
        }

        // GET: EstadosCasos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoCaso estadoCaso = db.EstadoCasos.Find(id);
            if (estadoCaso == null)
            {
                return HttpNotFound();
            }
            return View(estadoCaso);
        }

        // GET: EstadosCasos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadosCasos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEstadoCaso,Descripcion,Color")] EstadoCaso estadoCaso)
        {
            if (ModelState.IsValid)
            {
                db.EstadoCasos.Add(estadoCaso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estadoCaso);
        }

        // GET: EstadosCasos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoCaso estadoCaso = db.EstadoCasos.Find(id);
            if (estadoCaso == null)
            {
                return HttpNotFound();
            }
            return View(estadoCaso);
        }

        // POST: EstadosCasos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEstadoCaso,Descripcion,Color")] EstadoCaso estadoCaso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estadoCaso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estadoCaso);
        }

        // GET: EstadosCasos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoCaso estadoCaso = db.EstadoCasos.Find(id);
            if (estadoCaso == null)
            {
                return HttpNotFound();
            }
            return View(estadoCaso);
        }

        // POST: EstadosCasos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EstadoCaso estadoCaso = db.EstadoCasos.Find(id);
            db.EstadoCasos.Remove(estadoCaso);
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
