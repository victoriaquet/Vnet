using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class Suscripcion
    {
        [Key]
        public int IdSuscripcion { get; set; }

        public Suscripcion()
        {
            PrecioSuscripcion = 0;
            CantTarjetasExtras = 0;
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha alta")]
        public DateTime FechaAlta { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha solic. baja")]
        public DateTime? FechaSolicitudBaja { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha modif.")]
        public DateTime? FechaModificacion { get; set; }

        public int NumeroSuscripcion { get; set; }

        public virtual ICollection<DiaEntrega> DiaEntregas { get; set; }
        public virtual ICollection<SuscriptorSecundario> SuscriptorSecundarios { get; set; }

        [Display(Name = "Órdenes")]
        public virtual ICollection<Orden> Ordenes { get; set; }

        public virtual ICollection<Suspension> Suspensiones { get; set; }

        public virtual ICollection<EstadoSuscripcionHistorial> EstadoSuscripcionHistorial { get; set; }

        public virtual ICollection<Caso> Casos { get; set; }//reclamos, sugerencias, consultas

        public int? IdOferta { get; set; }

        public virtual Oferta Oferta { get; set; }

        public virtual ICollection<TarjetaClub>  TarjetaClub { get; set; }

        public int IdSuscriptor { get; set; }

        public virtual Suscriptor Suscriptor { get; set; }

        public UserView UsuarioAlta { get; set; }

        public UserView UsuarioModificacion { get; set; }

        public int? IdCanillita { get; set; }
        public virtual Canillita Canillita { get; set; }

        public TipoSuscripcion TipoSuscripcion { get; set; }

        public bool Lunes { get; set; }

        public bool Martes { get; set; }

        [Display(Name = "Miércoles")]
        public bool Miercoles { get; set; }

        public bool Jueves { get; set; }

        public bool Viernes { get; set; }

        [Display(Name = "Sábado")]
        public bool Sabado { get; set; }

        public bool Domingo { get; set; }

        public int? IdDatosFacturacion { get; set; }

        public virtual DatosFacturacion DatosFacturacion { get; set; }

        [Required]
        public decimal PrecioSuscripcion { get; set; }

        [Required]
        [Display(Name = "Tarjetas extras")]
        [Range(0, 9999)]
        public int CantTarjetasExtras { get; set; }

        public virtual ICollection<SuscripcionHojaDeRuta> SuscripcionHojaDeRutas { get; set; }

        public int? TipoMedioPagoId { get; set; }

        public virtual TipoMedioPago TipoMedioPago { get; set; }

        public virtual TipoPeriodoEfectivo TipoPeriodoEfectivo { get; set; }
        
        public virtual ICollection<TarjetaPago> Tarjetas { get; set; }

        public virtual ICollection<CuentaBancaria> CuentasBancarias { get; set; }

        public virtual ICollection<Premio> Premios { get; set; }
    }
}