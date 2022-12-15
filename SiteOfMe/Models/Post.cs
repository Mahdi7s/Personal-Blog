using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SiteOfMe.Models
{
    public class Post
    {
        public Post()
        {
            Views = 0;
        }

        //[ScaffoldColumn(false)]
        public int PostId { get; set; }
        public int UserId { get; set; }

        [Required]
        [DisplayName("عنوان")]
        public string Title { get; set; }

        //[Required]
        //[DisplayName("خلاصه متن")]
        public virtual BigString BodySummary { get; set; }

        //[Required]
        //[DisplayName("متن")]
        public virtual BigString Body { get; set; }

        [DisplayName("راست به چپ؟")]
        public bool IsRightToLeft { get; set; }

        [DisplayName("منتشر شده؟")]
        public bool IsPublished { get; set; }

        public DateTime PublishDate { get; set; }
        public int Views { get; set; }

        /// <summary>
        /// A relative path to App_Data/Thumbs/Posts
        /// </summary>
        public string Thumb { get; set; }

        public virtual User User { get; set; }
        public virtual List<RelatedLink> RelatedLinks { get; set; }
        public virtual List<PostRelate> PostRelates { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Tag> Tags { get; set; }
        public virtual List<Appendix> Appendices { get; set; }

        //-----------------------------------------------------------

        public static Post New(User currentUser)
        {
            return new Post
            {
                IsRightToLeft = true,
                Title = "پست جدید",
                PublishDate = DateTime.Now,
                User = currentUser,
                UserId = currentUser.UserId,
                BodySummary = new BigString { Value = "خلاصه متن" },
                Body = new BigString { Value = "متن مطلب" }
            };
        }
    }
}