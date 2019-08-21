using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class Distribuidor
    {
        [Key]
        public int IdDistribuidor { get; set; }

        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public string NombreLocalidad { get; set; }

        public int? IdLocalidad { get; set; }
        public virtual Localidad Localidad { get; set; }

        public virtual ICollection<Canillita> Canillitas { get; set; }
    }
}