using Microsoft.EntityFrameworkCore;
using MyBlog.Web.Data;
using MyBlog.Web.Models.Domain;

namespace MyBlog.Web.Repositories
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly MyBlogDbContext myBlogDbContext;

        public BlogPostCommentRepository(MyBlogDbContext myBlogDbContext)
        {
            this.myBlogDbContext = myBlogDbContext;
        }
        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
        {
           await myBlogDbContext.BlogPostComment.AddAsync(blogPostComment);
           await myBlogDbContext.SaveChangesAsync();
           return blogPostComment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetAllAsync(Guid blogPostId)
        {
           return await myBlogDbContext.BlogPostComment.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }
    }
}
