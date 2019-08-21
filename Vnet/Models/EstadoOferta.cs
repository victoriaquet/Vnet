using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class EstadoOferta
    {
        [Key]
        public int IdEstadoOferta { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        public virtual ICollection<Oferta> Ofertas { get; set; }
        //Activa = 1,
        //Inactiva = 2
    }
}