using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public enum MotivoEstadoSuscripcion
    {   
      // [Display(Name = "Alta")]
       //AltaOperador = 0,

        //[Display(Name = "Pedido suscriptor")]
        //PedidoSuscriptor = 1,

        //[Display(Name = "Falta de pago")]
        //FaltaDePago = 2,

        [Display(Name = "Asesoramiento erróneo")]
        AsesoramientoErroneo = 14,

        [Display(Name = "Cambio oferta")]
        CambioOferta = 9,

        [Display(Name = "Cambio suscriptor")]
        CambioSuscriptor = 8,

        [Display(Name = "Datos falsos")]
        DatosFalsos = 10,

        [Display(Name = "Deudor incobrable")]
        DeudorIncobrable = 7,

        [Display(Name = "No le dieron el beneficio")]
        NoLeDieronBeneficio = 2,

        [Display(Name = "No le interesa producto")]
        NoLeInteresaProducto = 12,

        [Display(Name = "No recibe diario")]
        NoRecibeDiario = 3,

        [Display(Name = "No recibió tarjeta")]
        NoRecibioTarjeta = 4,

        [Display(Name = "No utiliza beneficios")]
        NoUtilizaBeneficios = 1,

        [Display(Name = "Problemas de salud")]
        ProblemasDeSalud = 11,

        [Display(Name = "Problemas económicos")]
        ProblemasEconomicos = 6,

        [Display(Name = "Sin cobertura en la zona")]
        SinCoberturaEnZona = 5,

        [Display(Name = "Suscripción duplicada")]
        SuscripcionDuplicada = 13





    }
}