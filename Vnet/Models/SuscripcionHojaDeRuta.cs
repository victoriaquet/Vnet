using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class SuscripcionHojaDeRuta
    {
        [Key]
        public int IdSuscripcionHojaDeRuta { get; set; }

        public int IdSuscripcion { get; set; }
        public virtual Suscripcion Suscripcion { get; set; }

        public int IdHojaDeRuta { get; set; }
        public virtual HojaDeRuta HojaDeRuta { get; set; }
    }
}