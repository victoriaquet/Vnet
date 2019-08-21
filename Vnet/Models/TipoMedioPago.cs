using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
  
    public class TipoMedioPago
    {
        [Key]
        public int TipoMedioPagoId { get; set; }

        public string Descripcion { get; set; }

        public bool GeneraArchivo { get; set; }

        //Tarjeta_de_Credito = 1,
        //Tarjeta_de_Debito = 2,
        //Cuenta_Caja_de_Ahorro = 3,
        //Efectivo = 4
    }
}