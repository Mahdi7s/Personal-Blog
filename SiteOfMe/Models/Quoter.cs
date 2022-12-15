using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteOfMe.Models
{
    public class Quoter
    {
        public int QuoterId { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// A relative path to App_Data/Thumbs/Quoters
        /// </summary>
        public string Thumb { get; set; }
    }
}