using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class AsuntoCaso
    {
        [Key]
        public int IdAsuntoCaso { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        public int IdAreaCaso { get; set; }

        public virtual AreaCaso AreaCaso { get; set; }
    }
}