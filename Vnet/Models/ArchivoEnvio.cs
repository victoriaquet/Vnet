using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class ArchivoEnvio
    {
        [Key]
        public int IdArchivoEnvio { get; set; }

        [Display(Name = "Nro")]
        public int Numero { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? Fecha { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha creación")]
        public DateTime? FechaCreacion { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha respuesta")]
        public DateTime? FechaRespuesta { get; set; }

        public string Archivo { get; set; }

        [Display(Name = "Cant. registros")]
        public int? CantidadRegistros { get; set; }

        [Display(Name = "Importe")]
        public decimal? ImporteTotal { get; set; }


    }
}