using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vnet.Models
{
    public class RoleView
    {
        [Key]
        public string RoleID { get; set; }

        public string Name { get; set; }

    }
}