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
    public class OfertasController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: Ofertas
        public ActionResult Index(string msg)
        {
            ViewBag.msg = "mensaje";

            if (msg != null)
            {
                ViewBag.msg = msg;
            }
            return View(db.Ofertas.ToList());
        }

        // GET: Ofertas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oferta oferta = db.Ofertas.Find(id);
            if (oferta == null)
            {
                return HttpNotFound();
            }
            return View(oferta);
        }

        // GET: Ofertas/Create
        public ActionResult Create()
        {
            ViewBag.VbEstadosOfertas = db.EstadoOfertas.ToList();
            ViewBag.Products = db.Productos.ToList();


            return View();
        }

        // POST: Ofertas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdOferta,CantDias,Editorial,TipoOferta,Codigo,Nombre,CodigoProductoServinor,NombreProductoServinor,Descripcion,Precio,CantTarjetasAdicionales,IdEstadoOferta,FechaCarga,FechaInicio,FechaModificacion,FechaFin")] Oferta oferta, int[] idproducts, int[] copias)
        {
           
          

            oferta.FechaCarga=DateTime.Now;
            oferta.FechaModificacion=DateTime.Now;
            oferta.NombreProductoServinor = "DIARIO NORTE";
            oferta.CodigoProductoServinor = "000001";

            if (ModelState.IsValid)
            {
                db.Ofertas.Add(oferta);
                db.SaveChanges();

                var i = 0;
                foreach (int idprod in idproducts)
                {
                   

                    ProductoOferta productoOferta = new ProductoOferta();
                    productoOferta.IdProducto = idprod;
                    productoOferta.IdOferta = oferta.IdOferta;
                    productoOferta.Copias = copias[i];
                    db.ProductoOfertas.Add(productoOferta);
                    db.SaveChanges();
                    i = i + 1;

                }

                HistorialPrecioOferta hprecio = new HistorialPrecioOferta();
                hprecio.FechaInicio = oferta.FechaInicio;
                hprecio.FechaModificacion = DateTime.Now;
                hprecio.Precio = oferta.Precio;
                hprecio.IdOferta = oferta.IdOferta;
                db.HistorialPrecioOfertas.Add(hprecio);
                db.SaveChanges();
                

                return RedirectToAction("Index");
            }
            var a = 0;
            List<ProductoOferta> prodof=new List<ProductoOferta>();
            foreach (int idprod in idproducts)
            {
                

                ProductoOferta productoOferta = new ProductoOferta();
                productoOferta.IdProducto = idprod;
                productoOferta.Producto = db.Productos.Find(idprod);
                productoOferta.IdOferta = oferta.IdOferta;
                productoOferta.Copias = copias[a];
                prodof.Add(productoOferta);
                a = a + 1;
            }
            ViewBag.prodsacum = prodof;
            ViewBag.VbEstadosOfertas = db.EstadoOfertas.ToList();
            ViewBag.Products = db.Productos.ToList();
            return View(oferta);
        }

        // GET: Ofertas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oferta oferta = db.Ofertas.Find(id);
            if (oferta == null)
            {
                return HttpNotFound();
            }
            ViewBag.VbEstadosOfertas = db.EstadoOfertas.ToList();
            ViewBag.Products = db.Productos.ToList();
            return View(oferta);
        }

        // POST: Ofertas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdOferta,CantDias,Editorial,Codigo,TipoOferta,Nombre,NombreProductoServinor,CodigoProductoServinor,IdEstadoOferta,Descripcion,CantTarjetasAdicionales")] Oferta oferta, int[] idproducts)
        {
            
           
            
            Oferta ofertaedit = db.Ofertas.Find(oferta.IdOferta);

            List<ProductoOferta> listprod = ofertaedit.ProductoOfertas.ToList();
            foreach (var item in listprod)
            {
                ProductoOferta prodelim=new ProductoOferta();
                prodelim = item;
                db.ProductoOfertas.Remove(prodelim);
                db.SaveChanges();
            }




            foreach (int idprod in idproducts)
            {
                ProductoOferta productoOferta = new ProductoOferta();
                productoOferta.IdProducto = idprod;
                productoOferta.IdOferta = oferta.IdOferta;
                db.ProductoOfertas.Add(productoOferta);
                db.SaveChanges();

            }
            ofertaedit.Codigo = oferta.Codigo;
            ofertaedit.IdEstadoOferta = oferta.IdEstadoOferta;
            ofertaedit.Nombre = oferta.Nombre;
            ofertaedit.CantDias = oferta.CantDias;
            ofertaedit.Editorial = oferta.Editorial;
            ofertaedit.TipoOferta = oferta.TipoOferta;
            ofertaedit.Descripcion = oferta.Descripcion;
            ofertaedit.CantTarjetasAdicionales = oferta.CantTarjetasAdicionales;
            ofertaedit.FechaModificacion = DateTime.Now;
            db.Entry(ofertaedit).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { msg = "Edición correcta." });
            
           
        }

        // GET: Ofertas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oferta oferta = db.Ofertas.Find(id);
            if (oferta == null)
            {
                return HttpNotFound();
            }
            return View(oferta);
        }

        // POST: Ofertas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Oferta oferta = db.Ofertas.Find(id);
            db.Ofertas.Remove(oferta);
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
