using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SiteOfMe.Models
{
    public sealed class PrivateMessage
    {
        public PrivateMessage()
        {
            AnonymousUser = new AnonymousUser{EmailRequired = true};
        }

        public int PrivateMessageId { get; set; }
        public int AnonymousUserId { get; set; }       
        public int UserId { get; set; }
        public int? ReplyId { get; set; }

        [Required]
        [DisplayName("عنوان")]
        public string Title { get; set; }
        public BigString Message { get; set; }

        public AnonymousUser AnonymousUser { get; set; }
        public User User { get; set; }
        public PrivateMessage Reply { get; set; }
    }
}