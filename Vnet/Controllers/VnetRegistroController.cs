using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vnet.Models;
using System.IO;
using System.Globalization;

namespace Vnet.Controllers
{
    public class VnetRegistroController : Controller
    {
        //Cosas para usar el contexto

        private ApplicationDbContext _context;

        //Necesito inicilizar el atributo (variable) anterior, lo hago en el constructor
        public VnetRegistroController()
        {
            _context = new ApplicationDbContext();
        }

        //
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult SubirArchivo()
        {
            return View();
        }
            


        [HttpPost]
        public ActionResult SearchAndUpload(HttpPostedFileBase file)
        {

            try
            {
                //Obtengo la data del file.. No puede llamarse archivo.
                string resultado = new StreamReader(file.InputStream).ReadToEnd();
               
                //armo el string para poder parsear la fecha
                var dia = Convert.ToInt32(resultado.Substring(36, 2));
                var mes = Convert.ToInt32(resultado.Substring(38, 2));
                var anio = Convert.ToInt32(resultado.Substring(40, 4));
                var hora = Convert.ToInt32(resultado.Substring(44, 2));
                var minuto = Convert.ToInt32(resultado.Substring(46, 2));
                var segundo = Convert.ToInt32(resultado.Substring(48, 2));
                DateTime fechaCompleta = new DateTime(anio, mes, dia, hora, minuto, segundo);
                //anio + '-' + mes + '-' + dia + ' ' + hora + ':' + minuto + " PM";

                //Armo los strings para poder parsear los importes
                var impEntera = resultado.Substring(68, 10);
                var impDecimal = resultado.Substring(78, 2);
                var importe = Convert.ToDecimal(impEntera + '.' + impDecimal);

                var impDescEntera = resultado.Substring(80, 10);
                var impDescDecimal = resultado.Substring(90, 2);
                var importeDescuento = Convert.ToDecimal(impDescEntera + '.' + impDescDecimal);

                var descuento = resultado.Substring(66, 2);
                var descripcion = resultado.Substring(92, 28);

                var nuevoRegistro = new VnetRegistro();


                nuevoRegistro.Id = 0;
                nuevoRegistro.NroRegistro = Convert.ToInt32(resultado.Substring(0, 11));
                nuevoRegistro.NroEstablecimiento = Convert.ToInt32(resultado.Substring(12, 12));
                nuevoRegistro.NroTerminal = Convert.ToInt32(resultado.Substring(24, 12));
                nuevoRegistro.FechaHoraMov = fechaCompleta;
                nuevoRegistro.NroTarjeta = Convert.ToInt32(resultado.Substring(50, 16));

                
                nuevoRegistro.Descuento = Convert.ToInt16(descuento);
                nuevoRegistro.Importe =  importe;
                nuevoRegistro.ImporteDescuento = importeDescuento;
                nuevoRegistro.Descripcion = descripcion;
                
        
                
                
             


              

                _context.VnetRegistros.Add(nuevoRegistro);

                _context.SaveChanges();

                return RedirectToAction("SubirArchivo");
              

            }
            catch (Exception ex)
            {
                ViewBag.mensaje = "Se produjo un error:" + ex.Message;
            }

            return RedirectToAction("Index");

        }


        //
        //public ActionResult SaveRegistroVnet(VnetRegistro registro)

        //{
        //    _context.VnetRegistros.Add(registro);

        //    _context.SaveChanges();

        //    return RedirectToAction("Index");
        //}



        // GET: VnetRegistro
        public ActionResult Index()
        {
            return View();
        }
    }
}