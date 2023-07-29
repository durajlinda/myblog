using MyBlog.Web.Models.Domain;
using System.Collections;

namespace MyBlog.Web.Repositories
{
    public interface IBlogPostCommentRepository
    {
        Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);

        Task<IEnumerable<BlogPostComment>> GetAllAsync(Guid blogPostId);

    }
}
