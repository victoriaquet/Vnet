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
    public class AreaCasosController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: AreaCasos
        public ActionResult Index()
        {
            return View(db.AreaCasos.ToList());
        }

        // GET: AreaCasos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaCaso areaCaso = db.AreaCasos.Find(id);
            if (areaCaso == null)
            {
                return HttpNotFound();
            }
            return View(areaCaso);
        }

        // GET: AreaCasos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AreaCasos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAreaCaso,Descripcion")] AreaCaso areaCaso)
        {
            if (ModelState.IsValid)
            {
                db.AreaCasos.Add(areaCaso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(areaCaso);
        }

        // GET: AreaCasos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaCaso areaCaso = db.AreaCasos.Find(id);
            if (areaCaso == null)
            {
                return HttpNotFound();
            }
            return View(areaCaso);
        }

        // POST: AreaCasos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAreaCaso,Descripcion")] AreaCaso areaCaso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(areaCaso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(areaCaso);
        }

        // GET: AreaCasos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaCaso areaCaso = db.AreaCasos.Find(id);
            if (areaCaso == null)
            {
                return HttpNotFound();
            }
            return View(areaCaso);
        }

        // POST: AreaCasos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AreaCaso areaCaso = db.AreaCasos.Find(id);
            db.AreaCasos.Remove(areaCaso);
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
