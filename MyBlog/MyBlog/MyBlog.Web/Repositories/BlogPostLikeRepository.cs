using Microsoft.EntityFrameworkCore;
using MyBlog.Web.Data;
using MyBlog.Web.Models.Domain;

namespace MyBlog.Web.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly MyBlogDbContext myBlogDbContext;

        public BlogPostLikeRepository(MyBlogDbContext myBlogDbContext)
        {
            this.myBlogDbContext = myBlogDbContext;
        }

        public async Task AddLikeForBlog(Guid blogPostId, Guid userId)
        {
            var like = new BlogPostLike
            {
                Id = Guid.NewGuid(),
                BlogPostId = blogPostId,
                UserId = userId
            };
            await myBlogDbContext.BlogPostLike.AddAsync(like);
            await myBlogDbContext.SaveChangesAsync();
        }

        public  async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
        {
            return await myBlogDbContext.BlogPostLike
                .Where(x => x.BlogPostId == blogPostId)
                .ToListAsync();
        }

        public async Task<int> GetTotalLikesForBlog(Guid blogPostId)
        {
         return  await myBlogDbContext.BlogPostLike
                .CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}
