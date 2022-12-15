using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteOfMe.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// A relative path to App_Data/Thumbs/Tags
        /// </summary>
        public string Thumb { get; set; }

        public virtual List<Post> Posts { get; set; }       
    }
}