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
    public class CatalogoPremiosController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: CatalogoPremios
        public ActionResult Index()
        {
            return View(db.CatalogoPremios.ToList());
        }

        // GET: CatalogoPremios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CatalogoPremio catalogoPremio = db.CatalogoPremios.Find(id);
            if (catalogoPremio == null)
            {
                return HttpNotFound();
            }
            return View(catalogoPremio);
        }

        // GET: CatalogoPremios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CatalogoPremios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCatalogoPremio,Descripcion,FechaCarga,FechaBaja")] CatalogoPremio catalogoPremio)
        {
            if (ModelState.IsValid)
            {
                db.CatalogoPremios.Add(catalogoPremio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(catalogoPremio);
        }

        // GET: CatalogoPremios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CatalogoPremio catalogoPremio = db.CatalogoPremios.Find(id);
            if (catalogoPremio == null)
            {
                return HttpNotFound();
            }
            return View(catalogoPremio);
        }

        // POST: CatalogoPremios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCatalogoPremio,Descripcion,FechaCarga,FechaBaja")] CatalogoPremio catalogoPremio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(catalogoPremio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(catalogoPremio);
        }

        // GET: CatalogoPremios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CatalogoPremio catalogoPremio = db.CatalogoPremios.Find(id);
            if (catalogoPremio == null)
            {
                return HttpNotFound();
            }
            return View(catalogoPremio);
        }

        // POST: CatalogoPremios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CatalogoPremio catalogoPremio = db.CatalogoPremios.Find(id);
            db.CatalogoPremios.Remove(catalogoPremio);
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
