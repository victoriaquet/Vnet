using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{

    public class TipoPeriodoEfectivo
    {
        [Key]
        public int PeriodoPagoEfectivoId { get; set; }

        public string Nombre { get; set; }
    }
    //public enum TipoPeriodoEfectivo
    //{
    //    Mensual = 0,
    //    Bimestral = 1,
    //    Trimestral = 2,
    //    Cuatrimentral = 3,
    //    Semestral = 4,
    //    Anual = 5
    //}
}