
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SiteOfMe.Models
{
    [Bind(Exclude = "DownFileId")]
    public class DownFile
    {
        public int DownFileId { get; set; }

        [Required]
        [DisplayName("لینک دانلود")]
        public string DownUrl { get; set; }

        [Required]
        [DisplayName("پسوند فایل")]
        public string Extension { get; set; }

        [DisplayName("توضیحات")]
        [StringLength(400)]
        public string Description { get; set; }
    }
}
