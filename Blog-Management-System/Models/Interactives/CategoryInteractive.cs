using Blog_Management_System.Models.Tags;

namespace Blog_Management_System.Models.Interactives
{
    public class CategoryInteractive : ICategoryInteractive
    {
        private readonly BlogManagementSystemDbContext _blogManagementSystemDbContext;
        public List<Category> Categories { get; set; } = new();
        public CategoryInteractive(BlogManagementSystemDbContext blogManagementSystemDbContext)
        {
            _blogManagementSystemDbContext = blogManagementSystemDbContext;
            LoadCategory();
        }
        public void LoadCategory()
        {
            Categories = [.. _blogManagementSystemDbContext.Categories];
        }
    }
}
