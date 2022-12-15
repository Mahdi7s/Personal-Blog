using System.ComponentModel.DataAnnotations;

namespace SiteOfMe.ViewModels
{
    public class CommentReplyViewModel
    {
        public CommentReplyViewModel()
        {
            Reply = "Hey Your Reply Here !";
        }

        [Required]
        public int CommentId { get; set; }
        [Required]
        public string Reply { get; set; }
    }
}