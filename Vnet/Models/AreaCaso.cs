using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class AreaCaso
    {
        [Key]
        public int IdAreaCaso { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        public virtual ICollection<AsuntoCaso> AsuntosCasos { get; set; } 
    }
}