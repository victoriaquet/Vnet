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
    public class LocalidadesController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: Localidades
        public ActionResult Index()
        {
            var Localidades = db.Localidades.Include(l => l.Provincia);
            return View(Localidades.ToList());
        }

        // GET: Localidades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localidad localidad = db.Localidades.Find(id);
            if (localidad == null)
            {
                return HttpNotFound();
            }
            return View(localidad);
        }

        // GET: Localidades/Create
        public ActionResult Create()
        {
            ViewBag.IdProvincia = new SelectList(db.Provincias, "IdProvincia", "Nombre");
            return View();
        }

        // POST: Localidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdLocalidad,Nombre,CodPostal,IdProvincia")] Localidad localidad)
        {
            if (ModelState.IsValid)
            {
                db.Localidades.Add(localidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProvincia = new SelectList(db.Provincias, "IdProvincia", "Nombre", localidad.IdProvincia);
            return View(localidad);
        }

        // GET: Localidades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localidad localidad = db.Localidades.Find(id);
            if (localidad == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProvincia = new SelectList(db.Provincias, "IdProvincia", "Nombre", localidad.IdProvincia);
            return View(localidad);
        }

        // POST: Localidades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdLocalidad,Nombre,CodPostal,IdProvincia")] Localidad localidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(localidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProvincia = new SelectList(db.Provincias, "IdProvincia", "Nombre", localidad.IdProvincia);
            return View(localidad);
        }

        // GET: Localidades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localidad localidad = db.Localidades.Find(id);
            if (localidad == null)
            {
                return HttpNotFound();
            }
            return View(localidad);
        }

        // POST: Localidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Localidad localidad = db.Localidades.Find(id);
            db.Localidades.Remove(localidad);
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
