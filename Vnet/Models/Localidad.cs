using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class Localidad
    {
        [Key]
        public int IdLocalidad { get; set; }

        [Display(Name = "Ciudad")]
        public string Nombre { get; set; }

        public string Codigo { get; set; }

        public bool SoloParaPublicidad { get; set; }

        [Display(Name = "Cód. postal")]
        public string CodPostal { get; set; }

        public int IdProvincia { get; set; }

        public virtual Provincia Provincia { get; set; }

    }
}