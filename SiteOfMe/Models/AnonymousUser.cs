using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations.Schema;
using UrlAttribute = DataAnnotationsExtensions.UrlAttribute;

namespace SiteOfMe.Models
{
    public class AnonymousUser : IValidatableObject
    {
        public AnonymousUser()
        {
            EmailRequired = false;
            Name = "ناشناس";
        }

        [NotMapped]
        internal bool EmailRequired { get; set; }

        public int AnonymousUserId { get; set; }

        [Required]
        [DisplayName("نام")]
        public string Name { get; set; }

        [Email]
        [DataType(DataType.EmailAddress)]
        [DisplayName("ایمیل")]
        public string Email { get; set; }

        [Url(false)]
        [DataType(DataType.Url)]
        [DisplayName("سایت")]
        public string Website { get; set; }
        public string UserIP { get; set; }

        #region Implementation of IValidatableObject

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EmailRequired && string.IsNullOrEmpty(Email))
                yield return new ValidationResult("لطفا فیلد ایمیل را پر کنید", new[] { "Email" });            
        }

        #endregion
    }
}