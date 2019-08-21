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
    public class TipoMedioPagosController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: TipoMedioPagos
        public ActionResult Index()
        {
            return View(db.TipoMedioPagos.ToList());
        }

        // GET: TipoMedioPagos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoMedioPago tipoMedioPago = db.TipoMedioPagos.Find(id);
            if (tipoMedioPago == null)
            {
                return HttpNotFound();
            }
            return View(tipoMedioPago);
        }

        // GET: TipoMedioPagos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoMedioPagos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoMedioPagoId,Descripcion,GeneraArchivo")] TipoMedioPago tipoMedioPago)
        {
            if (ModelState.IsValid)
            {
                db.TipoMedioPagos.Add(tipoMedioPago);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoMedioPago);
        }

        // GET: TipoMedioPagos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoMedioPago tipoMedioPago = db.TipoMedioPagos.Find(id);
            if (tipoMedioPago == null)
            {
                return HttpNotFound();
            }
            return View(tipoMedioPago);
        }

        // POST: TipoMedioPagos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoMedioPagoId,Descripcion,GeneraArchivo")] TipoMedioPago tipoMedioPago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoMedioPago).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoMedioPago);
        }

        // GET: TipoMedioPagos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoMedioPago tipoMedioPago = db.TipoMedioPagos.Find(id);
            if (tipoMedioPago == null)
            {
                return HttpNotFound();
            }
            return View(tipoMedioPago);
        }

        // POST: TipoMedioPagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoMedioPago tipoMedioPago = db.TipoMedioPagos.Find(id);
            db.TipoMedioPagos.Remove(tipoMedioPago);
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
