using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
        public void Edit(Comment comment)
        {
            _blogManagementSystemDbContext.Entry(comment).State = EntityState.Detached;
            _blogManagementSystemDbContext.Comments.Update(comment);
            _blogManagementSystemDbContext.SaveChanges();
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
