using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteOfMe.Models
{
    [Bind(Exclude = "PostRelateId")]
    public class PostRelate
    {
        public int PostRelateId { get; set; }
        public int PostId { get; set; }

        [Required]
        [DisplayName("نوع مرتبط")]
        public string RelateType { get; set; }

        [Required]
        [DisplayName("مقدار")]
        public string RelateValue { get; set; }

        [DisplayName("متن")]
        public string RelateText { get; set; }

        public virtual Post Post { get; set; }
    }
}