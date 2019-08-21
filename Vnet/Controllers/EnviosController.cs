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
    public class EnviosController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: Envios
        public ActionResult Index()
        {
            return View(db.Envios.ToList());
        }

        // GET: Envios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Envio envio = db.Envios.Find(id);
            if (envio == null)
            {
                return HttpNotFound();
            }
            return View(envio);
        }

        // GET: Envios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Envios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEnvio,FechaEnvio")] Envio envio)
        {
            if (ModelState.IsValid)
            {
                db.Envios.Add(envio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(envio);
        }

        // GET: Envios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Envio envio = db.Envios.Find(id);
            if (envio == null)
            {
                return HttpNotFound();
            }
            return View(envio);
        }

        // POST: Envios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEnvio,FechaEnvio")] Envio envio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(envio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(envio);
        }

        // GET: Envios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Envio envio = db.Envios.Find(id);
            if (envio == null)
            {
                return HttpNotFound();
            }
            return View(envio);
        }

        // POST: Envios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Envio envio = db.Envios.Find(id);
            db.Envios.Remove(envio);
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
