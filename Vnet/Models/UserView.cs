using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Vnet.Models
{
    public class UserView
    {
        [Key]
        public string UserID { get; set; }

        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public RoleView Role { get; set; }

        public List<RoleView> Roles { get; set; }

    }
}