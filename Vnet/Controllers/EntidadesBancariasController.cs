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
    public class EntidadesBancariasController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: EntidadesBancarias
        public ActionResult Index()
        {
            return View(db.EntidadBancarias.ToList());
        }

        // GET: EntidadesBancarias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntidadBancaria entidadBancaria = db.EntidadBancarias.Find(id);
            if (entidadBancaria == null)
            {
                return HttpNotFound();
            }
            return View(entidadBancaria);
        }

        // GET: EntidadesBancarias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EntidadesBancarias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEntidadBancaria,Descripcion")] EntidadBancaria entidadBancaria)
        {
            if (ModelState.IsValid)
            {
                db.EntidadBancarias.Add(entidadBancaria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(entidadBancaria);
        }

        // GET: EntidadesBancarias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntidadBancaria entidadBancaria = db.EntidadBancarias.Find(id);
            if (entidadBancaria == null)
            {
                return HttpNotFound();
            }
            return View(entidadBancaria);
        }

        // POST: EntidadesBancarias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEntidadBancaria,Descripcion")] EntidadBancaria entidadBancaria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entidadBancaria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(entidadBancaria);
        }

        // GET: EntidadesBancarias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntidadBancaria entidadBancaria = db.EntidadBancarias.Find(id);
            if (entidadBancaria == null)
            {
                return HttpNotFound();
            }
            return View(entidadBancaria);
        }

        // POST: EntidadesBancarias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EntidadBancaria entidadBancaria = db.EntidadBancarias.Find(id);
            db.EntidadBancarias.Remove(entidadBancaria);
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
