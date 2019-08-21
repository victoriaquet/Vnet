using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }

        public int? Codigo { get; set; }

        [Required]
        [MaxLength(35)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }
        public virtual ICollection<ProductoOferta> ProductoOfertas { get; set; }

        public Decimal? Monto { get; set; }


    }
}