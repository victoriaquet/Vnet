using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class Linea
    {
        [Key]
        public int IdLinea { get; set; }
        public int NumeroOrden { get; set; }
        public int NumeroSuscriptor { get; set; }
        public string NombreSuscriptor { get; set; }
        public string Domicilio { get; set; }
        public string Localidad { get; set; }
        public bool Lunes { get; set; }
        public bool Martes { get; set; }
        public bool Miercoles { get; set; }
        public bool Jueves { get; set; }
        public bool Viernes { get; set; }
        public bool Sabado { get; set; }
        public bool Domingo { get; set; }
        public bool PrimeraEntrega { get; set; }
        public string Observacion { get; set; }

        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public string NumeroSuscripcion { get; set; }
        public string ObservacionLimpia { get; set; }
        public bool UltimaEntrega { get; set; }

        public int IdHojaDeRuta { get; set; }
        public virtual HojaDeRuta HojaDeRuta { get; set; }
    }
}