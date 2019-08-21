using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class HistorialCaso
    {
        [Key]
        public int IdHistorialCaso { get; set; }

        public DateTime? Fecha { get; set; }

        public string UsuarioModificacion { get; set; }

        public virtual EstadoCaso EstadoCasoHistorial { get; set; }

        public string ObservacionesHistorial { get; set; }

        public string LogTxt { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha carga")]
        public DateTime FechaCarga { get; set; }//cuando se cargó

        public virtual Caso Caso { get; set; }
    }
}

