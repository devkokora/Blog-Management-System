using System.ComponentModel.DataAnnotations;

namespace Blog_Management_System.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public List<Forum>? Forums { get; set; }
    }
}
