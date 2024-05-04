using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog_Management_System.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime Created_at { get; set; }
        public List<Forum>? Forums { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
