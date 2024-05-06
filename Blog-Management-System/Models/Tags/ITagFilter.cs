namespace Blog_Management_System.Models.Tags
{
    public interface ITagFilter
    {
        int Id { get; set; }
        string Name { get; set; }
        List<Forum>? Forums { get; set; }
    }
}
