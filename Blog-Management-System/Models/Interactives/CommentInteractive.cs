using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Blog_Management_System.Models.Interactives
{
    public class CommentInteractive : ICommentInteractive
    {
        private readonly BlogManagementSystemDbContext _blogManagementSystemDbContext;
        public List<Comment>? Comments { get; set; }
        public CommentInteractive(BlogManagementSystemDbContext blogManagementSystemDbContext)
        {
            _blogManagementSystemDbContext = blogManagementSystemDbContext;
        }
        public List<Comment>? GetAll() => _blogManagementSystemDbContext.Comments.ToList();
        public Comment? GetById(int id) => _blogManagementSystemDbContext.Comments.Find(id);
        public void Create(Comment? comment)
        {
            if (comment is not null)
            {
                _blogManagementSystemDbContext.Comments.Add(comment);
                _blogManagementSystemDbContext.SaveChanges();
            }
        }
        public void Edit(Comment comment)
        {
            var tempComment = _blogManagementSystemDbContext.Comments.Find(comment.CommentId);
            if (tempComment is not null)
            {
                tempComment.Body = comment.Body;
                _blogManagementSystemDbContext.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            var tempComment = _blogManagementSystemDbContext.Comments.Find(id);
            if (tempComment is not null)
            {
                var forum = tempComment.Forum;
                if (forum is not null &&
                    forum.Comments is not null)
                {
                    forum.Comments.Remove(tempComment);
                }

                _blogManagementSystemDbContext.Comments.Remove(tempComment);
                _blogManagementSystemDbContext.SaveChanges();
            }
        }
    }
}
