using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class DiaDevolucionCaso
    {
        [Key]
        public int IdDiaDevolucionCaso { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha")]
        public DateTime? Fecha { get; set; }

        public string Observaciones { get; set; }

        public string UsuarioModificacion { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha modificación")]
        public DateTime? FechaModificacion { get; set; }

        public virtual Caso Caso { get; set; }

    }
}