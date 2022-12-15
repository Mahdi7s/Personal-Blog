using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SiteOfMe.Models
{
    public class Appendix
    {
        public int AppendixId { get; set; }

        [Required]
        [DisplayName("مطلب")]
        public int PostId { get; set; }

        [Required]
        [DisplayName("نوع")]
        public string Type { get; set; }
        [Required]
        [DisplayName("نام")]
        public string Name { get; set; }
        [Required]
        [DisplayName("نام نمایشی")]
        public string DisplayName { get; set; }
        [Required]
        [DisplayName("محتوا")]
        public string Content { get; set; }

        public Post Post { get; set; }
    }
}