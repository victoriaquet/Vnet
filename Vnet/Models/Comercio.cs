using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class Comercio
    {
        [Key]
        public int IdComercio { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string RazonSocial { get; set; }

        public string Direccion { get; set; }

        public string Localidad { get; set; }

        public int? Establecimiento { get; set; }

        public string Posnet { get; set; }

        public string Lapos { get; set; }

        public string Beneficio { get; set; }

        [Required]
        public string Email { get; set; }

        public DateTime FechaAdhesion { get; set; }

        public bool AdhesionActiva { get; set; }

        public virtual ICollection<Venta> Ventas { get; set; }

        //Datos de Alta
        public string IdUsuarioAlta { get; set; }
        public DateTime? FechaAlta { get; set; }

        public string Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}