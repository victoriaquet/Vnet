using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class Premio
    {
        [Key]
        public int IdPremio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha")]
        public DateTime FechaPremio { get; set; }

        public int IdSuscriptor { get; set; }   

        public virtual Suscriptor Suscriptor { get; set; }

        public int IdCatalogoPremio { get; set; }   

        public virtual CatalogoPremio CatalogoPremio { get; set; }


    }
}