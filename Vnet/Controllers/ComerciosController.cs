using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity.EntityFramework;
//using OfficeOpenXml;
using Vnet.Models;

namespace SNC2017.Controllers
{
    public class ComerciosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        [HttpPost]
        public ActionResult GenerarVenta(string descripcion, decimal monto, int idtc)
        {
            if (descripcion.IsNullOrWhiteSpace()) descripcion = "Sin especificar";
            try
            {
                var venta = new Venta();
                //Datos Generales
                venta.Descripcion = descripcion;
                venta.Monto = monto;
                venta.FechaAlta = DateTime.Now;
                venta.Fecha = DateTime.Now;

                //Relaciones
                venta.IdTarjetaClub = idtc;
                venta.IdComercio = db.Comercios.ToList().First(x => x.Id == User.Identity.GetUserId()).IdComercio;
                venta.IdUsuarioAlta = User.Identity.GetUserId();
                db.Ventas.Add(venta);
                db.SaveChanges();
                return Json(
                    new
                    {
                        success = true,
                        message = "Venta almacenada correctamente.<br>Descripción: " + descripcion + "<br>Monto: $" +
                                  monto
                    }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
            }

            return Json(new {success = false, message = "Inténtelo nuevamente."}, JsonRequestBehavior.AllowGet);
        }

        // GET: Comercios
        public ActionResult Index()
        {
            return View(db.Comercios.ToList());
        }

        // GET: Comercios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comercio comercio = db.Comercios.Find(id);
            if (comercio == null)
            {
                return HttpNotFound();
            }

            return View(comercio);
        }

        // GET: Comercios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comercios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nombre,Email,AdhesionActiva")]
            Comercio comercio)
        {
            bool valid = true;
            if (db.Comercios.ToList().FirstOrDefault(x => x.Email == comercio.Email) != null)
            {
                ModelState.AddModelError(string.Empty, "Email existente.");
                valid = false;
            }

            if (ModelState.IsValid | valid)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var user = new ApplicationUser {UserName = comercio.Email, Email = comercio.Email};
                var result = userManager.Create(user, "SNorteClub!");
                if (result.Succeeded)
                {
                    comercio.Id = user.Id;
                    comercio.FechaAdhesion = DateTime.Now;
                    comercio.FechaAlta = DateTime.Now;
                    comercio.IdUsuarioAlta = User.Identity.GetUserId();
                    userManager.AddToRole(user.Id, "Comercio");
                    db.Comercios.Add(comercio);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Comercios");
                }
            }

            return View(comercio);
        }

        // GET: Comercios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comercio comercio = db.Comercios.Find(id);
            if (comercio == null)
            {
                return HttpNotFound();
            }

            return View(comercio);
        }

        // POST: Comercios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdComercio,Nombre,Email,AdhesionActiva")]
            Comercio comercio)
        {
            #region IniVar

            bool valid = true;
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var com = db.Comercios.Find(comercio.IdComercio);

            var verifcom = db.Comercios.ToList()
                .FirstOrDefault(x => x.Email == comercio.Email & x.IdComercio != com.IdComercio);
            var verifuser = userManager.Users.ToList().FirstOrDefault(x => x.Email == comercio.Email & x.Id != com.Id);

            #endregion

            #region Validaciones

            if (verifcom != null | verifuser != null)
            {
                ModelState.AddModelError(string.Empty, "Email existente.");
                valid = false;
            }

            if (ModelState.IsValid & valid)
            {
                com.Nombre = comercio.Nombre;
                com.AdhesionActiva = comercio.AdhesionActiva;
                if (com.Email != comercio.Email)
                {
                    com.Email = comercio.Email;
                    var user = userManager.FindById(com.Id);
                    user.Email = com.Email;
                    user.UserName = com.Email;
                    userManager.Update(user);
                }

                db.Entry(com).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            #endregion

            return View(comercio);
        }

        // GET: Comercios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comercio comercio = db.Comercios.Find(id);
            if (comercio == null)
            {
                return HttpNotFound();
            }

            return View(comercio);
        }

        // POST: Comercios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comercio comercio = db.Comercios.Find(id);
            db.Comercios.Remove(comercio);
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

        //[Authorize(Roles = "Administrador")]
        //public void AltaComerciosExcel(string path)
        //{
        //    if (path.IsNullOrWhiteSpace())
        //    {
        //        path = @"C:\\CorreosComercios\correoscomercios.xlsx";
        //    }

        //    using (ExcelPackage xlPackage =
        //        new ExcelPackage(new FileInfo(
        //            path)))
        //    {
        //        var myWorksheet = xlPackage.Workbook.Worksheets[1]; //select sheet here
        //        var totalRows = myWorksheet.Dimension.End.Row;
        //        var totalColumns = myWorksheet.Dimension.End.Column;
        //        var idAdmin = db.Users.FirstOrDefault(x => x.Email == "admin@diarionorte.com").Id;
        //        for (int rowNum = 2; rowNum <= 272; rowNum++) //selet starting row here
        //        {
        //            int intFTP = 0; //int for try parse
        //            var nuevocomercio = new Comercio();
        //            var cells = myWorksheet.Cells[rowNum, 1, rowNum, totalColumns];
        //            nuevocomercio.Nombre = cells.ElementAtOrDefault(0).GetValue<string>();
        //            nuevocomercio.FechaAdhesion = DateTime.Now;
        //            nuevocomercio.AdhesionActiva = true;
        //            nuevocomercio.Email = String.IsNullOrEmpty(cells.ElementAtOrDefault(3).GetValue<string>())?"sincorreo@diarionorte.com": cells.ElementAtOrDefault(3).GetValue<string>(); 
        //            nuevocomercio.IdUsuarioAlta = idAdmin;
        //            nuevocomercio.FechaAlta = DateTime.Now;
        //            nuevocomercio.RazonSocial = cells.ElementAtOrDefault(1).GetValue<string>();
        //            nuevocomercio.Direccion = cells.ElementAtOrDefault(2).GetValue<string>();
        //            nuevocomercio.Localidad = cells.ElementAtOrDefault(4).GetValue<string>();
        //            var est = cells.ElementAtOrDefault(5).GetValue<string>();
        //            nuevocomercio.Establecimiento = int.TryParse(est, out intFTP)? Int32.Parse(est):0;
        //            nuevocomercio.Posnet = cells.ElementAtOrDefault(6).GetValue<string>();
        //            nuevocomercio.Lapos = cells.ElementAtOrDefault(7).GetValue<string>();
        //            //nuevocomercio.Beneficio
        //            //FALTA EL BENEFICIO
        //            db.Comercios.Add(nuevocomercio);
        //        }

            //    db.SaveChanges();
            //}
        //}
    }
}