using MyBlog.Web.Models.Domain;

namespace MyBlog.Web.Repositories
{
    public interface IBlogPostCommentRepository
    {
        Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);

    }
}
