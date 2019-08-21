using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using SNC2017.Models;
using SNC2017.ViewModels;

namespace SNC2017.Controllers
{
    public class SuscriptoresController : Controller
    {
        private SNCContext db = new SNCContext();

        // GET: Suscriptores
        public ActionResult Index()
        {
            return View();
        }

        public string ListaSuscriptores(int tiposuscriptor)
        {
            switch (tiposuscriptor)
            {
                case 1:
                    return JsonConvert.SerializeObject(db.Suscriptores.ToList()
                        .FindAll(x => x.TipoSuscriptor == TipoSuscriptor.Normal).Select(a => new
                        {
                            Id = a.IdSuscriptor,
                            NumeroSuscriptor = a.NumeroSuscriptor,
                            Nombre = $"{a.Nombre} {a.Apellido}",
                            Dni = a.DNI,
                            Celular = $"{a.TelefonoMovilArea} {a.TelefonoMovilNumero}"
                        }));
                case 2:
                    return JsonConvert.SerializeObject(db.Suscriptores.ToList()
                        .FindAll(x => x.TipoSuscriptor == TipoSuscriptor.Coorporativo).Select(a => new
                        {
                            Id = a.IdSuscriptor,
                            NumeroSuscriptor = a.NumeroSuscriptor,
                            Nombre = $"{a.Nombre} {a.Apellido}",
                            Dni = a.DNI,
                            Celular = $"{a.TelefonoMovilArea} {a.TelefonoMovilNumero}"
                        }));
                default:
                    return JsonConvert.SerializeObject(db.Suscriptores.ToList().Select(a => new
                    {
                        Id = a.IdSuscriptor,
                        NumeroSuscriptor = a.NumeroSuscriptor,
                        Nombre = $"{a.Nombre} {a.Apellido}",
                        Dni = a.DNI,
                        Celular = $"{a.TelefonoMovilArea} {a.TelefonoMovilNumero}"
                    }));
            }
        }

        // GET: Suscriptores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suscriptor suscriptor = db.Suscriptores.Find(id);
            if (suscriptor == null)
            {
                return HttpNotFound();
            }
            return View(suscriptor);
        }

       

        // GET: Suscriptores/Create
        public ActionResult Create()
        {
            ViewBag.localidades = db.Localidades.ToList();
            ViewBag.provincias = db.Provincias.ToList();

            return View();
        }
        public ActionResult LocalidadesProv(int prov)
        {

            var localidades = db.Provincias.ToList().Find(x => x.IdProvincia == prov).Localidades.ToList().OrderBy(c=>c.Nombre);
            List <string> loclist=new List<string>();
            foreach (var item in localidades)
            {
                loclist.Add(item.IdLocalidad+"#"+item.Nombre);
            }

            return Json(loclist.ToList(), JsonRequestBehavior.AllowGet);
        }
        // POST: Suscriptores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NumeroSuscriptor,TipoSuscriptor,Nombre,Apellido,FechaNacimiento,NumeroSuscriptor,DNI,TipoSexo,Email,TelefonoFijoNumero,TelefonoFijoArea,TelefonoMovilNumero,TelefonoMovilArea,CUIT,RazonSocial,CalleSuscriptor,AlturaSuscriptor,PisoSuscriptor,DepartamentoSuscriptor,CalleLateral1Suscriptor,CalleLateral2Suscriptor,BarrioSuscriptor,ObservacionesDomicilioSuscriptor,IdLocalidad,IdProvincia")] SuscriptorVM suscriptorVM)
        {
            //Mapeo suscriptor
            Suscriptor suscriptor = new Suscriptor
            {
                EstadoSuscriptor = EstadoSuscriptor.Solicitante,
                Nombre = suscriptorVM.Nombre,
                Apellido = suscriptorVM.Apellido,
                FechaNacimiento = suscriptorVM.FechaNacimiento,
                NumeroSuscriptor = db.Suscriptores.Count()+1,
                DNI = suscriptorVM.DNI,
                TipoSexo = suscriptorVM.TipoSexo,
                Email = suscriptorVM.Email,
                TelefonoFijoNumero = suscriptorVM.TelefonoFijoNumero,
                TelefonoFijoArea = suscriptorVM.TelefonoFijoArea,
                TelefonoMovilArea = suscriptorVM.TelefonoMovilArea,
                TelefonoMovilNumero = suscriptorVM.TelefonoMovilNumero,
                CUIT = suscriptorVM.CUIT,
                RazonSocial = suscriptorVM.RazonSocial,
                TipoSuscriptor = suscriptorVM.TipoSuscriptor


            };
            //Mapeo Domicilio suscriptor
            Domicilio domicilio = new Domicilio
            {
                Calle = suscriptorVM.CalleSuscriptor,
                Altura = suscriptorVM.AlturaSuscriptor,
                Piso = suscriptorVM.PisoSuscriptor,
                Departamento = suscriptorVM.DepartamentoSuscriptor,
                CalleLateral1 = suscriptorVM.CalleLateral1Suscriptor,
                CalleLateral2 = suscriptorVM.CalleLateral2Suscriptor,
                Barrio = suscriptorVM.BarrioSuscriptor,
                IdLocalidad = suscriptorVM.IdLocalidad,
                Observaciones = suscriptorVM.ObservacionesDomicilioSuscriptor
            };
            try
            {
                db.Domicilios.Add(domicilio);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }

         
                suscriptor.IdDomicilio = domicilio.IdDomicilio;
                db.Suscriptores.Add(suscriptor);
                db.SaveChanges();
           
            
            return RedirectToAction("Details",new {id=suscriptor.IdSuscriptor});
        }

        // GET: Suscriptores/Edit/5
        public ActionResult Edit(int? id)
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Suscriptor suscriptor = db.Suscriptores.Find(id);
            if (suscriptor == null)
            {
                return HttpNotFound();
            }
            ViewBag.localidades = db.Localidades.ToList();
            ViewBag.provincias = db.Provincias.ToList();
            SuscriptorVM suscriptorVm=new SuscriptorVM();
            suscriptorVm.IdSuscriptor = suscriptor.IdSuscriptor;
            suscriptorVm.NumeroSuscriptor = suscriptor.NumeroSuscriptor;
            suscriptorVm.TipoSuscriptor = suscriptor.TipoSuscriptor;
            suscriptorVm.Nombre = suscriptor.Nombre;
            suscriptorVm.Apellido = suscriptor.Apellido;
            suscriptorVm.FechaNacimiento = suscriptor.FechaNacimiento;
            suscriptorVm.DNI = suscriptor.DNI;
            suscriptorVm.TipoSexo = suscriptor.TipoSexo;
            suscriptorVm.IdLocalidad = suscriptor.Domicilio.Localidad.IdLocalidad;
            suscriptorVm.Localidad = suscriptor.Domicilio.Localidad;
            suscriptorVm.IdProvincia = suscriptor.Domicilio.Localidad.IdProvincia;
            suscriptorVm.Email = suscriptor.Email;
            suscriptorVm.TelefonoFijoNumero = suscriptor.TelefonoFijoNumero;
            suscriptorVm.TelefonoFijoArea = suscriptor.TelefonoFijoArea;
            suscriptorVm.TelefonoMovilArea = suscriptor.TelefonoMovilArea;
            suscriptorVm.TelefonoMovilNumero = suscriptor.TelefonoMovilNumero;
            suscriptorVm.CUIT = suscriptor.CUIT;
            suscriptorVm.RazonSocial = suscriptor.RazonSocial;
            suscriptorVm.CalleSuscriptor = suscriptor.Domicilio.Calle;
            suscriptorVm.AlturaSuscriptor = suscriptor.Domicilio.Altura.Value;
            suscriptorVm.PisoSuscriptor = suscriptor.Domicilio.Piso;
            suscriptorVm.DepartamentoSuscriptor = suscriptor.Domicilio.Departamento;
            suscriptorVm.CalleLateral1Suscriptor = suscriptor.Domicilio.CalleLateral1;
            suscriptorVm.CalleLateral2Suscriptor = suscriptor.Domicilio.CalleLateral2;
            suscriptorVm.BarrioSuscriptor = suscriptor.Domicilio.Barrio;
            suscriptorVm.ObservacionesDomicilioSuscriptor = suscriptor.Domicilio.Observaciones;

           
            return View(suscriptorVm);
        }

        // POST: Suscriptores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSuscriptor,TipoSuscriptor,NumeroSuscriptor,Nombre,Apellido,FechaNacimiento,NumeroSuscriptor,DNI,TipoSexo,Email,TelefonoFijoNumero,TelefonoFijoArea,TelefonoMovilNumero,TelefonoMovilArea,CUIT,RazonSocial,CalleSuscriptor,AlturaSuscriptor,PisoSuscriptor,DepartamentoSuscriptor,CalleLateral1Suscriptor,CalleLateral2Suscriptor,BarrioSuscriptor,ObservacionesDomicilioSuscriptor,IdLocalidad,IdProvincia")] SuscriptorVM suscriptorVM)
        {
            if (suscriptorVM.IdSuscriptor == null)
            {
                return HttpNotFound();
            }
            
            Suscriptor suscriptor = db.Suscriptores.Find(suscriptorVM.IdSuscriptor);

            if (suscriptor==null)
            {
                return HttpNotFound();
            }
            //Mapeo suscriptor
            suscriptor.EstadoSuscriptor = suscriptorVM.EstadoSuscriptor;
            suscriptor.Nombre = suscriptorVM.Nombre;
            suscriptor.Apellido = suscriptorVM.Apellido;
            suscriptor.TipoSuscriptor = suscriptorVM.TipoSuscriptor;
            suscriptor.FechaNacimiento = suscriptorVM.FechaNacimiento;
            suscriptor.NumeroSuscriptor = db.Suscriptores.Count() + 1;
            suscriptor.DNI = suscriptorVM.DNI;
            suscriptor.TipoSexo = suscriptorVM.TipoSexo;
            suscriptor.Email = suscriptorVM.Email;
            suscriptor.TelefonoFijoNumero = suscriptorVM.TelefonoFijoNumero;
            suscriptor.TelefonoFijoArea = suscriptorVM.TelefonoFijoArea;
            suscriptor.TelefonoMovilArea = suscriptorVM.TelefonoMovilArea;
            suscriptor.TelefonoMovilNumero = suscriptorVM.TelefonoMovilNumero;
            suscriptor.CUIT = suscriptorVM.CUIT;
            suscriptor.RazonSocial = suscriptorVM.RazonSocial;
            db.Entry(suscriptor).State = EntityState.Modified;
            db.SaveChanges();
            //Mapeo Domicilio suscriptor
            Domicilio domicilio = db.Domicilios.Find(suscriptor.IdDomicilio);
            domicilio.Calle = suscriptorVM.CalleSuscriptor;
            domicilio.Altura = suscriptorVM.AlturaSuscriptor;
            domicilio.Piso = suscriptorVM.PisoSuscriptor;
            domicilio.Departamento = suscriptorVM.DepartamentoSuscriptor;
            domicilio.CalleLateral1 = suscriptorVM.CalleLateral1Suscriptor;
            domicilio.CalleLateral2 = suscriptorVM.CalleLateral2Suscriptor;
            domicilio.Barrio = suscriptorVM.BarrioSuscriptor;
            domicilio.IdLocalidad = suscriptorVM.IdLocalidad;
            domicilio.Observaciones = suscriptorVM.ObservacionesDomicilioSuscriptor;
            db.Entry(domicilio).State = EntityState.Modified;
            db.SaveChanges();
            
            return RedirectToAction("Details", new { id = suscriptor.IdSuscriptor });
        }

        // GET: Suscriptores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suscriptor suscriptor = db.Suscriptores.Find(id);
            if (suscriptor == null)
            {
                return HttpNotFound();
            }
            return View(suscriptor);
        }

        // POST: Suscriptores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Suscriptor suscriptor = db.Suscriptores.Find(id);
            db.Suscriptores.Remove(suscriptor);
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
