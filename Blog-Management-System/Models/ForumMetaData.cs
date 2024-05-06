namespace Blog_Management_System.Models
{
    public class ForumMetaData
    {
        private List<CategoryType>? SelectedCategory { get; set; }
        private List<StatusType>? SelectedStatus { get; set; }
    }

    public enum CategoryType
    {
        Alien = 1,
        UFO,
        Dog,
        Cat,
        Nasa,
        Zombie
    }

    public enum StatusType
    {
        Hot = 1,
        New
    }
}
