using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class EstadoCaso
    {
        [Key]
        public int IdEstadoCaso { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        public string Color { get; set; }

        //Abierto = 0,
        //Cerrado = 1,
        //En_Proceso = 2
    }
}