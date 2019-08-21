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
    public class MotivosSuspensionesController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: MotivosSuspensiones
        public ActionResult Index()
        {
            return View(db.MotivosSuspensiones.ToList());
        }

        // GET: MotivosSuspensiones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MotivoSuspension motivoSuspension = db.MotivosSuspensiones.Find(id);
            if (motivoSuspension == null)
            {
                return HttpNotFound();
            }
            return View(motivoSuspension);
        }

        // GET: MotivosSuspensiones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MotivosSuspensiones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMotivoSuspension,Descripcion")] MotivoSuspension motivoSuspension)
        {
            if (ModelState.IsValid)
            {
                db.MotivosSuspensiones.Add(motivoSuspension);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(motivoSuspension);
        }

        // GET: MotivosSuspensiones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MotivoSuspension motivoSuspension = db.MotivosSuspensiones.Find(id);
            if (motivoSuspension == null)
            {
                return HttpNotFound();
            }
            return View(motivoSuspension);
        }

        // POST: MotivosSuspensiones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMotivoSuspension,Descripcion")] MotivoSuspension motivoSuspension)
        {
            if (ModelState.IsValid)
            {
                db.Entry(motivoSuspension).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(motivoSuspension);
        }

        // GET: MotivosSuspensiones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MotivoSuspension motivoSuspension = db.MotivosSuspensiones.Find(id);
            if (motivoSuspension == null)
            {
                return HttpNotFound();
            }
            return View(motivoSuspension);
        }

        // POST: MotivosSuspensiones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MotivoSuspension motivoSuspension = db.MotivosSuspensiones.Find(id);
            db.MotivosSuspensiones.Remove(motivoSuspension);
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
