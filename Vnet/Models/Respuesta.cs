using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services.Configuration;

namespace Vnet.Models
{
    public class Respuesta
    {
        [Key]
        public int IdRespuesta { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }

        public bool Aprobado { get; set; }

        public string Observaciones { get; set; }

        public bool Manual { get; set; }

        public virtual UserView UsuarioCarga { get; set; }

        public int IdEnvio { get; set; }

        public virtual Envio Envio { get; set; }
    }
}