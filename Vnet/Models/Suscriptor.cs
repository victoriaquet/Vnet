using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Vnet.Models
{
    public class Suscriptor
    {
        [Key]
        public int IdSuscriptor { get; set; }

        public EstadoSuscriptor EstadoSuscriptor { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Nacimiento")]
        public DateTime? FechaNacimiento { get; set; }

        [Display(Name = "Nro. Susc.")]
        public int NumeroSuscriptor { get; set; }

        public int DNI { get; set; }

        [Display(Name = "Sexo")]
        public TipoSexo TipoSexo { get; set; }


        public string Email { get; set; }

        public int IdDomicilio { get; set; }
        
        public virtual Domicilio Domicilio { get; set; }

        [Display(Name = "tel. fijo")]
        public string TelefonoFijoNumero { get; set; }

        [Display(Name = "Área fijo")]
        public string TelefonoFijoArea { get; set; }

        [Display(Name = "Cel.")]
        public string TelefonoMovilNumero { get; set; }

        [Display(Name = "Área cel.")]
        public string TelefonoMovilArea { get; set; }

        public string CUIT { get; set; }

        [Display(Name = "Razón social")]
        public string RazonSocial { get; set; }

        public virtual ICollection<TarjetaPago> Tarjetas { get; set; }//crédito, débito, etc

        public virtual ICollection<Suscripcion> Suscripciones { get; set; }



        public virtual ICollection<CuentaBancaria> CuentasBancarias { get; set; }

        public TipoSuscriptor TipoSuscriptor { get; set; }

    }
}