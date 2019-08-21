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
    public class EstadosOfertasController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: EstadosOfertas
        public ActionResult Index()
        {
            return View(db.EstadoOfertas.ToList());
        }

        // GET: EstadosOfertas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoOferta estadoOferta = db.EstadoOfertas.Find(id);
            if (estadoOferta == null)
            {
                return HttpNotFound();
            }
            return View(estadoOferta);
        }

        // GET: EstadosOfertas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadosOfertas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEstadoOferta,Descripcion")] EstadoOferta estadoOferta)
        {
            if (ModelState.IsValid)
            {
                db.EstadoOfertas.Add(estadoOferta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estadoOferta);
        }

        // GET: EstadosOfertas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoOferta estadoOferta = db.EstadoOfertas.Find(id);
            if (estadoOferta == null)
            {
                return HttpNotFound();
            }
            return View(estadoOferta);
        }

        // POST: EstadosOfertas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEstadoOferta,Descripcion")] EstadoOferta estadoOferta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estadoOferta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estadoOferta);
        }

        // GET: EstadosOfertas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoOferta estadoOferta = db.EstadoOfertas.Find(id);
            if (estadoOferta == null)
            {
                return HttpNotFound();
            }
            return View(estadoOferta);
        }

        // POST: EstadosOfertas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EstadoOferta estadoOferta = db.EstadoOfertas.Find(id);
            db.EstadoOfertas.Remove(estadoOferta);
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
