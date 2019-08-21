using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class Venta
    {
        [Key]
        public int IdVenta { get; set; }

        public string Descripcion { get; set; }
        public decimal? Monto { get; set; }

        public DateTime? Fecha { get; set; }

        public int IdComercio { get; set; }
        public virtual Comercio Comercio { get; set; }

        public int IdTarjetaClub { get; set; }
        public virtual TarjetaClub TarjetaClub { get; set; }

        //Datos de Alta
        public string IdUsuarioAlta { get; set; }
        public DateTime? FechaAlta { get; set; }

    }
}