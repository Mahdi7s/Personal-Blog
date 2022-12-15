using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteOfMe.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public DateTime PublishDate { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
    }
}