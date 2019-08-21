using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class MotivoSuspension
    {
        [Key]
        public int IdMotivoSuspension { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
    }
}