﻿using System;
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
    public class AsuntoCasosController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: AsuntoCasos
        public ActionResult Index()
        {
            var asuntoCasos = db.AsuntoCasos.Include(a => a.AreaCaso);
            return View(asuntoCasos.ToList());
        }

        // GET: AsuntoCasos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsuntoCaso asuntoCaso = db.AsuntoCasos.Find(id);
            if (asuntoCaso == null)
            {
                return HttpNotFound();
            }
            return View(asuntoCaso);
        }

        // GET: AsuntoCasos/Create
        public ActionResult Create()
        {
            ViewBag.IdAreaCaso = new SelectList(db.AreaCasos, "IdAreaCaso", "Descripcion");
            return View();
        }

        // POST: AsuntoCasos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAsuntoCaso,Descripcion,IdAreaCaso")] AsuntoCaso asuntoCaso)
        {
            if (ModelState.IsValid)
            {
                db.AsuntoCasos.Add(asuntoCaso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAreaCaso = new SelectList(db.AreaCasos, "IdAreaCaso", "Descripcion", asuntoCaso.IdAreaCaso);
            return View(asuntoCaso);
        }

        // GET: AsuntoCasos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsuntoCaso asuntoCaso = db.AsuntoCasos.Find(id);
            if (asuntoCaso == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAreaCaso = new SelectList(db.AreaCasos, "IdAreaCaso", "Descripcion", asuntoCaso.IdAreaCaso);
            return View(asuntoCaso);
        }

        // POST: AsuntoCasos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAsuntoCaso,Descripcion,IdAreaCaso")] AsuntoCaso asuntoCaso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asuntoCaso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAreaCaso = new SelectList(db.AreaCasos, "IdAreaCaso", "Descripcion", asuntoCaso.IdAreaCaso);
            return View(asuntoCaso);
        }

        // GET: AsuntoCasos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsuntoCaso asuntoCaso = db.AsuntoCasos.Find(id);
            if (asuntoCaso == null)
            {
                return HttpNotFound();
            }
            return View(asuntoCaso);
        }

        // POST: AsuntoCasos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AsuntoCaso asuntoCaso = db.AsuntoCasos.Find(id);
            //AreaCaso areaCaso = db.AreaCasos.Find(asuntoCaso.IdAreaCaso);
            db.AsuntoCasos.Remove(asuntoCaso);
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