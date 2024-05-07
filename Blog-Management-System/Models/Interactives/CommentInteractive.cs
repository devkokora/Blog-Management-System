namespace Blog_Management_System.Models.Interactives
{
    public class CommentInteractive : ICommentInteractive
    {
        private readonly BlogManagementSystemDbContext _blogManagementSystemDbContext;

        public CommentInteractive(BlogManagementSystemDbContext blogManagementSystemDbContext)
        {
            _blogManagementSystemDbContext = blogManagementSystemDbContext;
        }
        public void Create(Comment? comment)
        {
            if (comment is not null)
            {
                _blogManagementSystemDbContext.Comments.Add(comment);
                _blogManagementSystemDbContext.SaveChanges();
            }
        }
    }
}
