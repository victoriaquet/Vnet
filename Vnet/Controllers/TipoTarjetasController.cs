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
    public class TipoTarjetasController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: TipoTarjetas
        public ActionResult Index()
        {
            return View(db.TiposTarjetas.ToList());
        }

        // GET: TipoTarjetas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoTarjeta tipoTarjeta = db.TiposTarjetas.Find(id);
            if (tipoTarjeta == null)
            {
                return HttpNotFound();
            }
            return View(tipoTarjeta);
        }

        // GET: TipoTarjetas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoTarjetas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoTarjetaId,Descripcion")] TipoTarjeta tipoTarjeta)
        {
            if (ModelState.IsValid)
            {
                db.TiposTarjetas.Add(tipoTarjeta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoTarjeta);
        }

        // GET: TipoTarjetas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoTarjeta tipoTarjeta = db.TiposTarjetas.Find(id);
            if (tipoTarjeta == null)
            {
                return HttpNotFound();
            }
            return View(tipoTarjeta);
        }

        // POST: TipoTarjetas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoTarjetaId,Descripcion")] TipoTarjeta tipoTarjeta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoTarjeta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoTarjeta);
        }

        // GET: TipoTarjetas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoTarjeta tipoTarjeta = db.TiposTarjetas.Find(id);
            if (tipoTarjeta == null)
            {
                return HttpNotFound();
            }
            return View(tipoTarjeta);
        }

        // POST: TipoTarjetas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoTarjeta tipoTarjeta = db.TiposTarjetas.Find(id);
            db.TiposTarjetas.Remove(tipoTarjeta);
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
