using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class MotivoCierreCaso
    {
        [Key]
        public int IdMotivoCierreCaso { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
            //CASO RESUELTO
            //CASO NO RESUELTO
            //CASO MAL INFORMADO
            //NO ENTREGA DE DIARIOS, RECONOCIDO POR NORTE
    }
}