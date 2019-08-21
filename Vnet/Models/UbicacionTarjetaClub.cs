using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public enum UbicacionTarjetaClub
    {
        //Se entrego la TC
        CallCenter = 1,

        //Dada de baja por diversos motivos
        Compras = 2,

        //solo cargaron los datos de las TC sin el pago
        Distribuidor = 3,

        EntregadaAlSuscriptor = 4
    }
}