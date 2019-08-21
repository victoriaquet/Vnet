using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet
    .Models
{
    public enum TipoTarjetaClub
    {
       Adicional = 0,

       Titular = 1,

       [Display(Name = "Cortesía")]
       Cortesia = 2
    }
}