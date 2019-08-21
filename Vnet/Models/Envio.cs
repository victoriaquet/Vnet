using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace Vnet.Models
{
    public class Envio
    {
        [Key]
        public int IdEnvio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha envío")]
        public DateTime FechaEnvio { get; set; }

        public Envio()
        {
            this.Pagos = new HashSet<Pago>();
        }

        public virtual ICollection<Pago> Pagos { get; set; }
        
         
    }
}