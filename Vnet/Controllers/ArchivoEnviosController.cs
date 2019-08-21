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
    public class ArchivoEnviosController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: ArchivoEnvios
        public ActionResult Index()
        {
            return View(db.ArchivoEnvios.ToList());
        }

        // GET: ArchivoEnvios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArchivoEnvio archivoEnvio = db.ArchivoEnvios.Find(id);
            if (archivoEnvio == null)
            {
                return HttpNotFound();
            }
            return View(archivoEnvio);
        }

        // GET: ArchivoEnvios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArchivoEnvios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdArchivoEnvio,numero,Fecha,FechaCreacion,FechaRespuesta,Archivo,CantidadRegistros,ImporteTotal")] ArchivoEnvio archivoEnvio)
        {
            if (ModelState.IsValid)
            {
                db.ArchivoEnvios.Add(archivoEnvio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(archivoEnvio);
        }

        // GET: ArchivoEnvios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArchivoEnvio archivoEnvio = db.ArchivoEnvios.Find(id);
            if (archivoEnvio == null)
            {
                return HttpNotFound();
            }
            return View(archivoEnvio);
        }

        // POST: ArchivoEnvios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdArchivoEnvio,numero,Fecha,FechaCreacion,FechaRespuesta,Archivo,CantidadRegistros,ImporteTotal")] ArchivoEnvio archivoEnvio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(archivoEnvio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(archivoEnvio);
        }

        // GET: ArchivoEnvios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArchivoEnvio archivoEnvio = db.ArchivoEnvios.Find(id);
            if (archivoEnvio == null)
            {
                return HttpNotFound();
            }
            return View(archivoEnvio);
        }

        // POST: ArchivoEnvios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArchivoEnvio archivoEnvio = db.ArchivoEnvios.Find(id);
            db.ArchivoEnvios.Remove(archivoEnvio);
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
