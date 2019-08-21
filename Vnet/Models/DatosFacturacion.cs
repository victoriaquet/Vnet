using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class DatosFacturacion
    {
        [Key]
        public int IdDatosFacturacion { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public int DNI { get; set; }

        public string CUIT { get; set; }

        [Display(Name = "Razón social")]
        public string RazonSocial { get; set; }

        public int IdDomicilio { get; set; }
        public virtual Domicilio Domicilio { get; set; }

        [Display(Name = "Tipo IVA")]
        public TipoIVA TipoIva { get; set; }

    }
}