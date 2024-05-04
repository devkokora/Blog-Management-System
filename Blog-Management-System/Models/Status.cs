using System.ComponentModel.DataAnnotations;

namespace Blog_Management_System.Models
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public List<Forum>? Forums { get; set; }
    }
}
