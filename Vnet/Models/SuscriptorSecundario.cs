using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class SuscriptorSecundario
    {
        [Key]
        public int IdSuscriptorSecundario { get; set; }

        public int IdSuscriptor { get; set; }

        public virtual Suscriptor Suscriptor { get; set; }

        public virtual ICollection<DiaEntrega> DiaEntregas { get; set; }

        public int IdTarjetaClub { get; set; }
        public virtual TarjetaClub TarjetaClub { get; set; }
        
    }
}