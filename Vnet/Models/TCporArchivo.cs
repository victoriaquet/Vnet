using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class TCporArchivo
    {
        [Key, Column(Order = 0)]
        public int IdArchivoImpresionTC { get; set; }

        public virtual ArchivoImpresionTC ArchivoImpresionTc { get; set; }

        [Key, Column(Order = 1)]
        public int IdTarjetaClub { get; set; }

        public virtual TarjetaClub TarjetaClub { get; set; }

       
    }
}