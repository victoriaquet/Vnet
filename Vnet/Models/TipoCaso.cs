using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class TipoCaso
    {

        [Key]
        public int IdTipoCaso { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        //Consulta = 0,
        //Reclamo = 1, 
        //Sugerencia = 2,
        //Peticiones =3 (Pedidos de suscriptores)
        //Generado_Por_Agente (ej. caso generado por el call center, 
        ///////////////////////informe de no entrega por parte del diario/servinor/canillita)
    }
}