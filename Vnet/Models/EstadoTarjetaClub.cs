using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public enum EstadoTarjetaClub
    {
        //Se entrego la TC
        Entregada = 1,

        //Dada de baja por diversos motivos
        Baja = 2,

        //solo cargaron los datos de las TC sin el pago
        [Display(Name = "Pendiente")]
        Pendiente = 3,

        //se registro el pago de la suscripción
       [Display(Name = "Derivada a impresión")]
        DerivadaAImpresion = 4,
       
       //se descargo el archivo de impresión
        [Display(Name = "Lista para Imprimir")]
        ListaParaImprimir = 5,

       //Se imprimió
        [Display(Name = "Impresa")]
        Impresa = 6,

        //lista para entregar al suscriptor
        [Display(Name = "Lista para entregar")]
        ListaParaEntregar = 7,

        //Baja temporal por suspension o falta de pago
        Inactiva = 0



    }
}