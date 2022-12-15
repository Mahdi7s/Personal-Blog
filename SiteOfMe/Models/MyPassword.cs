using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SiteOfMe.Models
{
    public class MyPassword
    {
        public int PasswordId { get; set; }

        [Required]
        [DisplayName("عنوان")]
        public string Title { get; set; }
        [Required]
        [DisplayName("نام کاربری")]
        public string UserName { get; set; }
        [Required]
        [DisplayName("رمز عبور")]
        public string Password { get; set; }
        [DisplayName("توضیحات")]
        public string Description { get; set; }
    }
}