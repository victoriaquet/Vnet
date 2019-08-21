using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SNC2017.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;

namespace SNC2017.Controllers
{
    public class ArchivoImpresionTCsController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: ArchivoImpresionTCs
        public ActionResult Index()
        {
            return View(db.ArchivoImpresionTcs.ToList());
        }
        public ActionResult GenerarArchivoTC(string[] idtc,string observacion)
        {
            ArchivoImpresionTC archivoImpresionTc = new ArchivoImpresionTC();
            archivoImpresionTc.FechaCreacion = DateTime.Now;
            archivoImpresionTc.FechaModificacion = DateTime.Now;
            archivoImpresionTc.Responsable = System.Web.HttpContext.Current.User.Identity.Name;
            archivoImpresionTc.Observacion = observacion;
            db.ArchivoImpresionTcs.Add(archivoImpresionTc);
            db.SaveChanges();
            db.Entry(archivoImpresionTc).State = EntityState.Modified;
            db.SaveChanges();

            foreach (var item in idtc)
            {
                int id = Int32.Parse(item);
                TarjetaClub tarjetaClub = db.TarjetaClubes.Find(id);
                tarjetaClub.EstadoTarjetaClub = EstadoTarjetaClub.ListaParaImprimir;
                tarjetaClub.FechaSolicitudImpresion = DateTime.Now;
                db.Entry(tarjetaClub).State = EntityState.Modified;
                db.SaveChanges();

                TCporArchivo tcporarchivo = new TCporArchivo();
                tcporarchivo.IdTarjetaClub = tarjetaClub.IdTarjetaClub;
                tcporarchivo.IdArchivoImpresionTC = archivoImpresionTc.IdArchivoImpresionTC;
                db.TCporArchivos.Add(tcporarchivo);
                db.SaveChanges();
            }
            archivoImpresionTc.NombreArchivo = DateTime.Now.ToString("yyyy/MM/dd HH:mm - " + archivoImpresionTc.IdArchivoImpresionTC + " - " + archivoImpresionTc.TCporArchivo.Count());
        
            db.Entry(archivoImpresionTc).State = EntityState.Modified;
            db.SaveChanges();
            TempData["ok"] = "success";
            return RedirectToAction("Details", new { id = archivoImpresionTc.IdArchivoImpresionTC });
        }

        public ActionResult DescargarArchivoTC(int id)
        {
            ArchivoImpresionTC archivoImpresionTc = db.ArchivoImpresionTcs.Find(id);
          
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;
            workSheet.Protection.IsProtected = true;
            //Header of table  
            //  
            workSheet.Row(1).Height = 20;
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            workSheet.Row(1).Style.Font.Bold = true;
            workSheet.Cells[1, 1].Value = "NroTarjeta";
            workSheet.Cells[1, 2].Value = "Apellido";
            workSheet.Cells[1, 3].Value = "Nombre";
            workSheet.Cells[1, 4].Value = "ApellidoYNombre";
            workSheet.Cells[1, 5].Value = "Track1";
            workSheet.Cells[1, 6].Value = "Track2";
            //Body of table  
            //  
            int recordIndex = 2;
            foreach (var item in archivoImpresionTc.TCporArchivo)
            {
                workSheet.Cells[recordIndex, 1].Value =HomeController.codigocomercio+item.TarjetaClub.Numero;
                workSheet.Cells[recordIndex, 2].Value = item.TarjetaClub.Apellido.ToUpper();
                workSheet.Cells[recordIndex, 3].Value = item.TarjetaClub.Nombres.ToUpper();
                workSheet.Cells[recordIndex, 4].Value = item.TarjetaClub.Apellido.ToUpper()+ " " + item.TarjetaClub.Nombres.ToUpper();
                workSheet.Cells[recordIndex, 5].Value = HomeController.letratrack1+item.TarjetaClub.Numero+"^"+ item.TarjetaClub.Apellido.ToUpper() + " " + item.TarjetaClub.Nombres.ToUpper() + "^"+HomeController.nroextratrack12;
                workSheet.Cells[recordIndex, 6].Value = item.TarjetaClub.Numero+"="+ HomeController.nroextratrack12;
                recordIndex++;
            }
            workSheet.Column(1).AutoFit();
            workSheet.Column(2).AutoFit();
            workSheet.Column(3).AutoFit();
            workSheet.Column(4).AutoFit();
            workSheet.Column(5).AutoFit();
            workSheet.Column(6).AutoFit();
            string excelName = archivoImpresionTc.NombreArchivo;
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //Response.Status = archivoImpresionTc.NombreArchivo + "-" + archivoImpresionTc.Responsable +"-DiarioNorte-SNC2018";
                Response.AddHeader("content-disposition", "attachment; filename=" + excelName + ".xlsx");
                excel.Workbook.Properties.Title = archivoImpresionTc.NombreArchivo;
                excel.Workbook.Properties.Author = System.Web.HttpContext.Current.User.Identity.Name;
                excel.Workbook.Properties.Company = "Diario Norte - Editorial Chaco S.A";
                excel.Workbook.Properties.Application ="SNC2018 * "+ archivoImpresionTc.NombreArchivo+" * "+ System.Web.HttpContext.Current.User.Identity.Name+" * " + DateTime.Now.ToString("dd-MM-yyyy HH:ss");
                excel.Workbook.Properties.Comments = DateTime.Now.ToString("dd-MM-yyyy HH:ss");

                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
         
            return RedirectToAction("Details", new { id = id, message="success" });
        }

        // GET: ArchivoImpresionTCs/Details/5
        public ActionResult Details(int? id,string message)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArchivoImpresionTC archivoImpresionTC = db.ArchivoImpresionTcs.Find(id);
            if (archivoImpresionTC == null)
            {
                return HttpNotFound();
            }
            if (!string.IsNullOrEmpty(message))TempData["ok"] = message;
            return View(archivoImpresionTC);
        }

        // GET: ArchivoImpresionTCs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArchivoImpresionTCs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdArchivoImpresionTC,FechaCreacion,FechaModificacion,NombreArchivo,Observacion,Responsable,ArchivoTxt")] ArchivoImpresionTC archivoImpresionTC)
        {
            if (ModelState.IsValid)
            {
                db.ArchivoImpresionTcs.Add(archivoImpresionTC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(archivoImpresionTC);
        }

        // GET: ArchivoImpresionTCs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArchivoImpresionTC archivoImpresionTC = db.ArchivoImpresionTcs.Find(id);
            if (archivoImpresionTC == null)
            {
                return HttpNotFound();
            }
            return View(archivoImpresionTC);
        }

        // POST: ArchivoImpresionTCs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdArchivoImpresionTC,FechaCreacion,FechaModificacion,NombreArchivo,Observacion,Responsable,ArchivoTxt")] ArchivoImpresionTC archivoImpresionTC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(archivoImpresionTC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(archivoImpresionTC);
        }

        public ActionResult EditObservacion(int id, string obs)
        {
            ArchivoImpresionTC archivoImpresionTc = db.ArchivoImpresionTcs.Find(id);
            archivoImpresionTc.Observacion = obs;
            if (ModelState.IsValid)
            {
                db.Entry(archivoImpresionTc).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = archivoImpresionTc.IdArchivoImpresionTC });

        }

        // GET: ArchivoImpresionTCs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArchivoImpresionTC archivoImpresionTC = db.ArchivoImpresionTcs.Find(id);
            if (archivoImpresionTC == null)
            {
                return HttpNotFound();
            }
            return View(archivoImpresionTC);
        }

        // POST: ArchivoImpresionTCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArchivoImpresionTC archivoImpresionTC = db.ArchivoImpresionTcs.Find(id);
            db.ArchivoImpresionTcs.Remove(archivoImpresionTC);
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
