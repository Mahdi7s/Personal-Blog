using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteOfMe.Models
{
    [Bind(Exclude = "RoleId")]
    public class Role
    {
        [ScaffoldColumn(false)]
        public int RoleId { get; set; }
        public string Name { get; set; }

        public virtual List<User> Users { get; set; } 
    }
}