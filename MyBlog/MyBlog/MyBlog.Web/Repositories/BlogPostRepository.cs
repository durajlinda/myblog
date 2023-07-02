using Microsoft.EntityFrameworkCore;
using MyBlog.Web.Data;
using MyBlog.Web.Models.Domain;

namespace MyBlog.Web.Repositories
{
    public class BlogPostRepository : IBlogRepository
    {
        private readonly MyBlogDbContext myblogDbContext;

        public BlogPostRepository(MyBlogDbContext myblogDbContext)
        {
            this.myblogDbContext = myblogDbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await myblogDbContext.BlogPost.AddAsync(blogPost);
            await myblogDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingBlog = await myblogDbContext.BlogPost.FindAsync(id);
            if (existingBlog != null)
            {
                myblogDbContext.BlogPost.Remove(existingBlog);
                await myblogDbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await myblogDbContext.BlogPost.ToListAsync();
        }

        public async Task<BlogPost> GetAsync(Guid id)
        {
            return await myblogDbContext.BlogPost.FindAsync(id);
        }

        public async Task<BlogPost> GetAsync(string urlHandle)
        {
            return await myblogDbContext.BlogPost.FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = await myblogDbContext.BlogPost.FindAsync(blogPost.Id);

            if (existingBlogPost != null)
            {
                existingBlogPost.Heading = blogPost.Heading;
                existingBlogPost.PageTitle = blogPost.PageTitle;
                existingBlogPost.Content = blogPost.Content;
                existingBlogPost.ShortDescription = blogPost.ShortDescription;
                existingBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlogPost.UrlHandle = blogPost.UrlHandle;
                existingBlogPost.PublishedDate = blogPost.PublishedDate;
                existingBlogPost.Author = blogPost.Author;
                existingBlogPost.Visible = blogPost.Visible;
            }

            await myblogDbContext.SaveChangesAsync();
            return existingBlogPost;
        }
    }
}
