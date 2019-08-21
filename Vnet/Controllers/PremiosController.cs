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
    public class PremiosController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: Premios
        public ActionResult Index()
        {
            var premios = db.Premios.Include(p => p.CatalogoPremio).Include(p => p.Suscriptor);
            return View(premios.ToList());
        }

        // GET: Premios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Premio premio = db.Premios.Find(id);
            if (premio == null)
            {
                return HttpNotFound();
            }
            return View(premio);
        }

        // GET: Premios/Create
        public ActionResult Create()
        {
            ViewBag.IdCatalogoPremio = new SelectList(db.CatalogoPremios, "IdCatalogoPremio", "Descripcion");
            ViewBag.IdSuscriptor = new SelectList(db.Suscriptores, "IdSuscriptor", "Nombre");
            return View();
        }

        // POST: Premios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPremio,FechaPremio,IdSuscriptor,IdCatalogoPremio")] Premio premio)
        {
            if (ModelState.IsValid)
            {
                db.Premios.Add(premio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCatalogoPremio = new SelectList(db.CatalogoPremios, "IdCatalogoPremio", "Descripcion", premio.IdCatalogoPremio);
            ViewBag.IdSuscriptor = new SelectList(db.Suscriptores, "IdSuscriptor", "Nombre", premio.IdSuscriptor);
            return View(premio);
        }

        // GET: Premios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Premio premio = db.Premios.Find(id);
            if (premio == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCatalogoPremio = new SelectList(db.CatalogoPremios, "IdCatalogoPremio", "Descripcion", premio.IdCatalogoPremio);
            ViewBag.IdSuscriptor = new SelectList(db.Suscriptores, "IdSuscriptor", "Nombre", premio.IdSuscriptor);
            return View(premio);
        }

        // POST: Premios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPremio,FechaPremio,IdSuscriptor,IdCatalogoPremio")] Premio premio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(premio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCatalogoPremio = new SelectList(db.CatalogoPremios, "IdCatalogoPremio", "Descripcion", premio.IdCatalogoPremio);
            ViewBag.IdSuscriptor = new SelectList(db.Suscriptores, "IdSuscriptor", "Nombre", premio.IdSuscriptor);
            return View(premio);
        }

        // GET: Premios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Premio premio = db.Premios.Find(id);
            if (premio == null)
            {
                return HttpNotFound();
            }
            return View(premio);
        }

        // POST: Premios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Premio premio = db.Premios.Find(id);
            db.Premios.Remove(premio);
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
