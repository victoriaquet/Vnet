﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vnet.Models;
using System.IO;
using System.Globalization;
using System.Data.Entity;

namespace Vnet.Controllers
{


    public class VnetRegistroController : Controller
    {
        // GET: VnetRegistro
        public ActionResult Index()
        {
            var registros = _context.VnetRegistros.Include(c => c.Comercio).ToList();                  

            return View(registros);

         }



        [HttpPost]
        public ActionResult TratarArchivo(HttpPostedFileBase file)
        {
            StreamReader archivo = new StreamReader(file.InputStream);

            string nombre = file.FileName;

            //Control Input:
            if (!ControlNombreArchivo(nombre) && !ControlRepeticionArchivo(nombre))
                return View("Error");
            
            
            string linea = archivo.ReadLine();
            ControlLinea(linea);


            while ( linea != null)
            {
                TratarLinea(linea,nombre);
                linea = archivo.ReadLine();
            }

            var registros = _context.VnetRegistros.Include(c=> c.Comercio ).ToList();

            return View("Index", registros);
        }


          
        
        //Trato la linea de los txt. Guardo cada registro correspondiente a la linea de un archivo.
        void TratarLinea (string linea, string nombreArchivo)
        {
            var l = linea.Length;

            //Relleno con blancos hasta 125
            while (linea.Length < 125)
            {
                linea = linea + " ";
                l = l + 1;
            }

            try
            {

                //Empiezo a crear variable auxiliaries para rescatar los campos de el string.
                var nroRegistro = Convert.ToInt32(linea.Substring(1, 11));
                var nroEstablecimiento = Convert.ToInt32(linea.Substring(12, 12));
                ControlNroEstablecimiento(nroEstablecimiento);

                var nroTerminal = Convert.ToInt32(linea.Substring(24, 12));

                //Fecha
                var dia = Convert.ToInt32(linea.Substring(36, 2));
                var mes = Convert.ToInt32(linea.Substring(38, 2));
                var anio = Convert.ToInt32(linea.Substring(40, 4));
                var hora = Convert.ToInt32(linea.Substring(44, 2));
                var minuto = Convert.ToInt32(linea.Substring(46, 2));
                var segundo = Convert.ToInt32(linea.Substring(48, 2));

                DateTime fechaCompleta = new DateTime();

                if (dia != 0 && mes != 0 && anio != 0)
                {
                fechaCompleta = new DateTime(anio, mes, dia, hora, minuto, segundo);
                }


                var nroTarjeta = Convert.ToInt64(linea.Substring(50, 16));

                //Importes
                var culture = CultureInfo.InvariantCulture;
                //Armo los strings para poder parsear los importes
                var impEntera = linea.Substring(68, 10);
                var impDecimal = linea.Substring(78, 2);
                var importe = Convert.ToDecimal(impEntera + '.' + impDecimal, culture);



                var descuento = Convert.ToInt32(linea.Substring(66, 2));
                var impDescEntera = linea.Substring(80, 10);
                var impDescDecimal = linea.Substring(90, 2);
                var importeDescuento = Convert.ToDecimal(impDescEntera + '.' + impDescDecimal, culture);

                var comercios = _context.Comercios.ToList();
                var descripcion = linea.Substring(92, 33);

                var nuevoRegistro = new VnetRegistro
                {
                    Id = 0,
                    NroRegistro = nroRegistro,
                    NroEstablecimiento = nroEstablecimiento,
                    NroTerminal = nroTerminal,
                    Comercio = comercios.SingleOrDefault(c => c.Establecimiento == nroEstablecimiento),

                    FechaHoraMov = fechaCompleta,
                    NroTarjeta = nroTarjeta,


                    Descuento = descuento,
                    Importe = importe,
                    ImporteDescuento = importeDescuento,
                    Descripcion = descripcion,

                    NombreArchivo = nombreArchivo,
                    HoraDeSubida = DateTime.Now

                 };

                 //Guardo el Registro
                 SaveRegistroVnet(nuevoRegistro);


            }
            catch (Exception ex)
            {
                ViewBag.mensaje = "Se produjo un error:" + ex.Message;
                
            }        


        }


        //Realiza la acción de guardar el registro en la BD
        void SaveRegistroVnet(VnetRegistro registro)

        {
           if (!ModelState.IsValid)
            {
                Index();
            }
            _context.VnetRegistros.Add(registro);

            _context.SaveChanges();
        }

        //------------------------------------------------------
        //----------CONTROLES DE INPUTS-------------------------
        //public Boolean ControlNombreArchivo (string nombre)
        //{
        //    var prefijo = nombre.Substring(0,9);
        //    //Ver que el nombre del archivo empiece con 'SNC_Visa_'
        //    if (prefijo == "SNC_Visa_")
        //        return (true);

        //    return (false);            
        //}


        ////Ver que ya no se hayan cargado registros de ese archivo (agregar path a Vnet Registro)
        //public Boolean ControlRepeticionArchivo (string nombre)
        //{
        //    var control = _context.VnetRegistros.SingleOrDefault(c=> c.NombreArchivo == nombre);

        //    if (control == null)
        //        return (true);

        //    return (false);          

        //}//No anda



        //void ControlLinea (string data)
        //{
        //    //Si la linea es blanca o excede la longitud devuelve falso
        //}

        //void ControlNroEstablecimiento (int nro)
        //{
        //    //Controlar que el numero de establecimiento parseado exista, 
        //    //sino tirar :´El archivo ingresado no pertenece a un comercio activo en nuestras BD. Comuniquese con Admin.´
        //}

        //void ControlFormatoFecha (int fecha)
        //{
        //    //Controla que los parametros de la fecha sean valores copados. reemplazo por try parse?
        //}


        //Metodo en el que hice el parser teniendp en cuenta txt de una linea
        [HttpPost]
        public ActionResult TratarArchivoMonoLinea(HttpPostedFileBase file)
        {

            try
            {
                //Obtengo la data del file .txt
                string linea = new StreamReader(file.InputStream).ReadToEnd();
                var l = linea.Length;
                //El txt. tiene longitud variable Completo con blancos hasta Que el campo descripcion (que está al final) llegue a 33. 

                while (linea.Length < 125)
                {
                    linea = linea + " ";
                    l = l + 1;
                }


                //Empiezo a crear variable auxiliaries para rescatar los campos de el string.
                var nroRegistro = Convert.ToInt32(linea.Substring(1, 11));
                var nroEstablecimiento = Convert.ToInt32(linea.Substring(12, 12));
                var nroTerminal = Convert.ToInt32(linea.Substring(24, 12));

                //Fecha
                var dia = Convert.ToInt32(linea.Substring(36, 2));
                var mes = Convert.ToInt32(linea.Substring(38, 2));
                var anio = Convert.ToInt32(linea.Substring(40, 4));
                var hora = Convert.ToInt32(linea.Substring(44, 2));
                var minuto = Convert.ToInt32(linea.Substring(46, 2));
                var segundo = Convert.ToInt32(linea.Substring(48, 2));

                DateTime fechaCompleta = new DateTime();

                if (dia!= 0 &&mes!= 0 &&anio!= 0)
                {
                fechaCompleta = new DateTime(anio, mes, dia, hora, minuto, segundo);
                }

               
                var nroTarjeta = Convert.ToInt64(linea.Substring(50, 16));

                //Importes
                var culture = CultureInfo.InvariantCulture;
                //Armo los strings para poder parsear los importes
                var impEntera = linea.Substring(68, 10);
                var impDecimal = linea.Substring(78, 2);
                var importe = Convert.ToDecimal(impEntera + '.' + impDecimal , culture);

               

                var descuento = Convert.ToInt32(linea.Substring(66, 2));
                var impDescEntera = linea.Substring(80, 10);
                var impDescDecimal = linea.Substring(90, 2);
                var importeDescuento = Convert.ToDecimal(impDescEntera + '.' + impDescDecimal , culture);

               
                var descripcion = linea.Substring(92, 33);
                var comercios = _context.Comercios.ToList();

                var nuevoRegistro = new VnetRegistro
                {
                    Id = 0,
                    NroRegistro = nroRegistro,
                    NroEstablecimiento = nroEstablecimiento,
                    NroTerminal = nroTerminal,
                    Comercio = comercios.SingleOrDefault(c => c.Establecimiento == nroEstablecimiento),
                    FechaHoraMov = fechaCompleta,
                    NroTarjeta = nroTarjeta,


                    Descuento = descuento,
                    Importe = importe,
                    ImporteDescuento = importeDescuento,
                    Descripcion = descripcion
                };

                //Guardo el Registro
                SaveRegistroVnet(nuevoRegistro);
              

            }
            catch (Exception ex)
            {
                ViewBag.mensaje = "Se produjo un error:" + ex.Message;
                return View("Error","Shared");
            }

            var registros = _context.VnetRegistros.Include(c => c.Comercio).ToList();

            return View("Index", registros);

        }





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

        //------------------------------------------------------------
        //--------- Generar Pdf

        public ActionResult Imprimir()
        {
            return View();
        }










    }
}