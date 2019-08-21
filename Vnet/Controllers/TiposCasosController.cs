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
    public class TiposCasosController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: TiposCasos
        public ActionResult Index()
        {
            return View(db.TipoCasos.ToList());
        }

        // GET: TiposCasos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoCaso tipoCaso = db.TipoCasos.Find(id);
            if (tipoCaso == null)
            {
                return HttpNotFound();
            }
            return View(tipoCaso);
        }

        // GET: TiposCasos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiposCasos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTipoCaso,Descripcion")] TipoCaso tipoCaso)
        {
            if (ModelState.IsValid)
            {
                db.TipoCasos.Add(tipoCaso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoCaso);
        }

        // GET: TiposCasos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoCaso tipoCaso = db.TipoCasos.Find(id);
            if (tipoCaso == null)
            {
                return HttpNotFound();
            }
            return View(tipoCaso);
        }

        // POST: TiposCasos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTipoCaso,Descripcion")] TipoCaso tipoCaso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoCaso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoCaso);
        }

        // GET: TiposCasos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoCaso tipoCaso = db.TipoCasos.Find(id);
            if (tipoCaso == null)
            {
                return HttpNotFound();
            }
            return View(tipoCaso);
        }

        // POST: TiposCasos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoCaso tipoCaso = db.TipoCasos.Find(id);
            db.TipoCasos.Remove(tipoCaso);
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
