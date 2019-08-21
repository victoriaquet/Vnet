using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class TipoTarjeta
    {
        [Key]
        public int TipoTarjetaId { get; set; }
        public string Descripcion { get; set; }
        //Visa = 0,
        //Master = 1,
        //Naranja = 2,
        //Tuya = 3,
        //Cabal = 4
    }
   
}