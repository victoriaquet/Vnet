using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SNC2017.Models;

namespace SNC2017.Controllers
{
    public class CanillitasController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: Canillitas
        public ActionResult Index()
        {
            if(User.IsInRole("Distribuidor")) {return View(db.Canillitas.ToList().FindAll(x=>x.Distribuidor.IdDistribuidor==Int32.Parse(db.Users.ToList().Find(z=>z.Id== User.Identity.GetUserId()).Identificador)));}
            return View(db.Canillitas.ToList());
        }

        // GET: Canillitas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Canillita canillita = db.Canillitas.Find(id);
            if (canillita == null)
            {
                return HttpNotFound();
            }
            return View(canillita);
        }

        // GET: Canillitas/Create
        public ActionResult Create()
        {
            if (User.IsInRole("Distribuidor"))
            {
                    ViewBag.Distribuidores = db.Distribuidores.ToList().FindAll(x => x.IdDistribuidor == Int32.Parse(db.Users.ToList().Find(z => z.Id == User.Identity.GetUserId()).Identificador));
            }
            else
            {
                ViewBag.Distribuidores = db.Distribuidores.ToList();

            }

            return View();
        }

        // POST: Canillitas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCanillita,NroCanillita,Nombre,Apellido,NroTel,Activo,IdDistribuidor")] Canillita canillita)
        {
            canillita.Activo = true;
            if (ModelState.IsValid)
            {
                if (db.Canillitas.FirstOrDefault(x => x.NroCanillita == canillita.NroCanillita) != null)
                {
                    ModelState.AddModelError("NroCanillita","Número existente.");
                }
                else
                {
                    db.Canillitas.Add(canillita);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }

            ViewBag.Distribuidores = db.Distribuidores.ToList();
            return View(canillita);
        }

        // GET: Canillitas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Canillita canillita = db.Canillitas.Find(id);
            if (canillita == null)
            {
                return HttpNotFound();
            }
            if (User.IsInRole("Distribuidor"))
            {
                ViewBag.Distribuidores = db.Distribuidores.ToList().FindAll(x => x.IdDistribuidor == Int32.Parse(db.Users.ToList().Find(z => z.Id == User.Identity.GetUserId()).Identificador));
            }
            else
            {
                ViewBag.Distribuidores = db.Distribuidores.ToList();

            }
            return View(canillita);
        }

        // POST: Canillitas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCanillita,NroCanillita,Nombre,Apellido,NroTel,Activo,IdDistribuidor")] Canillita canillita)
        {
            if (ModelState.IsValid)
            {
                if (db.Canillitas.FirstOrDefault(x => x.NroCanillita == canillita.NroCanillita && x.IdCanillita!=canillita.IdCanillita) != null)
                {
                    ModelState.AddModelError("NroCanillita", "Número existente.");
                }
                else
                {
                    db.Entry(canillita).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }
            ViewBag.Distribuidores = db.Distribuidores.ToList();

            return View(canillita);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsignarCanillita(int idcanillita,int idsuscripcion)
        {
           Suscripcion suscripcion=new Suscripcion();
           suscripcion = db.Suscripciones.Find(idsuscripcion);
           var idcanillitaanterior = suscripcion.IdCanillita;
           suscripcion.IdCanillita = idcanillita;
           db.Entry(suscripcion).State = EntityState.Modified;
           db.SaveChanges();
            foreach (var de in suscripcion.DiaEntregas)
            {
                if (de.IdCanillita == null)
                {
                    de.IdCanillita = idcanillita;
                    db.Entry(de).State = EntityState.Modified;
                }
                else
                {
                    //En caso de que el canilltia del día de entrega coincida con el canillita anterior se lo cambia por el nuevo
                    if (de.IdCanillita == idcanillitaanterior)
                    {
                        de.IdCanillita = idcanillita;
                        db.Entry(de).State = EntityState.Modified;
                    }
                }

               
                db.SaveChanges();
            }

           return RedirectToAction("Details","Suscripciones",new {id=suscripcion.IdSuscripcion});
           
        }
        [HttpPost]
        public ActionResult CanillitaPersonalizado(int iddiaentrega,int idcanillita)
        {
            try
            {
                 var diaentrega = db.DiaEntregas.Find(iddiaentrega);
            diaentrega.IdCanillita = idcanillita;
            db.Entry(diaentrega).State = EntityState.Modified;
            db.SaveChanges();
;            //May be you want to pass the posted model to the parial view?
                return Json(new{success = true}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);

            }

        }


        // GET: Canillitas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Canillita canillita = db.Canillitas.Find(id);
            if (canillita == null)
            {
                return HttpNotFound();
            }
            return View(canillita);
        }

        // POST: Canillitas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Canillita canillita = db.Canillitas.Find(id);
            db.Canillitas.Remove(canillita);
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
