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
    public class VentasController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: Ventas
        public ActionResult Index()
        {
            var ventas = db.Ventas.Include(v => v.Comercio).Include(v => v.TarjetaClub);
            return View(ventas.ToList());
        }

        // GET: Ventas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Ventas.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // GET: Ventas/Create
        public ActionResult Create()
        {
            ViewBag.IdComercio = new SelectList(db.Comercios, "IdComercio", "Nombre");
            ViewBag.IdTarjetaClub = new SelectList(db.TarjetaClubes, "IdTarjetaClub", "Nombres");
            return View();
        }

        // POST: Ventas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdVenta,Descripcion,Monto,Fecha,IdComercio,IdTarjetaClub,IdUsuarioAlta,FechaAlta")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                db.Ventas.Add(venta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdComercio = new SelectList(db.Comercios, "IdComercio", "Nombre", venta.IdComercio);
            ViewBag.IdTarjetaClub = new SelectList(db.TarjetaClubes, "IdTarjetaClub", "Nombres", venta.IdTarjetaClub);
            return View(venta);
        }

        // GET: Ventas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Ventas.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdComercio = new SelectList(db.Comercios, "IdComercio", "Nombre", venta.IdComercio);
            ViewBag.IdTarjetaClub = new SelectList(db.TarjetaClubes, "IdTarjetaClub", "Nombres", venta.IdTarjetaClub);
            return View(venta);
        }

        // POST: Ventas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdVenta,Descripcion,Monto,Fecha,IdComercio,IdTarjetaClub,IdUsuarioAlta,FechaAlta")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdComercio = new SelectList(db.Comercios, "IdComercio", "Nombre", venta.IdComercio);
            ViewBag.IdTarjetaClub = new SelectList(db.TarjetaClubes, "IdTarjetaClub", "Nombres", venta.IdTarjetaClub);
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Ventas.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Venta venta = db.Ventas.Find(id);
            db.Ventas.Remove(venta);
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
