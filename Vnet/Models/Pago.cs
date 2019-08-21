using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Services.Configuration;

namespace Vnet.Models
{
    public class Pago
    {
        [Key]
        public int IdPago { get; set; }

        public Decimal Monto { get; set; }

        [Display(Name = "Medio pago")]
        public TipoMedioPago MedioPago { get; set; }

        [Display(Name = "Estado")]
        public EstadoPago EstadoPago { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }

        public int? IdTarjetaPago { get; set; }
        public virtual TarjetaPago Tarjeta { get; set; }

        public int? IdCuentaBancaria { get; set; }
        public virtual CuentaBancaria CuentaBancaria { get; set; }

        public int IdOrden { get; set; }  

        public virtual Orden Orden { get; set; }

        public Pago()
        {
            this.Envios = new HashSet<Envio>();
        }

        public virtual ICollection<Envio> Envios { get; set; }
    }
}