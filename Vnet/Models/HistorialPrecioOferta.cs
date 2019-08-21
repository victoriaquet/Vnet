using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class HistorialPrecioOferta
    {
        [Key]
        public int IdHistorialPrecioOferta { get; set; }
        public decimal Precio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha inicio")]
        public DateTime FechaInicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha modif.")]
        public DateTime? FechaModificacion { get; set; }
        public UserView UsuarioModificacion { get; set; }


        public int IdOferta { get; set; }
        public virtual Oferta Oferta { get; set; }

    }
}