using System.ComponentModel.DataAnnotations;

namespace Blog_Management_System.Models.Tags
{
    public class Status : ITagFilter
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Forum>? Forums { get; set; }
    }
}
