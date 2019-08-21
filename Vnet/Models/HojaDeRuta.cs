using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class HojaDeRuta
    {
        [Key]
        public int IdHojaDeRuta { get; set; }

        public int IdCanillita { get; set; }
        public virtual Canillita Canillita { get; set; }

        public DateTime FechaEntrega { get; set; }

        public DateTime FechaGeneracion { get; set; }

        public virtual ICollection<SuscripcionHojaDeRuta> SuscripcionHojaDeRutas { get; set; }

        public virtual ICollection<Linea> Lineas { get; set; }
    }
}