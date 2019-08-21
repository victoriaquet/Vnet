using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class TarjetaPago
    {
        [Key]
        public int IdTarjetaPago { get; set; }

        [Display(Name = "Nro. tarjeta")]
        [Required]
        public string Numero { get; set; }

        [Display(Name = "Nombre exacto")]
        [Required]
        public string NombreExacto { get; set; }

        [Display(Name = "Mes venc.")]
        [Required]
        [Range(1,12)]
        public int MesVencimiento { get; set; }

        [Display(Name = "Año venc.")]
        [Required]
        [Range(2000, 2999)]
        public int AñoVencimiento { get; set; }

        [Display(Name = "Cód. seguridad")]
        [Required]
        public string CodigoSeguridad { get; set; }

        //public TipoPeriodoEfectivo TipoPeriodoEfectivo { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        //[Display(Name = "desde")]
        //[Required]
        //public DateTime FechaDesde { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        //[Display(Name = "hasta")]
        //public DateTime? FechaHasta { get; set; }

        public virtual TipoTarjeta TipoTarjeta { get; set; }
        //Visa = 0,
        //Master = 1,
        //Naranja = 2,
        //Tuya = 3,
        //Cabal = 4

        public virtual EntidadBancaria Banco { get; set; }

    }
}