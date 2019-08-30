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
        public long NroTarjeta { get; set; }

       
        public int? Descuento { get; set; }

        
        public decimal? Importe { get; set; }

        
        public decimal? ImporteDescuento { get; set; }

        
        public string Descripcion { get; set; }


        
        public int ComercioId { get; set; }
        [Required]
        public Comercio Comercio { get; set; }

        //Datos del archivo
        [Required]
        //[RegularExpression("^((?!SNC_Visa_)[a-zA-Z '])+$", ErrorMessage = "El nombre del archivo no coincide con el formato esperado.")]
        public string NombreArchivo { get; set; }
        public DateTime HoraDeSubida { get; set; }
    }
}