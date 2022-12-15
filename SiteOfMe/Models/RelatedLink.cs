using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DataAnnotationsExtensions;

namespace SiteOfMe.Models
{
    public class RelatedLink
    {
        public int RelatedLinkId { get; set; }
        
        [Required]
        [DisplayName("پست")]
        public int PostId { get; set; }

        [DisplayName("متن")]
        public string LinkText { get; set; }

        [Required]
        //[Url(false)]
        [DisplayName("آدرس")]
        public string Url { get; set; }
    }
}