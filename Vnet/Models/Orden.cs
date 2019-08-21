using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Services.Configuration;

namespace Vnet.Models
{
    public class Orden
    {
        [Key]
        public int IdOrden { get; set; }

        [Display(Name = "Nro. Orden")]
        public long Numero { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }

        public Decimal Monto { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Inicio susc.")]
        public DateTime? SuscripcionInicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fin susc.")]
        public DateTime? SuscripcionFin { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Libro")]
        public DateTime? FechaLibro { get; set; }

        public string NroLegalLetra { get; set; }

        public int? NroLegalPuntoVenta { get; set; }

        public int? NroLegalNumero { get; set; }

        public string ReciboNroLegalLetra { get; set; }

        public int? ReciboNroLegalPuntoVenta { get; set; }

        public int? ReciboNroLegalNumero { get; set; }

        public string Observaciones { get; set; }

        public int IdSuscripcion { get; set; }

        public virtual Suscripcion Suscripcion { get; set; }

        public EstadoOrden EstadoOrden { get; set; }

        public virtual DatosFacturacion DatosFacturacion { get; set; }

        public virtual ICollection<Pago> Pagos { get; set; }
        
     


     




    }
}