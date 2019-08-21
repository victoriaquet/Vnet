using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace Vnet.Models
{
    public class EstadoSuscripcionHistorial
    {
        [Key]
        public int IdEstadoSuscripcionHistorial { get; set; }

        [Display(Name = "Estado Suscripción")]
        public EstadoSuscripcion EstadoSuscripcion { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha modif.")]
        public DateTime? FechaModificacion { get; set; }

       
        public MotivoEstadoSuscripcion MotivoEstadoSuscripcion { get; set; }

        public string Observaciones { get; set; }

        public int IdSuscripcion { get; set; }
        public virtual Suscripcion Suscripcion { get; set; }

        public string Responsable { get; set; }


    }
}