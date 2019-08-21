using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class EntidadBancaria
    {
        [Key]
        public int IdEntidadBancaria { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
    }
}