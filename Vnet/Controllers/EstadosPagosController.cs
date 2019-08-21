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
    public class EstadosPagosController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: EstadosPagos
        public ActionResult Index()
        {
            return View(db.EstadoPagos.ToList());
        }

        // GET: EstadosPagos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoPago estadoPago = db.EstadoPagos.Find(id);
            if (estadoPago == null)
            {
                return HttpNotFound();
            }
            return View(estadoPago);
        }

        // GET: EstadosPagos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadosPagos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEstadoPago,Descripcion")] EstadoPago estadoPago)
        {
            if (ModelState.IsValid)
            {
                db.EstadoPagos.Add(estadoPago);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estadoPago);
        }

        // GET: EstadosPagos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoPago estadoPago = db.EstadoPagos.Find(id);
            if (estadoPago == null)
            {
                return HttpNotFound();
            }
            return View(estadoPago);
        }

        // POST: EstadosPagos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEstadoPago,Descripcion")] EstadoPago estadoPago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estadoPago).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estadoPago);
        }

        // GET: EstadosPagos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoPago estadoPago = db.EstadoPagos.Find(id);
            if (estadoPago == null)
            {
                return HttpNotFound();
            }
            return View(estadoPago);
        }

        // POST: EstadosPagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EstadoPago estadoPago = db.EstadoPagos.Find(id);
            db.EstadoPagos.Remove(estadoPago);
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
