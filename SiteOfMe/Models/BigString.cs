using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteOfMe.Models
{
    public class BigString
    {
        public int BigStringId { get; set; }

        [Required]
        [MinLength(7)]
        //[AllowHtml]
        [DisplayName("متن")]
        public string Value { get; set; }

        public static explicit operator BigString(string str)
        {
            return new BigString {Value = str};
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (BigString)) return false;
            return Equals((BigString) obj);
        }

        protected bool Equals(BigString other)
        {
            return BigStringId == other.BigStringId && string.Equals(Value, other.Value);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (BigStringId*397) ^ (Value != null ? Value.GetHashCode() : 0);
            }
        }
    }
}