using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace Vnet.Models
{
    public class Suspension //días que no se entregan a pedido explícito del suscriptor
    {
        [Key]
        public int IdSuspension { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Desde")]
        public DateTime FechaDesde { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hasta")]
        public DateTime? FechaHasta { get; set; }

        public string Observaciones { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha carga")]
        public DateTime FechaCarga { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha modif.")]
        public DateTime? FechaModificacion { get; set; }

        public UserView UsuarioCarga { get; set; }

        public UserView UsuarioModificacion { get; set; }

        public int IdSuscripcion { get; set; }

        public virtual Suscripcion Suscripcion { get; set; }

        public virtual MotivoSuspension MotivoSuspension { get; set; }



    }
}