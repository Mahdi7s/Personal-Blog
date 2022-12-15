using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SiteOfMe.Models
{
    public class Quote
    {
        public int QuoteId { get; set; }
        public int QuoterId { get; set; }

        [Required]
        [DisplayName("متن نقل قول")]
        public string QuoteText { get; set; }

        [DisplayName("راست به چپ؟")]
        public bool IsRightToLeft { get; set; }

        public DateTime? FirstShownTime { get; set; }
        public DateTime? LastShownTime { get; set; }

        public virtual Quoter Quoter { get; set; }
    }
}