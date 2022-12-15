using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SiteOfMe.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("نام کاربری")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("رمز عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("به خاطر سپردن؟")]
        public bool RememberMe { get; set; }
    }
}