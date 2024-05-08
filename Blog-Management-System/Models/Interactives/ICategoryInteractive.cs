using Blog_Management_System.Models.Tags;

namespace Blog_Management_System.Models.Interactives
{
    public interface ICategoryInteractive
    {
        List<Category> Categories { get; set; }
        void UpdateCategories();
    }
}
