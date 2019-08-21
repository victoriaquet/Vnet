using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Newtonsoft.Json;
using SNC2017.Models;
using SNC2017.ViewModels;

namespace SNC2017.Controllers
{
    public class SuscripcionesController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: Suscripciones
        public ActionResult Index()
        {
            return View();
        }
        public string SuscripcionesConCanillita(int estado)
        {
            switch (estado)
            {
                case 0:
                    return JsonConvert.SerializeObject( db.Suscripciones.Include(s => s.Oferta).Include(s => s.Suscriptor).ToList().Select(a => new
                    {
                        Id = a.IdSuscripcion,
                        Suscriptor = $"{a.Suscriptor.Apellido} {a.Suscriptor.Nombre} (N° {a.Suscriptor.NumeroSuscriptor})",
                        Estado = Enum.GetName(typeof(EstadoSuscripcion), a.EstadoSuscripcionHistorial.LastOrDefault().EstadoSuscripcion),
                        Oferta = _NombreOferta(a),
                        FechaAlta = a.FechaAlta.ToString("yyyy/MM/dd HH:mm")
                    }));
                case 1:
                    return JsonConvert.SerializeObject(db.Suscripciones.Include(s => s.Oferta).Include(s => s.Suscriptor).ToList().FindAll(x => x.TipoSuscripcion == TipoSuscripcion.Normal).Select(a => new
                    {
                        Id = a.IdSuscripcion,
                        Suscriptor = $"{a.Suscriptor.Apellido} {a.Suscriptor.Nombre} (N° {a.Suscriptor.NumeroSuscriptor})",
                        Estado = Enum.GetName(typeof(EstadoSuscripcion), a.EstadoSuscripcionHistorial.LastOrDefault().EstadoSuscripcion),
                        Oferta = _NombreOferta(a),
                        FechaAlta = a.FechaAlta.ToString("yyyy/MM/dd HH:mm")
                    }));
                case 2:
                    return JsonConvert.SerializeObject(db.Suscripciones.Include(s => s.Oferta).Include(s => s.Suscriptor).ToList().FindAll(x => x.TipoSuscripcion == TipoSuscripcion.Coorporativo).Select(a => new
                    {
                        Id = a.IdSuscripcion,
                        Suscriptor = $"{a.Suscriptor.Apellido} {a.Suscriptor.Nombre} (N° {a.Suscriptor.NumeroSuscriptor})",
                        Estado = Enum.GetName(typeof(EstadoSuscripcion), a.EstadoSuscripcionHistorial.LastOrDefault().EstadoSuscripcion),
                        Oferta = _NombreOferta(a),
                        FechaAlta = a.FechaAlta.ToString("yyyy/MM/dd HH:mm")
                    }));
                case 3:
                    return JsonConvert.SerializeObject(db.Suscripciones.Include(s => s.Oferta).Include(s => s.Suscriptor).ToList().FindAll(x => x.TipoSuscripcion == TipoSuscripcion.Cortesia).Select(a => new
                    {
                        Id = a.IdSuscripcion,
                        Suscriptor = $"{a.Suscriptor.Apellido} {a.Suscriptor.Nombre} (N° {a.Suscriptor.NumeroSuscriptor})",
                        Estado = Enum.GetName(typeof(EstadoSuscripcion), a.EstadoSuscripcionHistorial.LastOrDefault().EstadoSuscripcion),
                        Oferta = _NombreOferta(a),
                        FechaAlta = a.FechaAlta.ToString("yyyy/MM/dd HH:mm")
                    }));
                default:
                    return JsonConvert.SerializeObject(db.Suscripciones.Include(s => s.Oferta).Include(s => s.Suscriptor).ToList().Select(a => new
                    {
                        Id = a.IdSuscripcion,
                        Suscriptor = $"{a.Suscriptor.Apellido} {a.Suscriptor.Nombre} (N° {a.Suscriptor.NumeroSuscriptor})",
                        Estado = Enum.GetName(typeof(EstadoSuscripcion), a.EstadoSuscripcionHistorial.LastOrDefault().EstadoSuscripcion),
                        Oferta = _NombreOferta(a),
                        FechaAlta = a.FechaAlta.ToString("yyyy/MM/dd HH:mm")
                    }));
            }

            
        }
        public string SuscripcionesSinCanillita(int estado)
        {
            switch (estado)
            {
                case 0:
                    return JsonConvert.SerializeObject(db.Suscripciones.Include(s => s.Oferta).Include(s => s.Suscriptor).ToList().FindAll(x => x.IdCanillita == null ).Select(a => new
                    {
                        Id = a.IdSuscripcion,
                        Suscriptor = $"{a.Suscriptor.Apellido} {a.Suscriptor.Nombre} (N° {a.Suscriptor.NumeroSuscriptor})",
                        Estado = Enum.GetName(typeof(EstadoSuscripcion), a.EstadoSuscripcionHistorial.LastOrDefault().EstadoSuscripcion),
                        Oferta = _NombreOferta(a),
                        FechaAlta = a.FechaAlta.ToString("yyyy/MM/dd HH:mm")
                    }));
                case 1:
                    return JsonConvert.SerializeObject(db.Suscripciones.Include(s => s.Oferta).Include(s => s.Suscriptor).ToList().FindAll(x => x.IdCanillita == null && x.TipoSuscripcion == TipoSuscripcion.Normal).Select(a => new
                    {
                        Id = a.IdSuscripcion,
                        Suscriptor = $"{a.Suscriptor.Apellido} {a.Suscriptor.Nombre} (N° {a.Suscriptor.NumeroSuscriptor})",
                        Estado = Enum.GetName(typeof(EstadoSuscripcion), a.EstadoSuscripcionHistorial.LastOrDefault().EstadoSuscripcion),
                        Oferta = _NombreOferta(a),
                        FechaAlta = a.FechaAlta.ToString("yyyy/MM/dd HH:mm")
                    }));
                case 2:
                    return JsonConvert.SerializeObject(db.Suscripciones.Include(s => s.Oferta).Include(s => s.Suscriptor).ToList().FindAll(x => x.IdCanillita == null && x.TipoSuscripcion == TipoSuscripcion.Coorporativo).Select(a => new
                    {
                        Id = a.IdSuscripcion,
                        Suscriptor = $"{a.Suscriptor.Apellido} {a.Suscriptor.Nombre} (N° {a.Suscriptor.NumeroSuscriptor})",
                        Estado = Enum.GetName(typeof(EstadoSuscripcion), a.EstadoSuscripcionHistorial.LastOrDefault().EstadoSuscripcion),
                        Oferta = _NombreOferta(a),
                        FechaAlta = a.FechaAlta.ToString("yyyy/MM/dd HH:mm")
                    }));
                case 3:
                    return JsonConvert.SerializeObject(db.Suscripciones.Include(s => s.Oferta).Include(s => s.Suscriptor).ToList().FindAll(x => x.IdCanillita == null && x.TipoSuscripcion == TipoSuscripcion.Cortesia).Select(a => new
                    {
                        Id = a.IdSuscripcion,
                        Suscriptor = $"{a.Suscriptor.Apellido} {a.Suscriptor.Nombre} (N° {a.Suscriptor.NumeroSuscriptor})",
                        Estado = Enum.GetName(typeof(EstadoSuscripcion), a.EstadoSuscripcionHistorial.LastOrDefault().EstadoSuscripcion),
                        Oferta = _NombreOferta(a),
                        FechaAlta = a.FechaAlta.ToString("yyyy/MM/dd HH:mm")
                    }));
                default:
                    return JsonConvert.SerializeObject(db.Suscripciones.Include(s => s.Oferta).Include(s => s.Suscriptor).ToList().FindAll(x => x.IdCanillita == null).Select(a => new
                    {
                        Id = a.IdSuscripcion,
                        Suscriptor = $"{a.Suscriptor.Apellido} {a.Suscriptor.Nombre} (N° {a.Suscriptor.NumeroSuscriptor})",
                        Estado = Enum.GetName(typeof(EstadoSuscripcion), a.EstadoSuscripcionHistorial.LastOrDefault().EstadoSuscripcion),
                        Oferta = _NombreOferta(a),
                        FechaAlta = a.FechaAlta.ToString("yyyy/MM/dd HH:mm")
                    }));
            }
        }

        public string _NombreOferta(Suscripcion suscripcion)
        {
            var nombre = $"{suscripcion.Oferta.Nombre}";
            if(suscripcion.Lunes ||
               suscripcion.Martes ||
               suscripcion.Miercoles || 
               suscripcion.Jueves ||
               suscripcion.Viernes || 
               suscripcion.Sabado) nombre +=" + " ;
            nombre += suscripcion.Lunes ? "Lu " : "";
            nombre += suscripcion.Martes ? "Ma " : "";
            nombre += suscripcion.Miercoles ? "Mi " : "";
            nombre += suscripcion.Jueves ? "Ju " : "";
            nombre += suscripcion.Viernes ? "Vi " : "";
            nombre += suscripcion.Sabado ? "Sá" : "";

            return nombre;
        }
        public ActionResult ListadoNormal()
        {
            return View();
        }
        public ActionResult ListadoCoorporativa()
        {
            return View();
        }
        public ActionResult ListadoCortesia()
        {
            return View();
        }
        // GET: Suscripciones/Details/5
        public ActionResult VerDiasEntregaSec(int id)
        {
           
            SuscriptorSecundario suscriptorSecundario = db.SuscriptorSecundarios.Find(id);
                   ICollection<DiaEntrega>diasEntregas= db.SuscriptorSecundarios.Find(id).DiaEntregas;

            return View(suscriptorSecundario);

        }
        // GET: Suscripciones/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.canillitas = db.Canillitas.ToList().FindAll(x=>x.Activo);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suscripcion suscripcion = db.Suscripciones.Find(id);
            if (suscripcion == null)
            {
                return HttpNotFound();
            }
            if (suscripcion.TipoSuscripcion == TipoSuscripcion.Coorporativo)
            {
                return RedirectToAction("DetailsCoorporativa", new {id});
            }
            if (suscripcion.TipoSuscripcion == TipoSuscripcion.Cortesia)
            {
                return RedirectToAction("DetailsCortesia", new { id });
            }

            return View(suscripcion); 
            
        }
        // GET: Suscripciones/Details/5
        public ActionResult DetailsCortesia(int? id)
        {
            ViewBag.canillitas = db.Canillitas.ToList().FindAll(x => x.Activo);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suscripcion suscripcion = db.Suscripciones.Find(id);
            if (suscripcion == null)
            {
                return HttpNotFound();
            }
            if (suscripcion.TipoSuscripcion == TipoSuscripcion.Coorporativo)
            {
                return RedirectToAction("DetailsCoorporativa", new { id });
            }
            if (suscripcion.TipoSuscripcion == TipoSuscripcion.Normal)
            {
                return RedirectToAction("Details", new { id });
            }

            return View(suscripcion);

        }

        public ActionResult GetOfferDays(int id)
        {

            Suscripcion suscripcion = db.Suscripciones.Find(id);
            if (suscripcion == null)
            {
                return HttpNotFound();
            }
            string fechaInicio = DateTime.Now.AddMonths(-2).ToShortDateString();
            string fechaEstimadaFinCalendario = DateTime.Now.AddMonths(6).ToShortDateString();
            
            var listOfdays = new List<string>();
            //se agrega las fechas de inicio y fin para resaltar el calendario en ese rango
            listOfdays.Add(fechaInicio);
            listOfdays.Add(fechaEstimadaFinCalendario);

            // se agregar los días de la semana correspondientes a la oferta para resaltar en el calendario
            if (suscripcion.Domingo) { listOfdays.Add("0"); } //0 representa domingo...
            if (suscripcion.Lunes) { listOfdays.Add("1"); }
            if (suscripcion.Martes) { listOfdays.Add("2"); }
            if (suscripcion.Miercoles) { listOfdays.Add("3"); }//...3 representa miércoles... 
            if (suscripcion.Jueves) { listOfdays.Add("4"); }
            if (suscripcion.Viernes) { listOfdays.Add("5"); }
            if (suscripcion.Sabado) { listOfdays.Add("6"); } //...6 representa sábado


            return Json(listOfdays, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DetailsCoorporativa(int id)
        {
            ViewBag.Suscriptores = db.Suscriptores.ToList().FindAll(x => x.TipoSuscriptor == TipoSuscriptor.Normal);

            Suscripcion suscripcion = db.Suscripciones.Find(id);
            if (suscripcion == null)
            {
                return HttpNotFound();
            }
            ViewBag.canillitas = db.Canillitas.ToList().FindAll(x => x.Activo);
            return View(suscripcion);
        }

        #region CreateCoorporativa
        // GET: Suscripciones/Create
        public ActionResult CreateCoorporativa(int? id)
        {
            if (id != null)
            {
                ViewBag.idsus = id;
            }
            ViewBag.IdOferta = db.Ofertas.ToList().FindAll(x => x.TipoOferta != TipoOferta.CoorporativaAsignada);
            ViewBag.Suscriptores = db.Suscriptores.ToList().FindAll(x => x.TipoSuscriptor == TipoSuscriptor.Normal);
            ViewBag.Suscriptor = db.Suscriptores.Find(id);



            ViewBag.provincias = db.Provincias.ToList();
            ViewBag.tipoPeriodoEfectivo = db.TiposPeriodoEfectivo.ToList();
            ViewBag.localidades = db.Localidades.ToList();
            ViewBag.dateNow = DateTime.Now.GetDateTimeFormats()[7];
            ViewBag.ofertasChaco = db.Ofertas.ToList().FindAll(o => o.Editorial == Editorial.Chaco & o.TipoOferta == TipoOferta.Coorporativa);
            ViewBag.ofertasCtes = db.Ofertas.ToList().FindAll(o => o.Editorial == Editorial.Corrientes & o.TipoOferta == TipoOferta.Coorporativa);
            return View();
        }
        // POST: Suscripciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCoorporativa(SuscripcionVM suscripcionVM, string[] suscriptoressecundarios)
        {
            suscripcionVM.Domingo = true;
            Suscripcion suscripcion = new Suscripcion();
            suscripcion.DiaEntregas = new List<DiaEntrega>();
            suscripcion.SuscriptorSecundarios = new List<SuscriptorSecundario>();
            Suscriptor suscriptor = new Suscriptor();
            Domicilio domicilio = new Domicilio();
            //Mapeo suscriptor
            var nrosuscriptor = 0;
            try
            {
                nrosuscriptor = db.Suscriptores.Count() + 2000;
            }
            catch (Exception)
            {
                nrosuscriptor = 1;


            }

            bool status = true;



            //SI EL SUSC EXISTE SE MODIFICA el domicilio y los datos del suscriptor

            suscriptor = db.Suscriptores.Find(suscripcionVM.IdSuscriptor.Value);
            domicilio = db.Domicilios.Find(suscriptor.IdDomicilio);
            suscriptor.Nombre = suscripcionVM.Nombre;
            suscriptor.Apellido = suscripcionVM.Apellido;
            suscriptor.DNI = suscripcionVM.DNI;
            suscriptor.FechaNacimiento = suscripcionVM.FechaNacimiento;
            suscriptor.NumeroSuscriptor = nrosuscriptor;
            suscriptor.TipoSexo = suscripcionVM.TipoSexo;
            suscriptor.Email = suscripcionVM.Email;
            suscriptor.TelefonoFijoNumero = suscripcionVM.TelefonoFijoNumero;
            suscriptor.TelefonoFijoArea = suscripcionVM.TelefonoFijoArea;
            suscriptor.TelefonoMovilNumero = suscripcionVM.TelefonoMovilNumero;
            suscriptor.TelefonoMovilArea = suscripcionVM.TelefonoMovilArea;
            suscriptor.CUIT = suscripcionVM.CUIT;
            suscriptor.RazonSocial = suscripcionVM.RazonSocial;

            //Mapeo Domicilio suscriptor


            domicilio.Calle = suscripcionVM.CalleSuscriptor;
            domicilio.Altura = suscripcionVM.AlturaSuscriptor;
            domicilio.Piso = suscripcionVM.PisoSuscriptor;
            domicilio.Departamento = suscripcionVM.DepartamentoSuscriptor;
            domicilio.CalleLateral1 = suscripcionVM.CalleLateral1Suscriptor;
            domicilio.CalleLateral2 = suscripcionVM.CalleLateral2Suscriptor;
            domicilio.Barrio = suscripcionVM.BarrioSuscriptor;
            domicilio.IdLocalidad = suscripcionVM.IdLocalidadSuscriptor;
            domicilio.Observaciones = suscripcionVM.ObservacionesDomicilioSuscriptor;
            domicilio.TituloDom = "Domicilio principal";

            db.Entry(domicilio).State = EntityState.Modified;
            db.SaveChanges();

            //agrego domicilio al susc y guardo el suscriptor
            suscriptor.IdDomicilio = domicilio.IdDomicilio;
            suscriptor.IdSuscriptor = suscripcionVM.IdSuscriptor.GetValueOrDefault();
            db.Entry(suscriptor).State = EntityState.Modified;
            db.SaveChanges();


            //Mapeo domicilio Facturación
            Domicilio domicilioFacturacion = new Domicilio();
            if (suscripcionVM.CalleFacturacion != null & suscripcionVM.AlturaFacturacion != null)
            {
                domicilioFacturacion.Calle = suscripcionVM.CalleFacturacion;
                domicilioFacturacion.Altura = suscripcionVM.AlturaFacturacion.GetValueOrDefault(); ;
                domicilioFacturacion.Piso = suscripcionVM.PisoFacturacion;
                domicilioFacturacion.CalleLateral1 = suscripcionVM.CalleLateral1Facturacion;
                domicilioFacturacion.CalleLateral2 = suscripcionVM.CalleLateral2Facturacion;
                domicilioFacturacion.Departamento = suscripcionVM.DepartamentoFacturacion;
                domicilioFacturacion.Barrio = suscripcionVM.BarrioFacturacion;
                domicilioFacturacion.IdLocalidad = suscripcionVM.IdLocalidadFacturacion.GetValueOrDefault();
                domicilioFacturacion.Observaciones = suscripcionVM.ObservacionesDomicilioFacturacion;
                domicilioFacturacion.TituloDom = "Domicilio facturación";
                db.Domicilios.Add(domicilioFacturacion);
                db.SaveChanges();
            }
            else
            {
                domicilioFacturacion = domicilio;
            }
            //Mapeo datos Facturación

            DatosFacturacion datosFacturacion = new DatosFacturacion();
            if (suscripcionVM.NombreFacturacion != null & suscripcionVM.ApellidoFacturacion != null
                & suscripcionVM.CUITFacturacion != null & suscripcionVM.RazonSocialFacturacion != null &
                suscripcionVM.DNIFacturacion != null)
            {
                datosFacturacion.Nombre = suscripcionVM.NombreFacturacion;
                datosFacturacion.Apellido = suscripcionVM.ApellidoFacturacion;
                datosFacturacion.DNI = (int)suscripcionVM.DNIFacturacion;
                datosFacturacion.CUIT = suscripcionVM.CUITFacturacion;
                datosFacturacion.RazonSocial = suscripcionVM.RazonSocialFacturacion;

            }
            else
            {
                datosFacturacion.Nombre = suscripcionVM.Nombre;
                datosFacturacion.Apellido = suscripcionVM.Apellido;
                datosFacturacion.DNI = suscripcionVM.DNI;
                datosFacturacion.CUIT = suscripcionVM.CUIT;
                datosFacturacion.RazonSocial = suscripcionVM.RazonSocial;
            }
            datosFacturacion.TipoIva = suscripcionVM.TipoIva;
            datosFacturacion.IdDomicilio = domicilioFacturacion.IdDomicilio;
            db.DatosFacturaciones.Add(datosFacturacion);
            db.SaveChanges();

            //Mapeo datos Suscripción
            suscripcion.FechaAlta = DateTime.Now;
            suscripcion.TipoSuscripcion = suscripcionVM.TipoSuscripcion;

            var nrosuscripcion = 1;
            try
            {
                nrosuscripcion = db.Suscripciones.ToList().LastOrDefault().NumeroSuscripcion + 1;
            }
            catch (Exception)
            {
            }
            suscripcion.NumeroSuscripcion = nrosuscripcion;

            //Mapeo relaciones

            //suscripcion.IdCanillita = suscripcionVM.IdCanillita;
            suscripcion.IdSuscriptor = suscriptor.IdSuscriptor;
            suscripcion.IdOferta = suscripcionVM.IdOferta;
            suscripcion.IdDatosFacturacion = datosFacturacion.IdDatosFacturacion;
            suscripcion.TipoSuscripcion = TipoSuscripcion.Coorporativo;
            suscripcion.Lunes = suscripcionVM.Lunes;
            suscripcion.Martes = suscripcionVM.Martes;
            suscripcion.Miercoles = suscripcionVM.Miercoles;
            suscripcion.Jueves = suscripcionVM.Jueves;
            suscripcion.Viernes = suscripcionVM.Viernes;
            suscripcion.Sabado = suscripcionVM.Sabado;
            suscripcion.Domingo = suscripcionVM.Domingo;
            suscripcion.CantTarjetasExtras = suscripcionVM.CantTarjetasExtras;
            suscripcion.PrecioSuscripcion = suscripcionVM.PrecioSuscripcion;

            try
            {
                db.Suscripciones.Add(suscripcion);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                status = false;
            }


            //MAPEO y GUARDADO de DOMICILIOS y DIAS DE ENTREGA//
            //Mapeo Domicilio lunes
            Domicilio domicilioLunes = new Domicilio();
            DiaEntrega diaentregaLunes = new DiaEntrega();
            if (suscripcionVM.Lunes)
            {
                if ((!String.IsNullOrWhiteSpace(suscripcionVM.CalleLunes) || String.IsNullOrWhiteSpace(suscripcionVM.BarrioLunes) &
                     suscripcionVM.AlturaLunes != null))
                {
                    domicilioLunes.Calle = suscripcionVM.CalleLunes;
                    domicilioLunes.Altura = suscripcionVM.AlturaLunes;
                    domicilioLunes.Piso = suscripcionVM.PisoLunes;
                    domicilioLunes.Departamento = suscripcionVM.DepartamentoLunes;
                    domicilioLunes.CalleLateral1 = suscripcionVM.CalleLateral1Lunes;
                    domicilioLunes.CalleLateral2 = suscripcionVM.CalleLateral2Lunes;
                    domicilioLunes.Barrio = suscripcionVM.BarrioLunes;
                    domicilioLunes.IdLocalidad = suscripcionVM.IdLocalidadLunes.GetValueOrDefault();
                    domicilioLunes.Observaciones = suscripcionVM.ObservacionesDomicilioLunes;
                }
                else
                {
                    domicilioLunes.Calle = suscripcionVM.CalleSuscriptor;
                    domicilioLunes.Altura = suscripcionVM.AlturaSuscriptor;
                    domicilioLunes.Piso = suscripcionVM.PisoSuscriptor;
                    domicilioLunes.Departamento = suscripcionVM.DepartamentoSuscriptor;
                    domicilioLunes.CalleLateral1 = suscripcionVM.CalleLateral1Suscriptor;
                    domicilioLunes.CalleLateral2 = suscripcionVM.CalleLateral2Suscriptor;
                    domicilioLunes.Barrio = suscripcionVM.BarrioSuscriptor;
                    domicilioLunes.IdLocalidad = suscripcionVM.IdLocalidadSuscriptor;
                    domicilioLunes.Observaciones = suscripcionVM.ObservacionesDomicilioSuscriptor;
                }
                domicilioLunes.TituloDom = "Domicilio entrega - Lunes";

                db.Domicilios.Add(domicilioLunes);
                db.SaveChanges();
                diaentregaLunes.IdDomicilio = domicilioLunes.IdDomicilio;
                diaentregaLunes.Habilitado = true;
                diaentregaLunes.NombreDia = NombreDia.Lunes;
                db.DiaEntregas.Add(diaentregaLunes);
                db.SaveChanges();
                suscripcion.DiaEntregas.Add(diaentregaLunes);
                db.SaveChanges();

            }

            //Mapeo Domicilio Martes
            Domicilio domicilioMartes = new Domicilio();
            DiaEntrega diaentregaMartes = new DiaEntrega();

            if (suscripcionVM.Martes)
            {
                if ((!String.IsNullOrWhiteSpace(suscripcionVM.CalleMartes) || String.IsNullOrWhiteSpace(suscripcionVM.BarrioMartes) &
                     suscripcionVM.AlturaMartes != null))
                {
                    domicilioMartes.Calle = suscripcionVM.CalleMartes;
                    domicilioMartes.Altura = suscripcionVM.AlturaMartes;
                    domicilioMartes.Piso = suscripcionVM.PisoMartes;
                    domicilioMartes.Departamento = suscripcionVM.DepartamentoMartes;
                    domicilioMartes.CalleLateral1 = suscripcionVM.CalleLateral1Martes;
                    domicilioMartes.CalleLateral2 = suscripcionVM.CalleLateral2Martes;
                    domicilioMartes.Barrio = suscripcionVM.BarrioMartes;
                    domicilioMartes.IdLocalidad = suscripcionVM.IdLocalidadMartes.GetValueOrDefault();
                    domicilioMartes.Observaciones = suscripcionVM.ObservacionesDomicilioMartes;
                }
                else
                {
                    domicilioMartes.Calle = suscripcionVM.CalleSuscriptor;
                    domicilioMartes.Altura = suscripcionVM.AlturaSuscriptor;
                    domicilioMartes.Piso = suscripcionVM.PisoSuscriptor;
                    domicilioMartes.Departamento = suscripcionVM.DepartamentoSuscriptor;
                    domicilioMartes.CalleLateral1 = suscripcionVM.CalleLateral1Suscriptor;
                    domicilioMartes.CalleLateral2 = suscripcionVM.CalleLateral2Suscriptor;
                    domicilioMartes.Barrio = suscripcionVM.BarrioSuscriptor;
                    domicilioMartes.IdLocalidad = suscripcionVM.IdLocalidadSuscriptor;
                    domicilioMartes.Observaciones = suscripcionVM.ObservacionesDomicilioSuscriptor;
                }
                domicilioMartes.TituloDom = "Domicilio entrega - Martes";

                db.Domicilios.Add(domicilioMartes);
                db.SaveChanges();
                diaentregaMartes.IdDomicilio = domicilioMartes.IdDomicilio;
                diaentregaMartes.Habilitado = true;
                diaentregaMartes.NombreDia = NombreDia.Martes;
                db.DiaEntregas.Add(diaentregaMartes);
                db.SaveChanges();
                suscripcion.DiaEntregas.Add(diaentregaMartes);
                db.SaveChanges();

            }


            //Mapeo Domicilio Miercoles
            Domicilio domicilioMiercoles = new Domicilio();
            DiaEntrega diaentregaMiercoles = new DiaEntrega();

            if (suscripcionVM.Miercoles)
            {
                if ((!String.IsNullOrWhiteSpace(suscripcionVM.CalleMiercoles) || String.IsNullOrWhiteSpace(suscripcionVM.BarrioMiercoles) &
                     suscripcionVM.AlturaMiercoles != null))
                {
                    domicilioMiercoles.Calle = suscripcionVM.CalleMiercoles;
                    domicilioMiercoles.Altura = suscripcionVM.AlturaMiercoles;
                    domicilioMiercoles.Piso = suscripcionVM.PisoMiercoles;
                    domicilioMiercoles.Departamento = suscripcionVM.DepartamentoMiercoles;
                    domicilioMiercoles.CalleLateral1 = suscripcionVM.CalleLateral1Miercoles;
                    domicilioMiercoles.CalleLateral2 = suscripcionVM.CalleLateral2Miercoles;
                    domicilioMiercoles.Barrio = suscripcionVM.BarrioMiercoles;
                    domicilioMiercoles.IdLocalidad = suscripcionVM.IdLocalidadMiercoles.GetValueOrDefault();
                    domicilioMiercoles.Observaciones = suscripcionVM.ObservacionesDomicilioMiercoles;
                }
                else
                {
                    domicilioMiercoles.Calle = suscripcionVM.CalleSuscriptor;
                    domicilioMiercoles.Altura = suscripcionVM.AlturaSuscriptor;
                    domicilioMiercoles.Piso = suscripcionVM.PisoSuscriptor;
                    domicilioMiercoles.Departamento = suscripcionVM.DepartamentoSuscriptor;
                    domicilioMiercoles.CalleLateral1 = suscripcionVM.CalleLateral1Suscriptor;
                    domicilioMiercoles.CalleLateral2 = suscripcionVM.CalleLateral2Suscriptor;
                    domicilioMiercoles.Barrio = suscripcionVM.BarrioSuscriptor;
                    domicilioMiercoles.IdLocalidad = suscripcionVM.IdLocalidadSuscriptor;
                    domicilioMiercoles.Observaciones = suscripcionVM.ObservacionesDomicilioSuscriptor;
                }
                domicilioMiercoles.TituloDom = "Domicilio entrega - Miércoles";

                db.Domicilios.Add(domicilioMiercoles);
                db.SaveChanges();
                diaentregaMiercoles.IdDomicilio = domicilioMiercoles.IdDomicilio;
                diaentregaMiercoles.Habilitado = true;
                diaentregaMiercoles.NombreDia = NombreDia.Miercoles;
                db.DiaEntregas.Add(diaentregaMiercoles);
                db.SaveChanges();
                suscripcion.DiaEntregas.Add(diaentregaMiercoles);
                db.SaveChanges();

            }


            //Mapeo Domicilio Jueves
            Domicilio domicilioJueves = new Domicilio();
            DiaEntrega diaentregaJueves = new DiaEntrega();

            if (suscripcionVM.Jueves)
            {
                if ((!String.IsNullOrWhiteSpace(suscripcionVM.CalleJueves) || String.IsNullOrWhiteSpace(suscripcionVM.BarrioJueves) &
                     suscripcionVM.AlturaJueves != null))
                {
                    domicilioJueves.Calle = suscripcionVM.CalleJueves;
                    domicilioJueves.Altura = suscripcionVM.AlturaJueves;
                    domicilioJueves.Piso = suscripcionVM.PisoJueves;
                    domicilioJueves.Departamento = suscripcionVM.DepartamentoJueves;
                    domicilioJueves.CalleLateral1 = suscripcionVM.CalleLateral1Jueves;
                    domicilioJueves.CalleLateral2 = suscripcionVM.CalleLateral2Jueves;
                    domicilioJueves.Barrio = suscripcionVM.BarrioJueves;
                    domicilioJueves.IdLocalidad = suscripcionVM.IdLocalidadJueves.GetValueOrDefault();
                    domicilioJueves.Observaciones = suscripcionVM.ObservacionesDomicilioJueves;
                }
                else
                {
                    domicilioJueves.Calle = suscripcionVM.CalleSuscriptor;
                    domicilioJueves.Altura = suscripcionVM.AlturaSuscriptor;
                    domicilioJueves.Piso = suscripcionVM.PisoSuscriptor;
                    domicilioJueves.Departamento = suscripcionVM.DepartamentoSuscriptor;
                    domicilioJueves.CalleLateral1 = suscripcionVM.CalleLateral1Suscriptor;
                    domicilioJueves.CalleLateral2 = suscripcionVM.CalleLateral2Suscriptor;
                    domicilioJueves.Barrio = suscripcionVM.BarrioSuscriptor;
                    domicilioJueves.IdLocalidad = suscripcionVM.IdLocalidadSuscriptor;
                    domicilioJueves.Observaciones = suscripcionVM.ObservacionesDomicilioSuscriptor;
                }
                domicilioJueves.TituloDom = "Domicilio entrega - Jueves";

                db.Domicilios.Add(domicilioJueves);
                db.SaveChanges();
                diaentregaJueves.IdDomicilio = domicilioJueves.IdDomicilio;
                diaentregaJueves.Habilitado = true;
                diaentregaJueves.NombreDia = NombreDia.Jueves;
                db.DiaEntregas.Add(diaentregaJueves);
                db.SaveChanges();
                suscripcion.DiaEntregas.Add(diaentregaJueves);
                db.SaveChanges();

            }


            //Mapeo Domicilio Viernes
            Domicilio domicilioViernes = new Domicilio();
            DiaEntrega diaentregaViernes = new DiaEntrega();

            if (suscripcionVM.Viernes)
            {
                if ((!String.IsNullOrWhiteSpace(suscripcionVM.CalleViernes) || String.IsNullOrWhiteSpace(suscripcionVM.BarrioViernes) &
                     suscripcionVM.AlturaViernes != null))
                {
                    domicilioViernes.Calle = suscripcionVM.CalleViernes;
                    domicilioViernes.Altura = suscripcionVM.AlturaViernes;
                    domicilioViernes.Piso = suscripcionVM.PisoViernes;
                    domicilioViernes.Departamento = suscripcionVM.DepartamentoViernes;
                    domicilioViernes.CalleLateral1 = suscripcionVM.CalleLateral1Viernes;
                    domicilioViernes.CalleLateral2 = suscripcionVM.CalleLateral2Viernes;
                    domicilioViernes.Barrio = suscripcionVM.BarrioViernes;
                    domicilioViernes.IdLocalidad = suscripcionVM.IdLocalidadViernes.GetValueOrDefault();
                    domicilioViernes.Observaciones = suscripcionVM.ObservacionesDomicilioViernes;
                }
                else
                {
                    domicilioViernes.Calle = suscripcionVM.CalleSuscriptor;
                    domicilioViernes.Altura = suscripcionVM.AlturaSuscriptor;
                    domicilioViernes.Piso = suscripcionVM.PisoSuscriptor;
                    domicilioViernes.Departamento = suscripcionVM.DepartamentoSuscriptor;
                    domicilioViernes.CalleLateral1 = suscripcionVM.CalleLateral1Suscriptor;
                    domicilioViernes.CalleLateral2 = suscripcionVM.CalleLateral2Suscriptor;
                    domicilioViernes.Barrio = suscripcionVM.BarrioSuscriptor;
                    domicilioViernes.IdLocalidad = suscripcionVM.IdLocalidadSuscriptor;
                    domicilioViernes.Observaciones = suscripcionVM.ObservacionesDomicilioSuscriptor;
                }
                domicilioViernes.TituloDom = "Domicilio entrega - Viernes";

                db.Domicilios.Add(domicilioViernes);
                db.SaveChanges();
                diaentregaViernes.IdDomicilio = domicilioViernes.IdDomicilio;
                diaentregaViernes.Habilitado = true;
                diaentregaViernes.NombreDia = NombreDia.Viernes;
                db.DiaEntregas.Add(diaentregaViernes);
                db.SaveChanges();
                suscripcion.DiaEntregas.Add(diaentregaViernes);
                db.SaveChanges();
            }


            //Mapeo Domicilio Sabado
            Domicilio domicilioSabado = new Domicilio();
            DiaEntrega diaentregaSabado = new DiaEntrega();

            if (suscripcionVM.Sabado)
            {
                if ((!String.IsNullOrWhiteSpace(suscripcionVM.CalleSabado) || String.IsNullOrWhiteSpace(suscripcionVM.BarrioSabado) &
                     suscripcionVM.AlturaSabado != null))
                {
                    domicilioSabado.Calle = suscripcionVM.CalleSabado;
                    domicilioSabado.Altura = suscripcionVM.AlturaSabado;
                    domicilioSabado.Piso = suscripcionVM.PisoSabado;
                    domicilioSabado.Departamento = suscripcionVM.DepartamentoSabado;
                    domicilioSabado.CalleLateral1 = suscripcionVM.CalleLateral1Sabado;
                    domicilioSabado.CalleLateral2 = suscripcionVM.CalleLateral2Sabado;
                    domicilioSabado.Barrio = suscripcionVM.BarrioSabado;
                    domicilioSabado.IdLocalidad = suscripcionVM.IdLocalidadSabado.GetValueOrDefault();
                    domicilioSabado.Observaciones = suscripcionVM.ObservacionesDomicilioSabado;
                }
                else
                {
                    domicilioSabado.Calle = suscripcionVM.CalleSuscriptor;
                    domicilioSabado.Altura = suscripcionVM.AlturaSuscriptor;
                    domicilioSabado.Piso = suscripcionVM.PisoSuscriptor;
                    domicilioSabado.Departamento = suscripcionVM.DepartamentoSuscriptor;
                    domicilioSabado.CalleLateral1 = suscripcionVM.CalleLateral1Suscriptor;
                    domicilioSabado.CalleLateral2 = suscripcionVM.CalleLateral2Suscriptor;
                    domicilioSabado.Barrio = suscripcionVM.BarrioSuscriptor;
                    domicilioSabado.IdLocalidad = suscripcionVM.IdLocalidadSuscriptor;
                    domicilioSabado.Observaciones = suscripcionVM.ObservacionesDomicilioSuscriptor;
                }
                domicilioSabado.TituloDom = "Domicilio entrega - Sábado";

                db.Domicilios.Add(domicilioSabado);
                db.SaveChanges();
                diaentregaSabado.IdDomicilio = domicilioSabado.IdDomicilio;
                diaentregaSabado.Habilitado = true;
                diaentregaSabado.NombreDia = NombreDia.Sabado;
                db.DiaEntregas.Add(diaentregaSabado);
                db.SaveChanges();
                suscripcion.DiaEntregas.Add(diaentregaSabado);
                db.SaveChanges();
            }

            //Mapeo Domicilio Domingo
            Domicilio domicilioDomingo = new Domicilio();
            DiaEntrega diaentregaDomingo = new DiaEntrega();

            if ((!String.IsNullOrWhiteSpace(suscripcionVM.CalleDomingo) || String.IsNullOrWhiteSpace(suscripcionVM.BarrioDomingo) &
                   suscripcionVM.AlturaDomingo != null))
            {
                domicilioDomingo.Calle = suscripcionVM.CalleDomingo;
                domicilioDomingo.Altura = suscripcionVM.AlturaDomingo;
                domicilioDomingo.Piso = suscripcionVM.PisoDomingo;
                domicilioDomingo.Departamento = suscripcionVM.DepartamentoDomingo;
                domicilioDomingo.CalleLateral1 = suscripcionVM.CalleLateral1Domingo;
                domicilioDomingo.CalleLateral2 = suscripcionVM.CalleLateral2Domingo;
                domicilioDomingo.Barrio = suscripcionVM.BarrioDomingo;
                domicilioDomingo.IdLocalidad = suscripcionVM.IdLocalidadDomingo.GetValueOrDefault();
                domicilioDomingo.Observaciones = suscripcionVM.ObservacionesDomicilioDomingo;
            }
            else
            {
                domicilioDomingo.Calle = suscripcionVM.CalleSuscriptor;
                domicilioDomingo.Altura = suscripcionVM.AlturaSuscriptor;
                domicilioDomingo.Piso = suscripcionVM.PisoSuscriptor;
                domicilioDomingo.Departamento = suscripcionVM.DepartamentoSuscriptor;
                domicilioDomingo.CalleLateral1 = suscripcionVM.CalleLateral1Suscriptor;
                domicilioDomingo.CalleLateral2 = suscripcionVM.CalleLateral2Suscriptor;
                domicilioDomingo.Barrio = suscripcionVM.BarrioSuscriptor;
                domicilioDomingo.IdLocalidad = suscripcionVM.IdLocalidadSuscriptor;
                domicilioDomingo.Observaciones = suscripcionVM.ObservacionesDomicilioSuscriptor;
            }
            domicilioDomingo.TituloDom = "Domicilio entrega - Domingo";

            db.Domicilios.Add(domicilioDomingo);
            db.SaveChanges();
            diaentregaDomingo.IdDomicilio = domicilioDomingo.IdDomicilio;
            diaentregaDomingo.Habilitado = true;
            diaentregaDomingo.NombreDia = NombreDia.Domingo;
            db.DiaEntregas.Add(diaentregaDomingo);
            db.SaveChanges();
            suscripcion.DiaEntregas.Add(diaentregaDomingo);
            db.SaveChanges();


            //creación Tarjetas club
            var tarjetaClubprincipal = new TarjetaClub();
            tarjetaClubprincipal.Nombres = suscripcionVM.Nombre;
            tarjetaClubprincipal.Apellido = suscripcionVM.Apellido;
            tarjetaClubprincipal.DNI = suscripcionVM.DNI;
            if (IsValidSqlDateTime(suscripcionVM.FechaNacimiento)) { tarjetaClubprincipal.FechaNacimiento = suscripcionVM.FechaNacimiento; }

            tarjetaClubprincipal.Email = suscripcionVM.Email;
            tarjetaClubprincipal.TelefonoMovilArea = suscripcionVM.TelefonoMovilArea;
            tarjetaClubprincipal.TelefonoMovilNumero = suscripcionVM.TelefonoMovilNumero;
            tarjetaClubprincipal.FechaSolicitudImpresion = DateTime.Now;
            tarjetaClubprincipal.EstadoTarjetaClub = EstadoTarjetaClub.DerivadaAImpresion;
            tarjetaClubprincipal.Numero = db.TarjetaClubes.Count() + 1;


            tarjetaClubprincipal.TipoTarjetaClub = TipoTarjetaClub.Titular;
            tarjetaClubprincipal.TipoSexo = suscripcionVM.TipoSexoTarjetaClub;

            tarjetaClubprincipal.IdSuscripcion = suscripcion.IdSuscripcion;
            db.TarjetaClubes.Add(tarjetaClubprincipal);
            db.SaveChanges();
            try
            {
                if (suscriptoressecundarios != null)
                {
                    foreach (var sc in suscriptoressecundarios)

                    {
                        //Se agregan los suscriptores secundarios
                        //y los dias de entrega de cada suscriptor,por defecto se cargan los de la empresa
                        SuscriptorSecundario suscriptorsecundario = new SuscriptorSecundario();
                        suscriptorsecundario.IdSuscriptor = Int32.Parse(sc);
                        suscriptorsecundario.DiaEntregas = new List<DiaEntrega>();
                        foreach (var diaentregasecundario in suscripcion.DiaEntregas)
                        {
                            DiaEntrega diasecundario = new DiaEntrega();
                            Domicilio domiciliosecundario = new Domicilio();
                            domiciliosecundario.Calle = diaentregasecundario.Domicilio.Calle;
                            domiciliosecundario.Altura = diaentregasecundario.Domicilio.Altura;
                            domiciliosecundario.Piso = diaentregasecundario.Domicilio.Piso;
                            domiciliosecundario.Departamento = diaentregasecundario.Domicilio.Departamento;
                            domiciliosecundario.CalleLateral1 = diaentregasecundario.Domicilio.CalleLateral1;
                            domiciliosecundario.CalleLateral2 = diaentregasecundario.Domicilio.CalleLateral2;
                            domiciliosecundario.Barrio = diaentregasecundario.Domicilio.Barrio;
                            domiciliosecundario.IdLocalidad = diaentregasecundario.Domicilio.IdLocalidad;
                            domiciliosecundario.Observaciones = diaentregasecundario.Domicilio.Observaciones;
                            domiciliosecundario.TituloDom = diaentregasecundario.Domicilio.TituloDom;

                            db.Domicilios.Add(domiciliosecundario);
                            db.SaveChanges();
                            diasecundario.IdDomicilio = domiciliosecundario.IdDomicilio;
                            diasecundario.Habilitado = diaentregasecundario.Habilitado;
                            diasecundario.NombreDia = diaentregasecundario.NombreDia;
                            db.DiaEntregas.Add(diasecundario);
                            db.SaveChanges();
                            suscriptorsecundario.DiaEntregas.Add(diasecundario);
                            db.SaveChanges();

                        }
                        //se guarda la tarjetaclub secundarias
                        Suscriptor suscsec = new Suscriptor();
                        suscsec = db.Suscriptores.Find(Int32.Parse(sc));
                        var tarjetaClub = new TarjetaClub();
                        tarjetaClub.Nombres = suscsec.Nombre;
                        tarjetaClub.Apellido = suscsec.Apellido;
                        tarjetaClub.DNI = suscsec.DNI;
                        if (IsValidSqlDateTime(suscsec.FechaNacimiento)) { tarjetaClub.FechaNacimiento = suscsec.FechaNacimiento; }
                        tarjetaClub.Email = suscsec.Email;
                        tarjetaClub.TelefonoMovilArea = suscsec.TelefonoMovilArea;
                        tarjetaClub.TelefonoMovilNumero = suscsec.TelefonoMovilNumero;
                        tarjetaClub.FechaSolicitudImpresion = DateTime.Now;
                        tarjetaClub.EstadoTarjetaClub = EstadoTarjetaClub.DerivadaAImpresion;
                        tarjetaClub.Numero = db.TarjetaClubes.Count() + 1;
                        tarjetaClub.TipoTarjetaClub = TipoTarjetaClub.Adicional;
                        tarjetaClub.IdSuscripcion = suscripcion.IdSuscripcion;
                        tarjetaClub.TipoSexo = suscsec.TipoSexo;
                        db.TarjetaClubes.Add(tarjetaClub);
                        db.SaveChanges();
                        //fin guardar tarjetaclub

                        //se guarda el suscriptorsecundario con la relacion a tarjetaclub
                        //y se lo agrega a la suscripcion
                        suscriptorsecundario.IdTarjetaClub = tarjetaClub.IdTarjetaClub;
                        db.SuscriptorSecundarios.Add(suscriptorsecundario);
                        db.SaveChanges();
                        suscripcion.SuscriptorSecundarios.Add(suscriptorsecundario);
                        db.SaveChanges();
                        //fin guardar suscriptorsecundario

                    }
                }

            }
            catch (Exception e)
            {

            }

            //Historial estado suscripción
            EstadoSuscripcionHistorial esh = new EstadoSuscripcionHistorial
            {
                IdSuscripcion = suscripcion.IdSuscripcion,
                EstadoSuscripcion = EstadoSuscripcion.Activa,
                FechaModificacion = DateTime.Now,
                MotivoEstadoSuscripcion = 0,
                Responsable = "admin (modif. controller)" //HttpContext.User.Identity.Name

            };
            db.EstadoSuscripcionHistoriales.Add(esh);
            db.SaveChanges();

            if (status)
            {
                if (suscripcion.TipoSuscripcion == TipoSuscripcion.Coorporativo)
                {
                    Oferta ofer = new Oferta();
                    ofer = db.Ofertas.Find(suscripcionVM.IdOferta);
                    if (ofer.TipoOferta == TipoOferta.Coorporativa)
                    {
                        ofer.TipoOferta = TipoOferta.CoorporativaAsignada;
                    }
                    db.Entry(ofer).State = EntityState.Modified;
                }

                db.SaveChanges();


                return RedirectToAction("ListadoCoorporativa");
            }
            else
            {
                ViewBag.IdOferta = new SelectList(db.Ofertas, "IdOferta", "Nombre", suscripcion.IdOferta);
                ViewBag.IdSuscriptor = new SelectList(db.Suscriptores, "IdSuscriptor", "Nombre", suscripcion.IdSuscriptor);
                return View(suscripcionVM);
            }

        }



        #endregion

        #region CreateCortesia
        // GET: Suscripciones/Create
        public ActionResult CreateCortesia(int? id)
        {
            if (id != null)
            {
                ViewBag.idsus = id;
            }
            ViewBag.IdOferta = db.Ofertas.ToList().FindAll(x => x.TipoOferta == TipoOferta.Estandar);
            ViewBag.Suscriptores = db.Suscriptores.ToList().FindAll(x => x.TipoSuscriptor != TipoSuscriptor.Coorporativo);



            ViewBag.provincias = db.Provincias.ToList();
            ViewBag.localidades = db.Localidades.ToList();
            ViewBag.dateNow = DateTime.Now.GetDateTimeFormats()[7];
            ViewBag.ofertasChaco = db.Ofertas.ToList().FindAll(o => o.Editorial == Editorial.Chaco & o.TipoOferta == TipoOferta.Estandar & o.EstadoOferta.Descripcion == "Activa");
            ViewBag.ofertasCtes = db.Ofertas.ToList().FindAll(o => o.Editorial == Editorial.Corrientes & o.TipoOferta == TipoOferta.Estandar & o.EstadoOferta.Descripcion == "Activa");
            return View();
        }
        // POST: Suscripciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCortesia(SuscripcionVM suscripcionVM, string[] NombresTarjetaClub, string[] ApellidoTarjetaClub, int[] DNITarjetaClub, string[] TelefonoMovilNumeroTarjetaClub, string[] TelefonoMovilAreaTarjetaClub, string[] EmailTarjetaClub, DateTime[] FechaNacimientoTarjetaClub, TipoSexo[] TipoSexoTarjetaClub)
        {
            suscripcionVM.Domingo = true;
            Suscripcion suscripcion = new Suscripcion();
            suscripcion.DiaEntregas = new List<DiaEntrega>();
            Suscriptor suscriptor = new Suscriptor();
            Domicilio domicilio = new Domicilio();
            //Mapeo suscriptor
            var nrosuscriptor = 0;
            try
            {
                nrosuscriptor = db.Suscriptores.Count() + 2000;
            }
            catch (Exception)
            {
                nrosuscriptor = 1;


            }

            bool status = true;


            //SI EL SUSC NO EXISTE SOLO GUARDO
            if (suscripcionVM.IdSuscriptor == null)
            {
                suscriptor.Nombre = suscripcionVM.Nombre;
                suscriptor.Apellido = suscripcionVM.Apellido;
                suscriptor.DNI = suscripcionVM.DNI;
                suscriptor.FechaNacimiento = suscripcionVM.FechaNacimiento;
                suscriptor.NumeroSuscriptor = nrosuscriptor;
                suscriptor.TipoSexo = suscripcionVM.TipoSexo;
                suscriptor.Email = suscripcionVM.Email;
                suscriptor.TelefonoFijoNumero = suscripcionVM.TelefonoFijoNumero;
                suscriptor.TelefonoFijoArea = suscripcionVM.TelefonoFijoArea;
                suscriptor.TelefonoMovilNumero = suscripcionVM.TelefonoMovilNumero;
                suscriptor.TelefonoMovilArea = suscripcionVM.TelefonoMovilArea;
                suscriptor.CUIT = suscripcionVM.CUIT;
                suscriptor.RazonSocial = suscripcionVM.RazonSocial;
                suscriptor.TipoSuscriptor = TipoSuscriptor.Normal;
                //Mapeo Domicilio suscriptor
                domicilio.Calle = suscripcionVM.CalleSuscriptor;
                domicilio.Altura = suscripcionVM.AlturaSuscriptor;
                domicilio.Piso = suscripcionVM.PisoSuscriptor;
                domicilio.Departamento = suscripcionVM.DepartamentoSuscriptor;
                domicilio.CalleLateral1 = suscripcionVM.CalleLateral1Suscriptor;
                domicilio.CalleLateral2 = suscripcionVM.CalleLateral2Suscriptor;
                domicilio.Barrio = suscripcionVM.BarrioSuscriptor;
                domicilio.IdLocalidad = suscripcionVM.IdLocalidadSuscriptor;
                domicilio.Observaciones = suscripcionVM.ObservacionesDomicilioSuscriptor;
                domicilio.TituloDom = "Domicilio principal";
                db.Domicilios.Add(domicilio);
                db.SaveChanges();

                //agrego domicilio al susc y guardo el suscriptor
                suscriptor.IdDomicilio = domicilio.IdDomicilio;
                db.Suscriptores.Add(suscriptor);
                db.SaveChanges();
            }
            else
            {//SI EL SUSC EXISTE SE MODIFICA el domicilio y los datos del suscriptor

                suscriptor = db.Suscriptores.Find(suscripcionVM.IdSuscriptor.Value);
                domicilio = db.Domicilios.Find(suscriptor.IdDomicilio);
                suscriptor.Nombre = suscripcionVM.Nombre;
                suscriptor.Apellido = suscripcionVM.Apellido;
                suscriptor.DNI = suscripcionVM.DNI;
                suscriptor.FechaNacimiento = suscripcionVM.FechaNacimiento;
                suscriptor.NumeroSuscriptor = nrosuscriptor;
                suscriptor.TipoSexo = suscripcionVM.TipoSexo;
                suscriptor.Email = suscripcionVM.Email;
                suscriptor.TelefonoFijoNumero = suscripcionVM.TelefonoFijoNumero;
                suscriptor.TelefonoFijoArea = suscripcionVM.TelefonoFijoArea;
                suscriptor.TelefonoMovilNumero = suscripcionVM.TelefonoMovilNumero;
                suscriptor.TelefonoMovilArea = suscripcionVM.TelefonoMovilArea;
                suscriptor.CUIT = suscripcionVM.CUIT;
                suscriptor.RazonSocial = suscripcionVM.RazonSocial;

                //Mapeo Domicilio suscriptor


                domicilio.Calle = suscripcionVM.CalleSuscriptor;
                domicilio.Altura = suscripcionVM.AlturaSuscriptor;
                domicilio.Piso = suscripcionVM.PisoSuscriptor;
                domicilio.Departamento = suscripcionVM.DepartamentoSuscriptor;
                domicilio.CalleLateral1 = suscripcionVM.CalleLateral1Suscriptor;
                domicilio.CalleLateral2 = suscripcionVM.CalleLateral2Suscriptor;
                domicilio.Barrio = suscripcionVM.BarrioSuscriptor;
                domicilio.IdLocalidad = suscripcionVM.IdLocalidadSuscriptor;
                domicilio.Observaciones = suscripcionVM.ObservacionesDomicilioSuscriptor;
                domicilio.TituloDom = "Domicilio principal";

                db.Entry(domicilio).State = EntityState.Modified;
                db.SaveChanges();

                //agrego domicilio al susc y guardo el suscriptor
                suscriptor.IdDomicilio = domicilio.IdDomicilio;
                suscriptor.IdSuscriptor = suscripcionVM.IdSuscriptor.GetValueOrDefault();
                db.Entry(suscriptor).State = EntityState.Modified;
                db.SaveChanges();
            }

            //Mapeo domicilio Facturación
            Domicilio domicilioFacturacion = new Domicilio();
            if (suscripcionVM.CalleFacturacion != null & suscripcionVM.AlturaFacturacion != null)
            {
                domicilioFacturacion.Calle = suscripcionVM.CalleFacturacion;
                domicilioFacturacion.Altura = suscripcionVM.AlturaFacturacion.GetValueOrDefault(); ;
                domicilioFacturacion.Piso = suscripcionVM.PisoFacturacion;
                domicilioFacturacion.CalleLateral1 = suscripcionVM.CalleLateral1Facturacion;
                domicilioFacturacion.CalleLateral2 = suscripcionVM.CalleLateral2Facturacion;
                domicilioFacturacion.Departamento = suscripcionVM.DepartamentoFacturacion;
                domicilioFacturacion.Barrio = suscripcionVM.BarrioFacturacion;
                domicilioFacturacion.IdLocalidad = suscripcionVM.IdLocalidadFacturacion.GetValueOrDefault();
                domicilioFacturacion.Observaciones = suscripcionVM.ObservacionesDomicilioFacturacion;
                domicilioFacturacion.TituloDom = "Domicilio facturación";
                db.Domicilios.Add(domicilioFacturacion);
                db.SaveChanges();
            }
            else
            {
                domicilioFacturacion = domicilio;
            }
            //Mapeo datos Facturación

            DatosFacturacion datosFacturacion = new DatosFacturacion();
            if (suscripcionVM.NombreFacturacion != null & suscripcionVM.ApellidoFacturacion != null
                & suscripcionVM.CUITFacturacion != null & suscripcionVM.RazonSocialFacturacion != null &
                suscripcionVM.DNIFacturacion != null)
            {
                datosFacturacion.Nombre = suscripcionVM.NombreFacturacion;
                datosFacturacion.Apellido = suscripcionVM.ApellidoFacturacion;
                datosFacturacion.DNI = (int)suscripcionVM.DNIFacturacion;
                datosFacturacion.CUIT = suscripcionVM.CUITFacturacion;
                datosFacturacion.RazonSocial = suscripcionVM.RazonSocialFacturacion;

            }
            else
            {
                datosFacturacion.Nombre = suscripcionVM.Nombre;
                datosFacturacion.Apellido = suscripcionVM.Apellido;
                datosFacturacion.DNI = suscripcionVM.DNI;
                datosFacturacion.CUIT = suscripcionVM.CUIT;
                datosFacturacion.RazonSocial = suscripcionVM.RazonSocial;
            }
            datosFacturacion.TipoIva = suscripcionVM.TipoIva;
            datosFacturacion.IdDomicilio = domicilioFacturacion.IdDomicilio;
            db.DatosFacturaciones.Add(datosFacturacion);
            db.SaveChanges();

            //Mapeo datos Suscripción
            suscripcion.FechaAlta = DateTime.Now;
            var nrosuscripcion = 1;
            try
            {
                nrosuscripcion = db.Suscripciones.ToList().LastOrDefault().NumeroSuscripcion + 1;
            }
            catch (Exception)
            {
            }
            suscripcion.NumeroSuscripcion = nrosuscripcion;

            //Mapeo relaciones

            //suscripcion.IdCanillita = suscripcionVM.IdCanillita;
            suscripcion.IdSuscriptor = suscriptor.IdSuscriptor;
            suscripcion.IdOferta = suscripcionVM.IdOferta;
            suscripcion.IdDatosFacturacion = datosFacturacion.IdDatosFacturacion;
            suscripcion.TipoSuscripcion = TipoSuscripcion.Cortesia;
            suscripcion.Lunes = suscripcionVM.Lunes;
            suscripcion.Martes = suscripcionVM.Martes;
            suscripcion.Miercoles = suscripcionVM.Miercoles;
            suscripcion.Jueves = suscripcionVM.Jueves;
            suscripcion.Viernes = suscripcionVM.Viernes;
            suscripcion.Sabado = suscripcionVM.Sabado;
            suscripcion.Domingo = suscripcionVM.Domingo;
            suscripcion.CantTarjetasExtras = suscripcionVM.CantTarjetasExtras;
            suscripcion.PrecioSuscripcion = suscripcionVM.PrecioSuscripcion;

            try
            {
                db.Suscripciones.Add(suscripcion);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                status = false;
            }




            //MAPEO y GUARDADO de DOMICILIOS y DIAS DE ENTREGA//
            //Mapeo Domicilio lunes
            Domicilio domicilioLunes = new Domicilio();
            DiaEntrega diaentregaLunes = new DiaEntrega();
            if (suscripcionVM.Lunes)
            {
                if ((!String.IsNullOrWhiteSpace(suscripcionVM.CalleLunes) || String.IsNullOrWhiteSpace(suscripcionVM.BarrioLunes) &
                     suscripcionVM.AlturaLunes != null))
                {
                    domicilioLunes.Calle = suscripcionVM.CalleLunes;
                    domicilioLunes.Altura = suscripcionVM.AlturaLunes;
                    domicilioLunes.Piso = suscripcionVM.PisoLunes;
                    domicilioLunes.Departamento = suscripcionVM.DepartamentoLunes;
                    domicilioLunes.CalleLateral1 = suscripcionVM.CalleLateral1Lunes;
                    domicilioLunes.CalleLateral2 = suscripcionVM.CalleLateral2Lunes;
                    domicilioLunes.Barrio = suscripcionVM.BarrioLunes;
                    domicilioLunes.IdLocalidad = suscripcionVM.IdLocalidadLunes.GetValueOrDefault();
                    domicilioLunes.Observaciones = suscripcionVM.ObservacionesDomicilioLunes;
                }
                else
                {
                    domicilioLunes.Calle = suscripcionVM.CalleSuscriptor;
                    domicilioLunes.Altura = suscripcionVM.AlturaSuscriptor;
                    domicilioLunes.Piso = suscripcionVM.PisoSuscriptor;
                    domicilioLunes.Departamento = suscripcionVM.DepartamentoSuscriptor;
                    domicilioLunes.CalleLateral1 = suscripcionVM.CalleLateral1Suscriptor;
                    domicilioLunes.CalleLateral2 = suscripcionVM.CalleLateral2Suscriptor;
                    domicilioLunes.Barrio = suscripcionVM.BarrioSuscriptor;
                    domicilioLunes.IdLocalidad = suscripcionVM.IdLocalidadSuscriptor;
                    domicilioLunes.Observaciones = suscripcionVM.ObservacionesDomicilioSuscriptor;
                }
                domicilioLunes.TituloDom = "Domicilio entrega - Lunes";

                db.Domicilios.Add(domicilioLunes);
                db.SaveChanges();
                diaentregaLunes.IdDomicilio = domicilioLunes.IdDomicilio;
                diaentregaLunes.Habilitado = true;
                diaentregaLunes.NombreDia = NombreDia.Lunes;
                db.DiaEntregas.Add(diaentregaLunes);
                db.SaveChanges();
                suscripcion.DiaEntregas.Add(diaentregaLunes);
                db.SaveChanges();
            }





            //Mapeo Domicilio Martes
            Domicilio domicilioMartes = new Domicilio();
            DiaEntrega diaentregaMartes = new DiaEntrega();

            if (suscripcionVM.Martes)
            {
                if ((!String.IsNullOrWhiteSpace(suscripcionVM.CalleMartes) || String.IsNullOrWhiteSpace(suscripcionVM.BarrioMartes) &
                     suscripcionVM.AlturaMartes != null))
                {
                    domicilioMartes.Calle = suscripcionVM.CalleMartes;
                    domicilioMartes.Altura = suscripcionVM.AlturaMartes;
                    domicilioMartes.Piso = suscripcionVM.PisoMartes;
                    domicilioMartes.Departamento = suscripcionVM.DepartamentoMartes;
                    domicilioMartes.CalleLateral1 = suscripcionVM.CalleLateral1Martes;
                    domicilioMartes.CalleLateral2 = suscripcionVM.CalleLateral2Martes;
                    domicilioMartes.Barrio = suscripcionVM.BarrioMartes;
                    domicilioMartes.IdLocalidad = suscripcionVM.IdLocalidadMartes.GetValueOrDefault();
                    domicilioMartes.Observaciones = suscripcionVM.ObservacionesDomicilioMartes;
                }
                else
                {
                    domicilioMartes.Calle = suscripcionVM.CalleSuscriptor;
                    domicilioMartes.Altura = suscripcionVM.AlturaSuscriptor;
                    domicilioMartes.Piso = suscripcionVM.PisoSuscriptor;
                    domicilioMartes.Departamento = suscripcionVM.DepartamentoSuscriptor;
                    domicilioMartes.CalleLateral1 = suscripcionVM.CalleLateral1Suscriptor;
                    domicilioMartes.CalleLateral2 = suscripcionVM.CalleLateral2Suscriptor;
                    domicilioMartes.Barrio = suscripcionVM.BarrioSuscriptor;
                    domicilioMartes.IdLocalidad = suscripcionVM.IdLocalidadSuscriptor;
                    domicilioMartes.Observaciones = suscripcionVM.ObservacionesDomicilioSuscriptor;
                }
                domicilioMartes.TituloDom = "Domicilio entrega - Martes";

                db.Domicilios.Add(domicilioMartes);
                db.SaveChanges();
                diaentregaMartes.IdDomicilio = domicilioMartes.IdDomicilio;
                diaentregaMartes.Habilitado = true;
                diaentregaMartes.NombreDia = NombreDia.Martes;
                db.DiaEntregas.Add(diaentregaMartes);
                db.SaveChanges();
                suscripcion.DiaEntregas.Add(diaentregaMartes);
                db.SaveChanges();
            }



            //Mapeo Domicilio Miercoles
            Domicilio domicilioMiercoles = new Domicilio();
            DiaEntrega diaentregaMiercoles = new DiaEntrega();

            if (suscripcionVM.Miercoles)
            {
                if ((!String.IsNullOrWhiteSpace(suscripcionVM.CalleMiercoles) || String.IsNullOrWhiteSpace(suscripcionVM.BarrioMiercoles) &
                     suscripcionVM.AlturaMiercoles != null))
                {
                    domicilioMiercoles.Calle = suscripcionVM.CalleMiercoles;
                    domicilioMiercoles.Altura = suscripcionVM.AlturaMiercoles;
                    domicilioMiercoles.Piso = suscripcionVM.PisoMiercoles;
                    domicilioMiercoles.Departamento = suscripcionVM.DepartamentoMiercoles;
                    domicilioMiercoles.CalleLateral1 = suscripcionVM.CalleLateral1Miercoles;
                    domicilioMiercoles.CalleLateral2 = suscripcionVM.CalleLateral2Miercoles;
                    domicilioMiercoles.Barrio = suscripcionVM.BarrioMiercoles;
                    domicilioMiercoles.IdLocalidad = suscripcionVM.IdLocalidadMiercoles.GetValueOrDefault();
                    domicilioMiercoles.Observaciones = suscripcionVM.ObservacionesDomicilioMiercoles;
                }
                else
                {
                    domicilioMiercoles.Calle = suscripcionVM.CalleSuscriptor;
                    domicilioMiercoles.Altura = suscripcionVM.AlturaSuscriptor;
                    domicilioMiercoles.Piso = suscripcionVM.PisoSuscriptor;
                    domicilioMiercoles.Departamento = suscripcionVM.DepartamentoSuscriptor;
                    domicilioMiercoles.CalleLateral1 = suscripcionVM.CalleLateral1Suscriptor;
                    domicilioMiercoles.CalleLateral2 = suscripcionVM.CalleLateral2Suscriptor;
                    domicilioMiercoles.Barrio = suscripcionVM.BarrioSuscriptor;
                    domicilioMiercoles.IdLocalidad = suscripcionVM.IdLocalidadSuscriptor;
                    domicilioMiercoles.Observaciones = suscripcionVM.ObservacionesDomicilioSuscriptor;
                }
                domicilioMiercoles.TituloDom = "Domicilio entrega - Miércoles";

                db.Domicilios.Add(domicilioMiercoles);
                db.SaveChanges();
                diaentregaMiercoles.IdDomicilio = domicilioMiercoles.IdDomicilio;
                diaentregaMiercoles.Habilitado = true;
                diaentregaMiercoles.NombreDia = NombreDia.Miercoles;
                db.DiaEntregas.Add(diaentregaMiercoles);
                db.SaveChanges();
                suscripcion.DiaEntregas.Add(diaentregaMiercoles);
                db.SaveChanges();
            }




            //Mapeo Domicilio Jueves
            Domicilio domicilioJueves = new Domicilio();
            DiaEntrega diaentregaJueves = new DiaEntrega();

            if (suscripcionVM.Jueves)
            {
                if ((!String.IsNullOrWhiteSpace(suscripcionVM.CalleJueves) || String.IsNullOrWhiteSpace(suscripcionVM.BarrioJueves) &
                     suscripcionVM.AlturaJueves != null))
                {
                    domicilioJueves.Calle = suscripcionVM.CalleJueves;
                    domicilioJueves.Altura = suscripcionVM.AlturaJueves;
                    domicilioJueves.Piso = suscripcionVM.PisoJueves;
                    domicilioJueves.Departamento = suscripcionVM.DepartamentoJueves;
                    domicilioJueves.CalleLateral1 = suscripcionVM.CalleLateral1Jueves;
                    domicilioJueves.CalleLateral2 = suscripcionVM.CalleLateral2Jueves;
                    domicilioJueves.Barrio = suscripcionVM.BarrioJueves;
                    domicilioJueves.IdLocalidad = suscripcionVM.IdLocalidadJueves.GetValueOrDefault();
                    domicilioJueves.Observaciones = suscripcionVM.ObservacionesDomicilioJueves;
                }
                else
                {
                    domicilioJueves.Calle = suscripcionVM.CalleSuscriptor;
                    domicilioJueves.Altura = suscripcionVM.AlturaSuscriptor;
                    domicilioJueves.Piso = suscripcionVM.PisoSuscriptor;
                    domicilioJueves.Departamento = suscripcionVM.DepartamentoSuscriptor;
                    domicilioJueves.CalleLateral1 = suscripcionVM.CalleLateral1Suscriptor;
                    domicilioJueves.CalleLateral2 = suscripcionVM.CalleLateral2Suscriptor;
                    domicilioJueves.Barrio = suscripcionVM.BarrioSuscriptor;
                    domicilioJueves.IdLocalidad = suscripcionVM.IdLocalidadSuscriptor;
                    domicilioJueves.Observaciones = suscripcionVM.ObservacionesDomicilioSuscriptor;
                }
                domicilioJueves.TituloDom = "Domicilio entrega - Jueves";

                db.Domicilios.Add(domicilioJueves);
                db.SaveChanges();
                diaentregaJueves.IdDomicilio = domicilioJueves.IdDomicilio;
                diaentregaJueves.Habilitado = true;
                diaentregaJueves.NombreDia = NombreDia.Jueves;
                db.DiaEntregas.Add(diaentregaJueves);
                db.SaveChanges();
                suscripcion.DiaEntregas.Add(diaentregaJueves);
                db.SaveChanges();
            }




            //Mapeo Domicilio Viernes
            Domicilio domicilioViernes = new Domicilio();
            DiaEntrega diaentregaViernes = new DiaEntrega();

            if (suscripcionVM.Viernes)
            {
                if ((!String.IsNullOrWhiteSpace(suscripcionVM.CalleViernes) || String.IsNullOrWhiteSpace(suscripcionVM.BarrioViernes) &
                     suscripcionVM.AlturaViernes != null))
                {
                    domicilioViernes.Calle = suscripcionVM.CalleViernes;
                    domicilioViernes.Altura = suscripcionVM.AlturaViernes;
                    domicilioViernes.Piso = suscripcionVM.PisoViernes;
                    domicilioViernes.Departamento = suscripcionVM.DepartamentoViernes;
                    domicilioViernes.CalleLateral1 = suscripcionVM.CalleLateral1Viernes;
                    domicilioViernes.CalleLateral2 = suscripcionVM.CalleLateral2Viernes;
                    domicilioViernes.Barrio = suscripcionVM.BarrioViernes;
                    domicilioViernes.IdLocalidad = suscripcionVM.IdLocalidadViernes.GetValueOrDefault();
                    domicilioViernes.Observaciones = suscripcionVM.ObservacionesDomicilioViernes;
                }
                else
                {
                    domicilioViernes.Calle = suscripcionVM.CalleSuscriptor;
                    domicilioViernes.Altura = suscripcionVM.AlturaSuscriptor;
                    domicilioViernes.Piso = suscripcionVM.PisoSuscriptor;
                    domicilioViernes.Departamento = suscripcionVM.DepartamentoSuscriptor;
                    domicilioViernes.CalleLateral1 = suscripcionVM.CalleLateral1Suscriptor;
                    domicilioViernes.CalleLateral2 = suscripcionVM.CalleLateral2Suscriptor;
                    domicilioViernes.Barrio = suscripcionVM.BarrioSuscriptor;
                    domicilioViernes.IdLocalidad = suscripcionVM.IdLocalidadSuscriptor;
                    domicilioViernes.Observaciones = suscripcionVM.ObservacionesDomicilioSuscriptor;
                }
                domicilioViernes.TituloDom = "Domicilio entrega - Viernes";

                db.Domicilios.Add(domicilioViernes);
                db.SaveChanges();
                diaentregaViernes.IdDomicilio = domicilioViernes.IdDomicilio;
                diaentregaViernes.Habilitado = true;
                diaentregaViernes.NombreDia = NombreDia.Viernes;
                db.DiaEntregas.Add(diaentregaViernes);
                db.SaveChanges();
                suscripcion.DiaEntregas.Add(diaentregaViernes);
                db.SaveChanges();
            }




            //Mapeo Domicilio Sabado
            Domicilio domicilioSabado = new Domicilio();
            DiaEntrega diaentregaSabado = new DiaEntrega();

            if (suscripcionVM.Sabado)
            {
                if ((!String.IsNullOrWhiteSpace(suscripcionVM.CalleSabado) || String.IsNullOrWhiteSpace(suscripcionVM.BarrioSabado) &
                     suscripcionVM.AlturaSabado != null))
                {
                    domicilioSabado.Calle = suscripcionVM.CalleSabado;
                    domicilioSabado.Altura = suscripcionVM.AlturaSabado;
                    domicilioSabado.Piso = suscripcionVM.PisoSabado;
                    domicilioSabado.Departamento = suscripcionVM.DepartamentoSabado;
                    domicilioSabado.CalleLateral1 = suscripcionVM.CalleLateral1Sabado;
                    domicilioSabado.CalleLateral2 = suscripcionVM.CalleLateral2Sabado;
                    domicilioSabado.Barrio = suscripcionVM.BarrioSabado;
                    domicilioSabado.IdLocalidad = suscripcionVM.IdLocalidadSabado.GetValueOrDefault();
                    domicilioSabado.Observaciones = suscripcionVM.ObservacionesDomicilioSabado;
                }
                else
                {
                    domicilioSabado.Calle = suscripcionVM.CalleSuscriptor;
                    domicilioSabado.Altura = suscripcionVM.AlturaSuscriptor;
                    domicilioSabado.Piso = suscripcionVM.PisoSuscriptor;
                    domicilioSabado.Departamento = suscripcionVM.DepartamentoSuscriptor;
                    domicilioSabado.CalleLateral1 = suscripcionVM.CalleLateral1Suscriptor;
                    domicilioSabado.CalleLateral2 = suscripcionVM.CalleLateral2Suscriptor;
                    domicilioSabado.Barrio = suscripcionVM.BarrioSuscriptor;
                    domicilioSabado.IdLocalidad = suscripcionVM.IdLocalidadSuscriptor;
                    domicilioSabado.Observaciones = suscripcionVM.ObservacionesDomicilioSuscriptor;
                }
                domicilioSabado.TituloDom = "Domicilio entrega - Sábado";

                db.Domicilios.Add(domicilioSabado);
                db.SaveChanges();
                diaentregaSabado.IdDomicilio = domicilioSabado.IdDomicilio;
                diaentregaSabado.Habilitado = true;
                diaentregaSabado.NombreDia = NombreDia.Sabado;
                db.DiaEntregas.Add(diaentregaSabado);
                db.SaveChanges();
                suscripcion.DiaEntregas.Add(diaentregaSabado);
                db.SaveChanges();
            }



            //Mapeo Domicilio Domingo
            Domicilio domicilioDomingo = new Domicilio();
            DiaEntrega diaentregaDomingo = new DiaEntrega();

            if ((!String.IsNullOrWhiteSpace(suscripcionVM.CalleLunes) || String.IsNullOrWhiteSpace(suscripcionVM.BarrioLunes) &
                   suscripcionVM.AlturaLunes != null))
            {
                domicilioDomingo.Calle = suscripcionVM.CalleDomingo;
                domicilioDomingo.Altura = suscripcionVM.AlturaDomingo;
                domicilioDomingo.Piso = suscripcionVM.PisoDomingo;
                domicilioDomingo.Departamento = suscripcionVM.DepartamentoDomingo;
                domicilioDomingo.CalleLateral1 = suscripcionVM.CalleLateral1Domingo;
                domicilioDomingo.CalleLateral2 = suscripcionVM.CalleLateral2Domingo;
                domicilioDomingo.Barrio = suscripcionVM.BarrioDomingo;
                domicilioDomingo.IdLocalidad = suscripcionVM.IdLocalidadDomingo.GetValueOrDefault();
                domicilioDomingo.Observaciones = suscripcionVM.ObservacionesDomicilioDomingo;
            }
            else
            {
                domicilioDomingo.Calle = suscripcionVM.CalleSuscriptor;
                domicilioDomingo.Altura = suscripcionVM.AlturaSuscriptor;
                domicilioDomingo.Piso = suscripcionVM.PisoSuscriptor;
                domicilioDomingo.Departamento = suscripcionVM.DepartamentoSuscriptor;
                domicilioDomingo.CalleLateral1 = suscripcionVM.CalleLateral1Suscriptor;
                domicilioDomingo.CalleLateral2 = suscripcionVM.CalleLateral2Suscriptor;
                domicilioDomingo.Barrio = suscripcionVM.BarrioSuscriptor;
                domicilioDomingo.IdLocalidad = suscripcionVM.IdLocalidadSuscriptor;
                domicilioDomingo.Observaciones = suscripcionVM.ObservacionesDomicilioSuscriptor;
            }
            domicilioDomingo.TituloDom = "Domicilio entrega - Domingo";

            db.Domicilios.Add(domicilioDomingo);
            db.SaveChanges();
            diaentregaDomingo.IdDomicilio = domicilioDomingo.IdDomicilio;
            diaentregaDomingo.Habilitado = true;
            diaentregaDomingo.NombreDia = NombreDia.Domingo;
            db.DiaEntregas.Add(diaentregaDomingo);
            db.SaveChanges();
            suscripcion.DiaEntregas.Add(diaentregaDomingo);
            db.SaveChanges();


            //creación Tarjetas club
            for (int i = 0; i < NombresTarjetaClub.Length; i++)
            {
                if (NombresTarjetaClub[i] != "" & ApellidoTarjetaClub[i] != "")
                {
                    var tarjetaClub = new TarjetaClub();
                    tarjetaClub.Nombres = NombresTarjetaClub[i];
                    tarjetaClub.Apellido = ApellidoTarjetaClub[i];
                    tarjetaClub.DNI = DNITarjetaClub[i];
                    if (IsValidSqlDateTime(FechaNacimientoTarjetaClub[i])) { tarjetaClub.FechaNacimiento = FechaNacimientoTarjetaClub[i]; }

                    tarjetaClub.Email = EmailTarjetaClub[i];
                    tarjetaClub.TelefonoMovilArea = TelefonoMovilAreaTarjetaClub[i];
                    tarjetaClub.TelefonoMovilNumero = TelefonoMovilNumeroTarjetaClub[i];
                    tarjetaClub.FechaSolicitudImpresion = DateTime.Now;
                    tarjetaClub.EstadoTarjetaClub = EstadoTarjetaClub.DerivadaAImpresion;
                    tarjetaClub.Numero = db.TarjetaClubes.Count() + 1;
                  
                    tarjetaClub.TipoTarjetaClub = TipoTarjetaClub.Cortesia;
                    tarjetaClub.TipoSexo = TipoSexoTarjetaClub[i];

                    tarjetaClub.IdSuscripcion = suscripcion.IdSuscripcion;
                    db.TarjetaClubes.Add(tarjetaClub);
                    db.SaveChanges();
                }


            }
            //Historial estado suscripción
            EstadoSuscripcionHistorial esh = new EstadoSuscripcionHistorial
            {
                IdSuscripcion = suscripcion.IdSuscripcion,
                EstadoSuscripcion = EstadoSuscripcion.Activa,
                FechaModificacion = DateTime.Now,
                MotivoEstadoSuscripcion = 0,
                Responsable = "admin (modif. controller)" //HttpContext.User.Identity.Name

            };
            db.EstadoSuscripcionHistoriales.Add(esh);
            db.SaveChanges();

            if (status)
            {


                return RedirectToAction("ListadoCortesia");
            }
            else
            {
                ViewBag.IdOferta = new SelectList(db.Ofertas, "IdOferta", "Nombre", suscripcion.IdOferta);
                ViewBag.IdSuscriptor = new SelectList(db.Suscriptores, "IdSuscriptor", "Nombre", suscripcion.IdSuscriptor);
                return View(suscripcionVM);
            }

        }


        #endregion

        #region CreateNormal
        // GET: Suscripciones/Create
        public ActionResult Create(int? id)
        {
            if (id != null)
            {
                ViewBag.idsus = id;
            }
            ViewBag.IdOferta = db.Ofertas.ToList().FindAll(x => x.TipoOferta == TipoOferta.Estandar);
            ViewBag.Suscriptores = db.Suscriptores.ToList().FindAll(x => x.TipoSuscriptor != TipoSuscriptor.Coorporativo);



            ViewBag.provincias = db.Provincias.ToList();
            ViewBag.localidades = db.Localidades.ToList();
            ViewBag.dateNow = DateTime.Now.GetDateTimeFormats()[7];
            ViewBag.ofertasChaco = db.Ofertas.ToList().FindAll(o => o.Editorial == Editorial.Chaco & o.TipoOferta == TipoOferta.Estandar & o.EstadoOferta.Descripcion == "Activa");
            ViewBag.ofertasCtes = db.Ofertas.ToList().FindAll(o => o.Editorial == Editorial.Corrientes & o.TipoOferta == TipoOferta.Estandar & o.EstadoOferta.Descripcion == "Activa");
            ViewBag.formasDePago = db.TipoMedioPagos.ToList();
            ViewBag.tipoPeriodoEfectivo = db.TiposPeriodoEfectivo.ToList();
            ViewBag.TipoTarjetaCred = db.TiposTarjetas.ToList();
            return View();
        }
        // POST: Suscripciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SuscripcionVM suscripcionVM, string[] NombresTarjetaClub, 
            string[] ApellidoTarjetaClub, int[] DNITarjetaClub, string[] TelefonoMovilNumeroTarjetaClub, 
            string[] TelefonoMovilAreaTarjetaClub, string[] EmailTarjetaClub,
            DateTime[] FechaNacimientoTarjetaClub, TipoSexo[] TipoSexoTarjetaClub,
            int TipoMedioPagoId,int? IdEntidadBancaria, TarjetaPago tarjetaPago, CuentaBancaria cuentaBancaria, int? PeriodoPagoEfectivoId

      
            )
        {
            suscripcionVM.Domingo = true;
            Suscripcion suscripcion = new Suscripcion();
            suscripcion.DiaEntregas = new List<DiaEntrega>();
            Suscriptor suscriptor = new Suscriptor();
            Domicilio domicilio = new Domicilio();
            //Mapeo suscriptor
            var nrosuscriptor = 0;
            try
            {
                nrosuscriptor = db.Suscriptores.Max(x => x.NumeroSuscriptor) + 1;
            }
            catch (Exception)
            {
                nrosuscriptor = 1;


            }

            bool status = true;


            //SI EL SUSC NO EXISTE SOLO GUARDO
            if (suscripcionVM.IdSuscriptor == null)
            {
                suscriptor.Nombre = suscripcionVM.Nombre;
                suscriptor.Apellido = suscripcionVM.Apellido;
                suscriptor.DNI = suscripcionVM.DNI;
                suscriptor.FechaNacimiento = suscripcionVM.FechaNacimiento;
                suscriptor.NumeroSuscriptor = nrosuscriptor;
                suscriptor.TipoSexo = suscripcionVM.TipoSexo;
                suscriptor.Email = suscripcionVM.Email;
                suscriptor.TelefonoFijoNumero = suscripcionVM.TelefonoFijoNumero;
                suscriptor.TelefonoFijoArea = suscripcionVM.TelefonoFijoArea;
                suscriptor.TelefonoMovilNumero = suscripcionVM.TelefonoMovilNumero;
                suscriptor.TelefonoMovilArea = suscripcionVM.TelefonoMovilArea;
                suscriptor.CUIT = suscripcionVM.CUIT;
                suscriptor.RazonSocial = suscripcionVM.RazonSocial;
                suscriptor.TipoSuscriptor = TipoSuscriptor.Normal;
                //Mapeo Domicilio suscriptor
                domicilio.Calle = suscripcionVM.CalleSuscriptor;
                domicilio.Altura = suscripcionVM.AlturaSuscriptor;
                domicilio.Piso = suscripcionVM.PisoSuscriptor;
                domicilio.Departamento = suscripcionVM.DepartamentoSuscriptor;
                domicilio.CalleLateral1 = suscripcionVM.CalleLateral1Suscriptor;
                domicilio.CalleLateral2 = suscripcionVM.CalleLateral2Suscriptor;
                domicilio.Barrio = suscripcionVM.BarrioSuscriptor;
                domicilio.IdLocalidad = suscripcionVM.IdLocalidadSuscriptor;
                domicilio.Observaciones = suscripcionVM.ObservacionesDomicilioSuscriptor;
                domicilio.TituloDom = "Domicilio principal";
                db.Domicilios.Add(domicilio);
                db.SaveChanges();

                //agrego domicilio al susc y guardo el suscriptor
                suscriptor.IdDomicilio = domicilio.IdDomicilio;
                db.Suscriptores.Add(suscriptor);
                db.SaveChanges();
            }
            else
            {//SI EL SUSC EXISTE SE MODIFICA el domicilio y los datos del suscriptor

                suscriptor = db.Suscriptores.Find(suscripcionVM.IdSuscriptor.Value);
                domicilio = db.Domicilios.Find(suscriptor.IdDomicilio);
                suscriptor.Nombre = suscripcionVM.Nombre;
                suscriptor.Apellido = suscripcionVM.Apellido;
                suscriptor.DNI = suscripcionVM.DNI;
                suscriptor.FechaNacimiento = suscripcionVM.FechaNacimiento;
                //suscriptor.NumeroSuscriptor = nrosuscriptor;
                suscriptor.TipoSexo = suscripcionVM.TipoSexo;
                suscriptor.Email = suscripcionVM.Email;
                suscriptor.TelefonoFijoNumero = suscripcionVM.TelefonoFijoNumero;
                suscriptor.TelefonoFijoArea = suscripcionVM.TelefonoFijoArea;
                suscriptor.TelefonoMovilNumero = suscripcionVM.TelefonoMovilNumero;
                suscriptor.TelefonoMovilArea = suscripcionVM.TelefonoMovilArea;
                suscriptor.CUIT = suscripcionVM.CUIT;
                suscriptor.RazonSocial = suscripcionVM.RazonSocial;

                //Mapeo Domicilio suscriptor


                domicilio.Calle = suscripcionVM.CalleSuscriptor;
                domicilio.Altura = suscripcionVM.AlturaSuscriptor;
                domicilio.Piso = suscripcionVM.PisoSuscriptor;
                domicilio.Departamento = suscripcionVM.DepartamentoSuscriptor;
                domicilio.CalleLateral1 = suscripcionVM.CalleLateral1Suscriptor;
                domicilio.CalleLateral2 = suscripcionVM.CalleLateral2Suscriptor;
                domicilio.Barrio = suscripcionVM.BarrioSuscriptor;
                domicilio.IdLocalidad = suscripcionVM.IdLocalidadSuscriptor;
                domicilio.Observaciones = suscripcionVM.ObservacionesDomicilioSuscriptor;
                domicilio.TituloDom = "Domicilio principal";

                db.Entry(domicilio).State = EntityState.Modified;
                db.SaveChanges();

                //agrego domicilio al susc y guardo el suscriptor
                suscriptor.IdDomicilio = domicilio.IdDomicilio;
                suscriptor.IdSuscriptor = suscripcionVM.IdSuscriptor.GetValueOrDefault();
                db.Entry(suscriptor).State = EntityState.Modified;
                db.SaveChanges();
            }

            //Mapeo domicilio Facturación
            Domicilio domicilioFacturacion = new Domicilio();
            if (suscripcionVM.CalleFacturacion != null & suscripcionVM.AlturaFacturacion != null)
            {
                domicilioFacturacion.Calle = suscripcionVM.CalleFacturacion;
                domicilioFacturacion.Altura = suscripcionVM.AlturaFacturacion.GetValueOrDefault(); ;
                domicilioFacturacion.Piso = suscripcionVM.PisoFacturacion;
                domicilioFacturacion.CalleLateral1 = suscripcionVM.CalleLateral1Facturacion;
                domicilioFacturacion.CalleLateral2 = suscripcionVM.CalleLateral2Facturacion;
                domicilioFacturacion.Departamento = suscripcionVM.DepartamentoFacturacion;
                domicilioFacturacion.Barrio = suscripcionVM.BarrioFacturacion;
                domicilioFacturacion.IdLocalidad = suscripcionVM.IdLocalidadFacturacion.GetValueOrDefault();
                domicilioFacturacion.Observaciones = suscripcionVM.ObservacionesDomicilioFacturacion;
                domicilioFacturacion.TituloDom = "Domicilio facturación";
                db.Domicilios.Add(domicilioFacturacion);
                db.SaveChanges();
            }
            else
            {
                domicilioFacturacion = domicilio;
            }
            //Mapeo datos Facturación

            DatosFacturacion datosFacturacion = new DatosFacturacion();
            if (suscripcionVM.NombreFacturacion != null & suscripcionVM.ApellidoFacturacion != null
                & suscripcionVM.CUITFacturacion != null & suscripcionVM.RazonSocialFacturacion != null &
                suscripcionVM.DNIFacturacion != null)
            {
                datosFacturacion.Nombre = suscripcionVM.NombreFacturacion;
                datosFacturacion.Apellido = suscripcionVM.ApellidoFacturacion;
                datosFacturacion.DNI = (int)suscripcionVM.DNIFacturacion;
                datosFacturacion.CUIT = suscripcionVM.CUITFacturacion;
                datosFacturacion.RazonSocial = suscripcionVM.RazonSocialFacturacion;

            }
            else
            {
                datosFacturacion.Nombre = suscripcionVM.Nombre;
                datosFacturacion.Apellido = suscripcionVM.Apellido;
                datosFacturacion.DNI = suscripcionVM.DNI;
                datosFacturacion.CUIT = suscripcionVM.CUIT;
                datosFacturacion.RazonSocial = suscripcionVM.RazonSocial;
            }
            datosFacturacion.TipoIva = suscripcionVM.TipoIva;
            datosFacturacion.IdDomicilio = domicilioFacturacion.IdDomicilio;
            db.DatosFacturaciones.Add(datosFacturacion);
            db.SaveChanges();

            //Mapeo datos Suscripción
            suscripcion.FechaAlta = DateTime.Now;
            var nrosuscripcion = 1;
            try
            {
                nrosuscripcion = db.Suscripciones.ToList().LastOrDefault().NumeroSuscripcion + 1;
            }
            catch (Exception)
            {
            }
            suscripcion.NumeroSuscripcion = nrosuscripcion;

            //Mapeo relaciones

            //suscripcion.IdCanillita = suscripcionVM.IdCanillita;
            suscripcion.IdSuscriptor = suscriptor.IdSuscriptor;
            suscripcion.IdOferta = suscripcionVM.IdOferta;
            suscripcion.IdDatosFacturacion = datosFacturacion.IdDatosFacturacion;
            suscripcion.TipoSuscripcion = TipoSuscripcion.Normal;
            suscripcion.Lunes = suscripcionVM.Lunes;
            suscripcion.Martes = suscripcionVM.Martes;
            suscripcion.Miercoles = suscripcionVM.Miercoles;
            suscripcion.Jueves = suscripcionVM.Jueves;
            suscripcion.Viernes = suscripcionVM.Viernes;
            suscripcion.Sabado = suscripcionVM.Sabado;
            suscripcion.Domingo = suscripcionVM.Domingo;
            suscripcion.CantTarjetasExtras = suscripcionVM.CantTarjetasExtras;
            suscripcion.PrecioSuscripcion = suscripcionVM.PrecioSuscripcion;

            try
            {
                db.Suscripciones.Add(suscripcion);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                status = false;
            }




            //MAPEO y GUARDADO de DOMICILIOS y DIAS DE ENTREGA//
            //Mapeo Domicilio lunes
            Domicilio domicilioLunes = new Domicilio();
            DiaEntrega diaentregaLunes = new DiaEntrega();
            if (suscripcionVM.Lunes)
            {
                if ((!String.IsNullOrWhiteSpace(suscripcionVM.CalleLunes) || String.IsNullOrWhiteSpace(suscripcionVM.BarrioLunes) &
                     suscripcionVM.AlturaLunes != null))
                {
                    domicilioLunes.Calle = suscripcionVM.CalleLunes;
                    domicilioLunes.Altura = suscripcionVM.AlturaLunes;
                    domicilioLunes.Piso = suscripcionVM.PisoLunes;
                    domicilioLunes.Departamento = suscripcionVM.DepartamentoLunes;
                    domicilioLunes.CalleLateral1 = suscripcionVM.CalleLateral1Lunes;
                    domicilioLunes.CalleLateral2 = suscripcionVM.CalleLateral2Lunes;
                    domicilioLunes.Barrio = suscripcionVM.BarrioLunes;
                    domicilioLunes.IdLocalidad = suscripcionVM.IdLocalidadLunes.GetValueOrDefault();
                    domicilioLunes.Observaciones = suscripcionVM.ObservacionesDomicilioLunes;
                }
                else
                {
                    domicilioLunes.Calle = suscripcionVM.CalleSuscriptor;
                    domicilioLunes.Altura = suscripcionVM.AlturaSuscriptor;
                    domicilioLunes.Piso = suscripcionVM.PisoSuscriptor;
                    domicilioLunes.Departamento = suscripcionVM.DepartamentoSuscriptor;
                    domicilioLunes.CalleLateral1 = suscripcionVM.CalleLateral1Suscriptor;
                    domicilioLunes.CalleLateral2 = suscripcionVM.CalleLateral2Suscriptor;
                    domicilioLunes.Barrio = suscripcionVM.BarrioSuscriptor;
                    domicilioLunes.IdLocalidad = suscripcionVM.IdLocalidadSuscriptor;
                    domicilioLunes.Observaciones = suscripcionVM.ObservacionesDomicilioSuscriptor;
                }
                domicilioLunes.TituloDom = "Domicilio entrega - Lunes";

                db.Domicilios.Add(domicilioLunes);
                db.SaveChanges();
                diaentregaLunes.IdDomicilio = domicilioLunes.IdDomicilio;
                diaentregaLunes.Habilitado = true;
                diaentregaLunes.NombreDia = NombreDia.Lunes;
                db.DiaEntregas.Add(diaentregaLunes);
                db.SaveChanges();
                suscripcion.DiaEntregas.Add(diaentregaLunes);
                db.SaveChanges();
            }
            
            //Mapeo Domicilio Martes
            Domicilio domicilioMartes = new Domicilio();
            DiaEntrega diaentregaMartes = new DiaEntrega();

            if (suscripcionVM.Martes)
            {
                if ((!String.IsNullOrWhiteSpace(suscripcionVM.CalleMartes) || String.IsNullOrWhiteSpace(suscripcionVM.BarrioMartes) &
                     suscripcionVM.AlturaMartes != null))
                {
                    domicilioMartes.Calle = suscripcionVM.CalleMartes;
                    domicilioMartes.Altura = suscripcionVM.AlturaMartes;
                    domicilioMartes.Piso = suscripcionVM.PisoMartes;
                    domicilioMartes.Departamento = suscripcionVM.DepartamentoMartes;
                    domicilioMartes.CalleLateral1 = suscripcionVM.CalleLateral1Martes;
                    domicilioMartes.CalleLateral2 = suscripcionVM.CalleLateral2Martes;
                    domicilioMartes.Barrio = suscripcionVM.BarrioMartes;
                    domicilioMartes.IdLocalidad = suscripcionVM.IdLocalidadMartes.GetValueOrDefault();
                    domicilioMartes.Observaciones = suscripcionVM.ObservacionesDomicilioMartes;
                }
                else
                {
                    domicilioMartes.Calle = suscripcionVM.CalleSuscriptor;
                    domicilioMartes.Altura = suscripcionVM.AlturaSuscriptor;
                    domicilioMartes.Piso = suscripcionVM.PisoSuscriptor;
                    domicilioMartes.Departamento = suscripcionVM.DepartamentoSuscriptor;
                    domicilioMartes.CalleLateral1 = suscripcionVM.CalleLateral1Suscriptor;
                    domicilioMartes.CalleLateral2 = suscripcionVM.CalleLateral2Suscriptor;
                    domicilioMartes.Barrio = suscripcionVM.BarrioSuscriptor;
                    domicilioMartes.IdLocalidad = suscripcionVM.IdLocalidadSuscriptor;
                    domicilioMartes.Observaciones = suscripcionVM.ObservacionesDomicilioSuscriptor;
                }
                domicilioMartes.TituloDom = "Domicilio entrega - Martes";

                db.Domicilios.Add(domicilioMartes);
                db.SaveChanges();
                diaentregaMartes.IdDomicilio = domicilioMartes.IdDomicilio;
                diaentregaMartes.Habilitado = true;
                diaentregaMartes.NombreDia = NombreDia.Martes;
                db.DiaEntregas.Add(diaentregaMartes);
                db.SaveChanges();
                suscripcion.DiaEntregas.Add(diaentregaMartes);
                db.SaveChanges();
            }
            
            //Mapeo Domicilio Miercoles
            Domicilio domicilioMiercoles = new Domicilio();
            DiaEntrega diaentregaMiercoles = new DiaEntrega();

            if (suscripcionVM.Miercoles)
            {
                if ((!String.IsNullOrWhiteSpace(suscripcionVM.CalleMiercoles) || String.IsNullOrWhiteSpace(suscripcionVM.BarrioMiercoles) &
                     suscripcionVM.AlturaMiercoles != null))
                {
                    domicilioMiercoles.Calle = suscripcionVM.CalleMiercoles;
                    domicilioMiercoles.Altura = suscripcionVM.AlturaMiercoles;
                    domicilioMiercoles.Piso = suscripcionVM.PisoMiercoles;
                    domicilioMiercoles.Departamento = suscripcionVM.DepartamentoMiercoles;
                    domicilioMiercoles.CalleLateral1 = suscripcionVM.CalleLateral1Miercoles;
                    domicilioMiercoles.CalleLateral2 = suscripcionVM.CalleLateral2Miercoles;
                    domicilioMiercoles.Barrio = suscripcionVM.BarrioMiercoles;
                    domicilioMiercoles.IdLocalidad = suscripcionVM.IdLocalidadMiercoles.GetValueOrDefault();
                    domicilioMiercoles.Observaciones = suscripcionVM.ObservacionesDomicilioMiercoles;
                }
                else
                {
                    domicilioMiercoles.Calle = suscripcionVM.CalleSuscriptor;
                    domicilioMiercoles.Altura = suscripcionVM.AlturaSuscriptor;
                    domicilioMiercoles.Piso = suscripcionVM.PisoSuscriptor;
                    domicilioMiercoles.Departamento = suscripcionVM.DepartamentoSuscriptor;
                    domicilioMiercoles.CalleLateral1 = suscripcionVM.CalleLateral1Suscriptor;
                    domicilioMiercoles.CalleLateral2 = suscripcionVM.CalleLateral2Suscriptor;
                    domicilioMiercoles.Barrio = suscripcionVM.BarrioSuscriptor;
                    domicilioMiercoles.IdLocalidad = suscripcionVM.IdLocalidadSuscriptor;
                    domicilioMiercoles.Observaciones = suscripcionVM.ObservacionesDomicilioSuscriptor;
                }
                domicilioMiercoles.TituloDom = "Domicilio entrega - Miércoles";

                db.Domicilios.Add(domicilioMiercoles);
                db.SaveChanges();
                diaentregaMiercoles.IdDomicilio = domicilioMiercoles.IdDomicilio;
                diaentregaMiercoles.Habilitado = true;
                diaentregaMiercoles.NombreDia = NombreDia.Miercoles;
                db.DiaEntregas.Add(diaentregaMiercoles);
                db.SaveChanges();
                suscripcion.DiaEntregas.Add(diaentregaMiercoles);
                db.SaveChanges();
            }
            
            //Mapeo Domicilio Jueves
            Domicilio domicilioJueves = new Domicilio();
            DiaEntrega diaentregaJueves = new DiaEntrega();

            if (suscripcionVM.Jueves)
            {
                if ((!String.IsNullOrWhiteSpace(suscripcionVM.CalleJueves) || String.IsNullOrWhiteSpace(suscripcionVM.BarrioJueves) &
                     suscripcionVM.AlturaJueves != null))
                {
                    domicilioJueves.Calle = suscripcionVM.CalleJueves;
                    domicilioJueves.Altura = suscripcionVM.AlturaJueves;
                    domicilioJueves.Piso = suscripcionVM.PisoJueves;
                    domicilioJueves.Departamento = suscripcionVM.DepartamentoJueves;
                    domicilioJueves.CalleLateral1 = suscripcionVM.CalleLateral1Jueves;
                    domicilioJueves.CalleLateral2 = suscripcionVM.CalleLateral2Jueves;
                    domicilioJueves.Barrio = suscripcionVM.BarrioJueves;
                    domicilioJueves.IdLocalidad = suscripcionVM.IdLocalidadJueves.GetValueOrDefault();
                    domicilioJueves.Observaciones = suscripcionVM.ObservacionesDomicilioJueves;
                }
                else
                {
                    domicilioJueves.Calle = suscripcionVM.CalleSuscriptor;
                    domicilioJueves.Altura = suscripcionVM.AlturaSuscriptor;
                    domicilioJueves.Piso = suscripcionVM.PisoSuscriptor;
                    domicilioJueves.Departamento = suscripcionVM.DepartamentoSuscriptor;
                    domicilioJueves.CalleLateral1 = suscripcionVM.CalleLateral1Suscriptor;
                    domicilioJueves.CalleLateral2 = suscripcionVM.CalleLateral2Suscriptor;
                    domicilioJueves.Barrio = suscripcionVM.BarrioSuscriptor;
                    domicilioJueves.IdLocalidad = suscripcionVM.IdLocalidadSuscriptor;
                    domicilioJueves.Observaciones = suscripcionVM.ObservacionesDomicilioSuscriptor;
                }
                domicilioJueves.TituloDom = "Domicilio entrega - Jueves";

                db.Domicilios.Add(domicilioJueves);
                db.SaveChanges();
                diaentregaJueves.IdDomicilio = domicilioJueves.IdDomicilio;
                diaentregaJueves.Habilitado = true;
                diaentregaJueves.NombreDia = NombreDia.Jueves;
                db.DiaEntregas.Add(diaentregaJueves);
                db.SaveChanges();
                suscripcion.DiaEntregas.Add(diaentregaJueves);
                db.SaveChanges();
            }
            
            //Mapeo Domicilio Viernes
            Domicilio domicilioViernes = new Domicilio();
            DiaEntrega diaentregaViernes = new DiaEntrega();

            if (suscripcionVM.Viernes)
            {
                if ((!String.IsNullOrWhiteSpace(suscripcionVM.CalleViernes) || String.IsNullOrWhiteSpace(suscripcionVM.BarrioViernes) &
                     suscripcionVM.AlturaViernes != null))
                {
                    domicilioViernes.Calle = suscripcionVM.CalleViernes;
                    domicilioViernes.Altura = suscripcionVM.AlturaViernes;
                    domicilioViernes.Piso = suscripcionVM.PisoViernes;
                    domicilioViernes.Departamento = suscripcionVM.DepartamentoViernes;
                    domicilioViernes.CalleLateral1 = suscripcionVM.CalleLateral1Viernes;
                    domicilioViernes.CalleLateral2 = suscripcionVM.CalleLateral2Viernes;
                    domicilioViernes.Barrio = suscripcionVM.BarrioViernes;
                    domicilioViernes.IdLocalidad = suscripcionVM.IdLocalidadViernes.GetValueOrDefault();
                    domicilioViernes.Observaciones = suscripcionVM.ObservacionesDomicilioViernes;
                }
                else
                {
                    domicilioViernes.Calle = suscripcionVM.CalleSuscriptor;
                    domicilioViernes.Altura = suscripcionVM.AlturaSuscriptor;
                    domicilioViernes.Piso = suscripcionVM.PisoSuscriptor;
                    domicilioViernes.Departamento = suscripcionVM.DepartamentoSuscriptor;
                    domicilioViernes.CalleLateral1 = suscripcionVM.CalleLateral1Suscriptor;
                    domicilioViernes.CalleLateral2 = suscripcionVM.CalleLateral2Suscriptor;
                    domicilioViernes.Barrio = suscripcionVM.BarrioSuscriptor;
                    domicilioViernes.IdLocalidad = suscripcionVM.IdLocalidadSuscriptor;
                    domicilioViernes.Observaciones = suscripcionVM.ObservacionesDomicilioSuscriptor;
                }
                domicilioViernes.TituloDom = "Domicilio entrega - Viernes";

                db.Domicilios.Add(domicilioViernes);
                db.SaveChanges();
                diaentregaViernes.IdDomicilio = domicilioViernes.IdDomicilio;
                diaentregaViernes.Habilitado = true;
                diaentregaViernes.NombreDia = NombreDia.Viernes;
                db.DiaEntregas.Add(diaentregaViernes);
                db.SaveChanges();
                suscripcion.DiaEntregas.Add(diaentregaViernes);
                db.SaveChanges();
            }
            
            //Mapeo Domicilio Sabado
            Domicilio domicilioSabado = new Domicilio();
            DiaEntrega diaentregaSabado = new DiaEntrega();

            if (suscripcionVM.Sabado)
            {
                if ((!String.IsNullOrWhiteSpace(suscripcionVM.CalleSabado) || String.IsNullOrWhiteSpace(suscripcionVM.BarrioSabado) &
                     suscripcionVM.AlturaSabado != null))
                {
                    domicilioSabado.Calle = suscripcionVM.CalleSabado;
                    domicilioSabado.Altura = suscripcionVM.AlturaSabado;
                    domicilioSabado.Piso = suscripcionVM.PisoSabado;
                    domicilioSabado.Departamento = suscripcionVM.DepartamentoSabado;
                    domicilioSabado.CalleLateral1 = suscripcionVM.CalleLateral1Sabado;
                    domicilioSabado.CalleLateral2 = suscripcionVM.CalleLateral2Sabado;
                    domicilioSabado.Barrio = suscripcionVM.BarrioSabado;
                    domicilioSabado.IdLocalidad = suscripcionVM.IdLocalidadSabado.GetValueOrDefault();
                    domicilioSabado.Observaciones = suscripcionVM.ObservacionesDomicilioSabado;
                }
                else
                {
                    domicilioSabado.Calle = suscripcionVM.CalleSuscriptor;
                    domicilioSabado.Altura = suscripcionVM.AlturaSuscriptor;
                    domicilioSabado.Piso = suscripcionVM.PisoSuscriptor;
                    domicilioSabado.Departamento = suscripcionVM.DepartamentoSuscriptor;
                    domicilioSabado.CalleLateral1 = suscripcionVM.CalleLateral1Suscriptor;
                    domicilioSabado.CalleLateral2 = suscripcionVM.CalleLateral2Suscriptor;
                    domicilioSabado.Barrio = suscripcionVM.BarrioSuscriptor;
                    domicilioSabado.IdLocalidad = suscripcionVM.IdLocalidadSuscriptor;
                    domicilioSabado.Observaciones = suscripcionVM.ObservacionesDomicilioSuscriptor;
                }
                domicilioSabado.TituloDom = "Domicilio entrega - Sábado";

                db.Domicilios.Add(domicilioSabado);
                db.SaveChanges();
                diaentregaSabado.IdDomicilio = domicilioSabado.IdDomicilio;
                diaentregaSabado.Habilitado = true;
                diaentregaSabado.NombreDia = NombreDia.Sabado;
                db.DiaEntregas.Add(diaentregaSabado);
                db.SaveChanges();
                suscripcion.DiaEntregas.Add(diaentregaSabado);
                db.SaveChanges();
            }
            
            //Mapeo Domicilio Domingo
            Domicilio domicilioDomingo = new Domicilio();
            DiaEntrega diaentregaDomingo = new DiaEntrega();

            if ((!String.IsNullOrWhiteSpace(suscripcionVM.CalleDomingo) || String.IsNullOrWhiteSpace(suscripcionVM.BarrioDomingo) &
                   suscripcionVM.AlturaDomingo != null))
            {
                domicilioDomingo.Calle = suscripcionVM.CalleDomingo;
                domicilioDomingo.Altura = suscripcionVM.AlturaDomingo;
                domicilioDomingo.Piso = suscripcionVM.PisoDomingo;
                domicilioDomingo.Departamento = suscripcionVM.DepartamentoDomingo;
                domicilioDomingo.CalleLateral1 = suscripcionVM.CalleLateral1Domingo;
                domicilioDomingo.CalleLateral2 = suscripcionVM.CalleLateral2Domingo;
                domicilioDomingo.Barrio = suscripcionVM.BarrioDomingo;
                domicilioDomingo.IdLocalidad = suscripcionVM.IdLocalidadDomingo.GetValueOrDefault();
                domicilioDomingo.Observaciones = suscripcionVM.ObservacionesDomicilioDomingo;
            }
            else
            {
                domicilioDomingo.Calle = suscripcionVM.CalleSuscriptor;
                domicilioDomingo.Altura = suscripcionVM.AlturaSuscriptor;
                domicilioDomingo.Piso = suscripcionVM.PisoSuscriptor;
                domicilioDomingo.Departamento = suscripcionVM.DepartamentoSuscriptor;
                domicilioDomingo.CalleLateral1 = suscripcionVM.CalleLateral1Suscriptor;
                domicilioDomingo.CalleLateral2 = suscripcionVM.CalleLateral2Suscriptor;
                domicilioDomingo.Barrio = suscripcionVM.BarrioSuscriptor;
                domicilioDomingo.IdLocalidad = suscripcionVM.IdLocalidadSuscriptor;
                domicilioDomingo.Observaciones = suscripcionVM.ObservacionesDomicilioSuscriptor;
            }

            domicilioDomingo.TituloDom = "Domicilio entrega - Domingo";

            db.Domicilios.Add(domicilioDomingo);
            db.SaveChanges();
            diaentregaDomingo.IdDomicilio = domicilioDomingo.IdDomicilio;
            diaentregaDomingo.Habilitado = true;
            diaentregaDomingo.NombreDia = NombreDia.Domingo;
            db.DiaEntregas.Add(diaentregaDomingo);
            db.SaveChanges();
            suscripcion.DiaEntregas.Add(diaentregaDomingo);
            db.SaveChanges();


            //creación Tarjetas club
            for (int i = 0; i < NombresTarjetaClub.Length; i++)
            {
                if (NombresTarjetaClub[i] != "" & ApellidoTarjetaClub[i] != "")
                {
                    var tarjetaClub = new TarjetaClub();
                    tarjetaClub.Nombres = NombresTarjetaClub[i];
                    tarjetaClub.Apellido = ApellidoTarjetaClub[i];
                    tarjetaClub.DNI = DNITarjetaClub[i];
                    if (IsValidSqlDateTime(FechaNacimientoTarjetaClub[i])) { tarjetaClub.FechaNacimiento = FechaNacimientoTarjetaClub[i]; }

                    tarjetaClub.Email = EmailTarjetaClub[i];
                    tarjetaClub.TelefonoMovilArea = TelefonoMovilAreaTarjetaClub[i];
                    tarjetaClub.TelefonoMovilNumero = TelefonoMovilNumeroTarjetaClub[i];
                    tarjetaClub.FechaSolicitudImpresion = DateTime.Now;
                    tarjetaClub.EstadoTarjetaClub = EstadoTarjetaClub.DerivadaAImpresion;
                    tarjetaClub.Numero = db.TarjetaClubes.Count() + 1;
                    tarjetaClub.TipoSexo = TipoSexoTarjetaClub[i];

                    if (suscripcionVM.TipoSuscripcion == TipoSuscripcion.Cortesia)
                    {
                        tarjetaClub.TipoTarjetaClub = TipoTarjetaClub.Cortesia;
                    }
                    else
                    {
                        tarjetaClub.TipoTarjetaClub = (i == 0)
                       ? TipoTarjetaClub.Titular
                       : TipoTarjetaClub.Adicional;
                    }
                    tarjetaClub.IdSuscripcion = suscripcion.IdSuscripcion;
                    db.TarjetaClubes.Add(tarjetaClub);
                    db.SaveChanges();
                }
                
            }

            #region facturacion&pago
            // int TipoMedioPagoId,int? IdEntidadBancaria, TarjetaPago tarjetaPago, CuentaBancaria cuentaBancaria, int? PeriodoPagoEfectivoId
            suscripcion.TipoMedioPagoId = suscripcionVM.TipoMedioPagoId;
           // if (PeriodoPagoEfectivoId != null) //ignorar cuenta bancaria y tarjeta pago
            //{
            //    suscripcion.
            //}

            //switch (suscripcionVM.TipoMedioPagoId)
            //{
            //    case 1: //tarjeta credito
            //        suscripcion.IdEntidadBancaria = suscripcionVM.IdEntidadBancaria;
            //        break;

            //    case 2://cuenta/caja ahorro
            //        suscripcion.IdEntidadBancaria = suscripcionVM.IdEntidadBancaria;
            //        break;

            //    case 3: //efectivo

            //        break;

                    
            //}

            #endregion

            //Historial estado suscripción
            EstadoSuscripcionHistorial esh = new EstadoSuscripcionHistorial
            {
                IdSuscripcion = suscripcion.IdSuscripcion,
                EstadoSuscripcion = EstadoSuscripcion.Activa,
                FechaModificacion = DateTime.Now,
                MotivoEstadoSuscripcion = 0,
                Responsable = "admin (modif. controller)" //todo HttpContext.User.Identity.Name

            };
            db.EstadoSuscripcionHistoriales.Add(esh);
            db.SaveChanges();

            if (status)
            {
                return RedirectToAction("ListadoNormal");
            }
            else
            {
                ViewBag.provincias = db.Provincias.ToList();
                ViewBag.localidades = db.Localidades.ToList();
                ViewBag.dateNow = DateTime.Now.GetDateTimeFormats()[7];
                ViewBag.ofertasChaco = db.Ofertas.ToList().FindAll(o => o.Editorial == Editorial.Chaco & o.TipoOferta == TipoOferta.Estandar & o.EstadoOferta.Descripcion == "Activa");
                ViewBag.ofertasCtes = db.Ofertas.ToList().FindAll(o => o.Editorial == Editorial.Corrientes & o.TipoOferta == TipoOferta.Estandar & o.EstadoOferta.Descripcion == "Activa");
                ViewBag.formasDePago = db.TipoMedioPagos.ToList();
                ViewBag.IdSuscriptor = new SelectList(db.Suscriptores, "IdSuscriptor", "Nombre", suscripcion.IdSuscriptor);
                ViewBag.formasDePago = db.TipoMedioPagos.ToList();
                ViewBag.IdOferta = db.Ofertas.ToList().FindAll(x => x.TipoOferta == TipoOferta.Estandar);
                ViewBag.Suscriptores = db.Suscriptores.ToList().FindAll(x => x.TipoSuscriptor != TipoSuscriptor.Coorporativo);
                
                ViewBag.tipoPeriodoEfectivo = db.TiposPeriodoEfectivo.ToList();
                ViewBag.TipoTarjetaCred = db.TiposTarjetas.ToList();
                return View(suscripcionVM);
            }

        }



        #endregion

        // GET: Suscripciones/_CreateDom
        public ActionResult _CreateDom1()
        {
            ViewBag.provincias = db.Provincias.ToList();
            ViewBag.localidades = db.Localidades.ToList();
            return View();
        }
        public ActionResult _CreateDom2()
        {
            ViewBag.provincias = db.Provincias.ToList();
            ViewBag.localidades = db.Localidades.ToList();
            return View();
        }
        public ActionResult _CreateDom3()
        {
            ViewBag.provincias = db.Provincias.ToList();
            ViewBag.localidades = db.Localidades.ToList();
            return View();
        }
        public ActionResult _CreateDom4()
        {
            ViewBag.provincias = db.Provincias.ToList();
            ViewBag.localidades = db.Localidades.ToList();
            return View();
        }
        public ActionResult _CreateDom5()
        {
            ViewBag.provincias = db.Provincias.ToList();
            ViewBag.localidades = db.Localidades.ToList();
            return View();
        }
        public ActionResult _CreateDom6()
        {
            ViewBag.provincias = db.Provincias.ToList();
            ViewBag.localidades = db.Localidades.ToList();
            return View();
        }
        public ActionResult _CreateDom7()
        {
            ViewBag.provincias = db.Provincias.ToList();
            ViewBag.localidades = db.Localidades.ToList();
            return View();
        }

     
        // GET: Suscripciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suscripcion suscripcion = db.Suscripciones.Find(id);
            if (suscripcion == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdOferta = new SelectList(db.Ofertas, "IdOferta", "Nombre", suscripcion.IdOferta);
            ViewBag.IdSuscriptor = new SelectList(db.Suscriptores, "IdSuscriptor", "Nombre", suscripcion.IdSuscriptor);
            return View(suscripcion);
        }

        // POST: Suscripciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSuscripcion,FechaAlta,FechaSolicitudBaja,FechaModificacion,NumeroSuscripcion,IdOferta,IdSuscriptor")] Suscripcion suscripcion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suscripcion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdOferta = new SelectList(db.Ofertas, "IdOferta", "Nombre", suscripcion.IdOferta);
            ViewBag.IdSuscriptor = new SelectList(db.Suscriptores, "IdSuscriptor", "Nombre", suscripcion.IdSuscriptor);
            return View(suscripcion);
        }

        // GET: Suscripciones/CambiarEstadoSuscripcion
        public ActionResult CambiarEstadoSuscripcion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suscripcion suscripcion = db.Suscripciones.Find(id);
            if (suscripcion == null)
            {
                return HttpNotFound();
            }
            return View(suscripcion);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CambiarEstadoSuscripcion(int IdSuscripcion, EstadoSuscripcion Estado, DateTime Fecha, MotivoEstadoSuscripcion Motivo, string Observaciones, string Responsable)
        {
            Suscripcion suscripcion;
            try
            {
                suscripcion = db.Suscripciones.Find(IdSuscripcion);
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
           
            if (Estado != EstadoSuscripcion.Activa)
            {
                foreach (var m in suscripcion.TarjetaClub.ToList())
                {
                    m.EstadoTarjetaClub = EstadoTarjetaClub.Baja;
                }
            }


            EstadoSuscripcionHistorial estadoSuscripcionHistorial = new EstadoSuscripcionHistorial
            {
                MotivoEstadoSuscripcion = Motivo,
                EstadoSuscripcion = Estado,
                FechaModificacion = Fecha,
                Observaciones = Observaciones,
                Responsable = Responsable,
                IdSuscripcion = suscripcion.IdSuscripcion
            };
         
            db.EstadoSuscripcionHistoriales.Add(estadoSuscripcionHistorial);
            db.SaveChanges();
          

            db.Entry(suscripcion).State = EntityState.Modified;
            db.SaveChanges();
            
           
            return RedirectToAction("Index");
        }

        //VALIDADOR DE FECHAS TRUE=VALIDA
        public static bool IsValidSqlDateTime(DateTime? dateTime)
        {
            if (dateTime == null) return true;

            DateTime minValue = DateTime.Parse(System.Data.SqlTypes.SqlDateTime.MinValue.ToString());
            DateTime maxValue = DateTime.Parse(System.Data.SqlTypes.SqlDateTime.MaxValue.ToString());

            if (minValue > dateTime.Value || maxValue < dateTime.Value)
                return false;

            return true;
        }

        #region vistasParcialesFormasDePago

        public ActionResult TarjetaCreditoPartial()
        {
            
           
            ViewBag.TipoTarjetaCred = db.TiposTarjetas.ToList();
            return PartialView("_TarjetaPago");
        }
        public ActionResult CuentaBancariaPartial()
        {
            ViewBag.formasDePago = db.TipoMedioPagos.ToList();
            return PartialView("_CuentaCajaAhoro");
        }
        public ActionResult EfectivoPartial()
        {
            ViewBag.tipoPeriodoEfectivo = db.TiposPeriodoEfectivo.ToList();
            return PartialView("_Efectivo");
        }

        #endregion
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
