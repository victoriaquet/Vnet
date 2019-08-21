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
    public class MotivoCierreCasosController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: MotivoCierreCasos
        public ActionResult Index()
        {
            return View(db.MotivoCierreCasos.ToList());
        }

        // GET: MotivoCierreCasos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MotivoCierreCaso motivoCierreCaso = db.MotivoCierreCasos.Find(id);
            if (motivoCierreCaso == null)
            {
                return HttpNotFound();
            }
            return View(motivoCierreCaso);
        }

        // GET: MotivoCierreCasos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MotivoCierreCasos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMotivoCierreCaso,Descripcion")] MotivoCierreCaso motivoCierreCaso)
        {
            if (ModelState.IsValid)
            {
                db.MotivoCierreCasos.Add(motivoCierreCaso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(motivoCierreCaso);
        }

        // GET: MotivoCierreCasos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MotivoCierreCaso motivoCierreCaso = db.MotivoCierreCasos.Find(id);
            if (motivoCierreCaso == null)
            {
                return HttpNotFound();
            }
            return View(motivoCierreCaso);
        }

        // POST: MotivoCierreCasos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMotivoCierreCaso,Descripcion")] MotivoCierreCaso motivoCierreCaso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(motivoCierreCaso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(motivoCierreCaso);
        }

        // GET: MotivoCierreCasos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MotivoCierreCaso motivoCierreCaso = db.MotivoCierreCasos.Find(id);
            if (motivoCierreCaso == null)
            {
                return HttpNotFound();
            }
            return View(motivoCierreCaso);
        }

        // POST: MotivoCierreCasos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MotivoCierreCaso motivoCierreCaso = db.MotivoCierreCasos.Find(id);
            db.MotivoCierreCasos.Remove(motivoCierreCaso);
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
