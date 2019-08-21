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
    public class HistorialPrecioOfertasController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: HistorialPrecioOfertas
        public ActionResult Index()
        {
            var historialPrecioOfertas = db.HistorialPrecioOfertas.Include(h => h.Oferta);
            return View(historialPrecioOfertas.ToList());
        }

        // GET: HistorialPrecioOfertas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistorialPrecioOferta historialPrecioOferta = db.HistorialPrecioOfertas.Find(id);
            if (historialPrecioOferta == null)
            {
                return HttpNotFound();
            }
            return View(historialPrecioOferta);
        }

        // GET: HistorialPrecioOfertas/Create
        public ActionResult Create(int idoferta, string msg)
        {
            ViewBag.msg = "mensaje";
            
            Oferta oferta = db.Ofertas.Find(idoferta);
            if (msg!=null)
            {
                ViewBag.msg = msg;
            }

            return View(oferta);

        }

        // POST: HistorialPrecioOfertas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Precio,FechaInicio,IdOferta")] HistorialPrecioOferta historialPrecioOferta)
        {
            if (ModelState.IsValid)
            {
                historialPrecioOferta.FechaModificacion=DateTime.Now;
                db.HistorialPrecioOfertas.Add(historialPrecioOferta);
                db.SaveChanges();
                Oferta oferta= new Oferta();
                oferta = db.Ofertas.Find(historialPrecioOferta.IdOferta);
                oferta.Precio = historialPrecioOferta.Precio;
                oferta.FechaInicio = historialPrecioOferta.FechaInicio;
                db.Entry(oferta).State = EntityState.Modified;
                db.SaveChanges();
            

                return RedirectToAction("Create","HistorialPrecioOfertas", new{idoferta=historialPrecioOferta.IdOferta, msg="Precio actualizado: $ "+historialPrecioOferta.Precio});
            }

            return View(historialPrecioOferta.Oferta);
        }

        // GET: HistorialPrecioOfertas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistorialPrecioOferta historialPrecioOferta = db.HistorialPrecioOfertas.Find(id);
            if (historialPrecioOferta == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdOferta = new SelectList(db.Ofertas, "IdOferta", "Nombre", historialPrecioOferta.IdOferta);
            return View(historialPrecioOferta);
        }

        // POST: HistorialPrecioOfertas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdHistorialPrecioOferta,Precio,FechaInicio,IdOferta")] HistorialPrecioOferta historialPrecioOferta)
        {
            historialPrecioOferta.FechaModificacion=DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Entry(historialPrecioOferta).State = EntityState.Modified;
                db.SaveChanges();
                Oferta oferta = new Oferta();
                oferta = db.Ofertas.Find(historialPrecioOferta.IdOferta);
                oferta.Precio = historialPrecioOferta.Precio;
                oferta.FechaInicio = historialPrecioOferta.FechaInicio;
                db.Entry(oferta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Create", "HistorialPrecioOfertas", new { idoferta = historialPrecioOferta.IdOferta, msg = "Edición correcta: $ " + historialPrecioOferta.Precio });
            }
            return View(historialPrecioOferta);
        }

        // GET: HistorialPrecioOfertas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistorialPrecioOferta historialPrecioOferta = db.HistorialPrecioOfertas.Find(id);
            if (historialPrecioOferta == null)
            {
                return HttpNotFound();
            }
            return View(historialPrecioOferta);
        }

        // POST: HistorialPrecioOfertas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HistorialPrecioOferta historialPrecioOferta = db.HistorialPrecioOfertas.Find(id);
            db.HistorialPrecioOfertas.Remove(historialPrecioOferta);
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
