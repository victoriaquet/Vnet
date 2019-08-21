using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class Domicilio
    {
        [Key]
        public int IdDomicilio { get; set; }

        public string Calle { get; set; }

        public int? Altura { get; set; }

        public string Piso { get; set; }

        public string Departamento { get; set; }

        public string CalleLateral1 { get; set; }

        public string CalleLateral2 { get; set; }

        public string Barrio { get; set; }

        public int IdLocalidad { get; set; }
        public virtual Localidad Localidad { get; set; }

        public string Observaciones { get; set; }

        public string TituloDom { get; set; }

    }
}