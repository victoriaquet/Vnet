using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class Provincia
    {
        [Key]
        public int IdProvincia { get; set; }

        [Display(Name = "Provincia")]
        public string Nombre { get; set; }

        public bool SoloParaPublicidad { get; set; }
        public bool SoloParaSuscripcion { get; set; }

        public virtual ICollection<Localidad> Localidades { get; set; } 
    } 
}