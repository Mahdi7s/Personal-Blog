using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteOfMe.Models
{
    public class User
    {
        [ScaffoldColumn(false)]
        public int UserId { get; set; }
                
        [Required]
        [DisplayName("نام کاربری")]
        [Remote("CheckUserName", "Account", ErrorMessage = "نام کاربری وارد شده قابل قبول نمی باشد")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("نام نمایشی")]
        public string DisplayName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(5)]
        [DisplayName("رمز عبور")]
        public string Password { get; set; }

        [Required]
        [Email]
        [DisplayName("ایمیل")]
        [Remote("CheckEmail", "Account", ErrorMessage = "ایمیل وارد شده قبلا به ثبت رسیده")]
        public string Email { get; set; }

        [DisplayName("آدرس تصویر شما")]
        public string ImageAddress { get; set; }

        //[StringLength(4000)]
        //[DisplayName("درباره")]
        public virtual BigString About { get; set; }

        public virtual List<Role> Roles { get; set; }
        public virtual List<Post> Posts { get; set; }

        [NotMapped]
        [Required]
        [DisplayName("تائید رمز عبور")]
        [DataType(DataType.Password)]
        [EqualTo("Password")]
        public string PasswordConfirm { get; set; }
    }
}