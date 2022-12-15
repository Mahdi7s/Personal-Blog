using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DataAnnotationsExtensions;

namespace SiteOfMe.ViewModels
{
    /// <summary>
    /// this is a user message to me
    /// </summary>
    public class ContactViewModel
    {
        [Email]
        [Required]
        [DisplayName("ایمیل شما")]
        public string UserEmail { get; set; }

        [Required]
        [DisplayName("عنوان")]
        public string Subject { get; set; }

        [Required]
        [DisplayName("متن پیام")]
        public string Body { get; set; }
    }
}