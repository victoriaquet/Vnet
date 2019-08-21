using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class CuentaBancaria
    {
        [Key]
        public int IdCuentaBancaria { get; set; }

        [Required]
        [MaxLength(35)]
        public string CBU { get; set; }
        [Required]
        [MaxLength(35)]
        [Display(Name = "Nombre")]
        public string NombreTitular { get; set; }

        [Display(Name = "DNI")]
        [Required]
        [Range(1000000, 99999999)]
        public int DNITitular { get; set; }

        public int IdDomicilio { get; set; }

        //[Display(Name = "Domicilio")]
        //public virtual Domicilio DomicilioTitular { get; set; }

        [Display(Name = "CUIT")]
        [Required]
        [MaxLength(11)]
        public string CUITTitular { get; set; }

        [Display(Name = "Alias cuenta")]
        public string AliasCuenta { get; set; }

        public virtual EntidadBancaria Banco { get; set; }

    }
}