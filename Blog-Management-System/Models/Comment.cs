using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog_Management_System.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "Message must be less than 250 characters.")]
        public string Body { get; set; } = string.Empty;
        [ValidateNever]
        public int Like { get; set; } = 0;
        [ValidateNever]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }

        public int ForumId { get; set; }
        [ForeignKey("ForumId")]
        public Forum? Forum { get; set; }
    }
}
