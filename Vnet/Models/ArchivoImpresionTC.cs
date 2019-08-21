using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class ArchivoImpresionTC
    {
        [Key]
        public int IdArchivoImpresionTC { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [MaxLength(35)]
        public string NombreArchivo { get; set; }
        [MaxLength(300)]
        public string Observacion { get; set; }

        public string Responsable { get; set; }

        public virtual ICollection<TCporArchivo> TCporArchivo { get; set; }

        public byte[] ArchivoTxt { get; set; }
    }
}