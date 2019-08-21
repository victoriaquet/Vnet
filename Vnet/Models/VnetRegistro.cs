using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Vnet.Models
{
    public class VnetRegistro
    {
        public int Id { get; set; }

        [Required]
        public int NroRegistro { get; set; }

        [Required]
        public int NroEstablecimiento { get; set; }

        [Required]
        public int NroTerminal { get; set; }

        [Required]
        public DateTime FechaHoraMov { get; set; }

        [Required]
        public int NroTarjeta { get; set; }

       
        public int? Descuento { get; set; }

        
        public decimal? Importe { get; set; }

        
        public decimal? ImporteDescuento { get; set; }

        
        public string Descripcion { get; set; }
    }
}