using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class EstadoPago
    {
        [Key]
        public int IdEstadoPago { get; set; }


        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        //Pendiente = 0,
        //En_Proceso = 1,
        //Acreditado = 2,
        //Rechazado = 3
    }
}