using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog_Management_System.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string Body { get; set; } = string.Empty;
        public int Like { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }

        public int ForumId { get; set; }
        [ForeignKey("ForumId")]
        public Forum? Forum { get; set; }
    }
}
