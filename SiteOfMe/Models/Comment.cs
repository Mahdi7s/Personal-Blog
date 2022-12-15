using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAnnotationsExtensions;

namespace SiteOfMe.Models
{    
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public int AnonymousUserId { get; set; }
        public int PostId { get; set; }
        public int? PublisherId { get; set; }
        public int? ReplyId { get; set; }
        
        [DisplayName("عنوان")]
        public string Title { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("متن")]        
        public BigString Body { get; set; }

        public bool IsReply { get; set; }
        public bool IsVisible { get; set; }
        public DateTime PublishDate { get; set; }

        public virtual AnonymousUser AnonymousUser { get; set; }
        public Post Post { get; set; }
        public User Publisher { get; set; }
        public Comment Reply { get; set; }
    }
}