using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class CatalogoPremio
    {
        [Key]
        public int IdCatalogoPremio { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha carga")]
        public DateTime? FechaCarga { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha baja")]
        public DateTime? FechaBaja { get; set; }

        public virtual ICollection<Premio> Premios { get; set; }


    }
}