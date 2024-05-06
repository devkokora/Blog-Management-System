using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blog_Management_System.Models.Tags;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Blog_Management_System.Models;

public class Forum
{
    [Key]
    public int ForumId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public int Like { get; set; }
    public DateTime Created_at { get; set; }
    public List<int>? CategoriesId { get; set; }
    public List<Category>? Categories { get; set; }
    public List<Status>? Statuses { get; set; }
    public List<Comment>? Comments { get; set; }
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }
}
