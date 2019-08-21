using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class Caso
    {
        [Key]
        public int IdCaso { get; set; }

        public string Descripcion { get; set; } //detalles del caso

        public string Observacion { get; set; }//notas internas para el personal

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha carga")]
        public DateTime FechaCarga { get; set; }
        

        public virtual ICollection<HistorialCaso> HistorialCaso { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha cierre")]
        public DateTime? FechaCierre { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha vencimiento")]
        public DateTime? FechaVencimiento { get; set; }

        public int IdAsuntoCaso { get; set; }

        [Display(Name = "Área")]
        public virtual AsuntoCaso AsuntoCaso { get; set; }

        public int IdEstadoCaso { get; set; }
     
        public virtual EstadoCaso EstadoCaso { get; set; }

        public int IdSuscripcion { get; set; }

        public virtual Suscripcion Suscripcion { get; set; } //debe traer las suscripciones del suscriptor seleccionado
        
        //public virtual ICollection<SeguimientoCaso> SeguimientoCaso { get; set; }

        public int IdTipoCaso { get; set; }

        public virtual TipoCaso TipoCaso { get; set; }

        public int? IdCanillita { get; set; }
        public virtual Canillita Canillita { get; set; }

        [Display(Name = "Motivo cierre")]
        public int? IdMotivoCierreCaso { get; set; }
        public virtual MotivoCierreCaso MotivoCierreCaso { get; set; }


        public string UsuarioCarga { get; set; } 

        public virtual ICollection<DiaDevolucionCaso> DiasDevoluciones { get; set; } 




        


    }
}