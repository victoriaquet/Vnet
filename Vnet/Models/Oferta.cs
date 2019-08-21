using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class Oferta
    {
        [Key]
        public int IdOferta { get; set; }
        [Required]
        [Display(Name = "Cód.")]
        public int Codigo { get; set; }

        [Required]
        [MaxLength(35)]
        public string Nombre { get; set; }

        public Editorial Editorial { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(300)]
        public string Descripcion { get; set; }
        [Required]
        public decimal Precio { get; set; }

        [Display(Name = "Cant. Días")]
        [Range(0, 6)]
        public int CantDias { get; set; }
        [Required]
        [Display(Name = "Tarjetas adicionales")]
        [Range(0, 9999)]
        public int CantTarjetasAdicionales { get; set; }

        public int? IdEstadoOferta { get; set; }

        [Display(Name = "Estado")]
        public virtual EstadoOferta EstadoOferta { get; set; }

        public TipoOferta TipoOferta { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha carga")]
        public DateTime FechaCarga { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Inicio")]
        public DateTime FechaInicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Modif.")]
        public DateTime? FechaModificacion { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha fin")]
        public DateTime? FechaFin { get; set; }

        public UserView UsuarioCarga { get; set; }

        public UserView UsuarioModificacion { get; set; }

        public virtual ICollection<ProductoOferta> ProductoOfertas { get; set; }

        public virtual ICollection<Suscripcion> Suscripciones { get; set; }

        public virtual ICollection<HistorialPrecioOferta> HistorialPrecioOfertas { get; set; }

        public string CodigoProductoServinor { get; set; }
        public string NombreProductoServinor { get; set; }
    }
}