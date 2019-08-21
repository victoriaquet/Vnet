//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;

//namespace SNC2017.Models
//{
//    public class SeguimientoCaso
//    {
//        [Key]
//        public int IdSeguimientoCaso { get; set; }

//        [Display(Name = "Descripción")]
//        public string Descripcion { get; set; }

//        [DataType(DataType.Date)]
//        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
//        [Display(Name = "Fecha suceso")]
//        public DateTime FechaSuceso { get; set; }//cuando ocurrió el evento

//        [DataType(DataType.Date)]
//        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
//        [Display(Name = "Fecha carga")]
//        public DateTime FechaCarga { get; set; }//cuando se cargó

//        public string Responsable { get; set; }


//    }
//}