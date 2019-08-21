using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
   
    public enum TipoSuscripcion
    {
        Normal = 1,
        Coorporativo = 2,

        [Display(Name = "Cortesía")]
        Cortesia = 3
        //Normal
        //Coorporativo Mismo Domicilio
        //Coorporativo Diferente Domicilio
    }
}