using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class DiaEntrega
    {
        [Key]
        public int IdDiaEntrega { get; set; }

        public NombreDia NombreDia { get; set; }//lunes, martes...

        public bool Habilitado { get; set; }

        public int? IdCanillita { get; set; }
        public virtual Canillita Canillita { get; set; }

        public int IdDomicilio { get; set; }

        public virtual Domicilio Domicilio { get; set; }

        [Display(Name = "Horarios entrega")]
        public string HorarioEntrega { get; set; }

        [Display(Name = "Observaciones")]
        public string Observacion { get; set; }

        public int? IdSuscripcion { get; set; }
        public virtual Suscripcion Suscripcion { get; set; }




    }
}