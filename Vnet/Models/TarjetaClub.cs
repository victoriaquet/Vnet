using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class TarjetaClub
    {
        [Key]
        public int IdTarjetaClub { get; set; }
        [MaxLength(35)]
        public string Nombres { get; set; }
        [MaxLength(35)]
        public string Apellido { get; set; }

        public int? DNI { get; set; }
        public string Email { get; set; }

        [Display(Name = "tel. fijo")]
        public string TelefonoFijoNumero { get; set; }

        [Display(Name = "Área fijo")]
        public string TelefonoFijoArea { get; set; }

        [Display(Name = "Cel.")]
        public string TelefonoMovilNumero { get; set; }

        [Display(Name = "Área cel.")]
        public string TelefonoMovilArea { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Nacimiento")]
        public DateTime? FechaNacimiento { get; set; }

        public int IdSuscripcion { get; set; }
        public virtual Suscripcion Suscripcion { get; set; }
        
        public long Numero { get; set; }

        public decimal? Precio { get; set; }

        public EstadoTarjetaClub EstadoTarjetaClub { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Solicitud")]
        public DateTime? FechaSolicitud { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Solic. impresión")]
        public DateTime? FechaSolicitudImpresion { get; set; } //es la misma fecha que el alta del susc.

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "P/Imprimir")]
        public DateTime? FechaGeneracionArchivo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Solic. impresión")]
        public DateTime? FechaImpresion { get; set; } //es la misma fecha que el alta del susc.
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Para entregar")]
        public DateTime? FechaParaEntregar { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Entrega")]
        public DateTime? FechaEntrega { get; set; }
        

        public TipoTarjetaClub TipoTarjetaClub { get; set; }
        public virtual ICollection<TCporArchivo> TCporArchivo { get; set; }

        public UbicacionTarjetaClub UbicacionTarjetaClub { get; set; }

        public TipoSexo TipoSexo{ get; set; }


    }
}