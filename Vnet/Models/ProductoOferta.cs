using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class ProductoOferta
    {
        [Key,Column(Order = 0)]
        public int IdProducto { get; set; }

        public virtual Producto Producto { get; set; }

        [Key, Column(Order = 1)]
        public int IdOferta { get; set; }

        public virtual Oferta Oferta { get; set; }
        
        public int? Copias { get; set; }

    }
}