using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class Canillita
    {
        [Key]
        public int IdCanillita { get; set; }

        public int? NroCanillita { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string NroTel { get; set; }

        public bool Activo { get; set; }

        public int? IdLocalidad { get; set; }
        public virtual Localidad Localidad { get; set; }

        public virtual ICollection<Suscripcion> Suscripciones { get; set; }
        public virtual ICollection<DiaEntrega> DiaEntregas { get; set; }

        public virtual ICollection<HojaDeRuta> HojaDeRuta { get; set; }

        public int? IdDistribuidor { get; set; }
        public virtual Distribuidor Distribuidor { get; set; }
    }
}