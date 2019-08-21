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
    public class DistribuidoresController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: Distribuidores
        public ActionResult Index()
        {
            return View(db.Distribuidores.ToList());
        }

        // GET: Distribuidores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Distribuidor distribuidor = db.Distribuidores.Find(id);
            if (distribuidor == null)
            {
                return HttpNotFound();
            }
            return View(distribuidor);
        }

        // GET: Distribuidores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Distribuidores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDistribuidor,Codigo,Nombre")] Distribuidor distribuidor)
        {
            if (ModelState.IsValid)
            {
                db.Distribuidores.Add(distribuidor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(distribuidor);
        }

        // GET: Distribuidores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Distribuidor distribuidor = db.Distribuidores.Find(id);
            if (distribuidor == null)
            {
                return HttpNotFound();
            }
            return View(distribuidor);
        }

        // POST: Distribuidores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDistribuidor,Codigo,Nombre")] Distribuidor distribuidor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(distribuidor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(distribuidor);
        }

        // GET: Distribuidores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Distribuidor distribuidor = db.Distribuidores.Find(id);
            if (distribuidor == null)
            {
                return HttpNotFound();
            }
            return View(distribuidor);
        }

        // POST: Distribuidores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Distribuidor distribuidor = db.Distribuidores.Find(id);
            db.Distribuidores.Remove(distribuidor);
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
