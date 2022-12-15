using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteOfMe.ViewModels
{
    public class RecentCommentViewModel
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string AuthorName { get; set; }
    }
}