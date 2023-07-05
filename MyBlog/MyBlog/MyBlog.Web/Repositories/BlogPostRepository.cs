using MyBlog.Web.Models.Domain;
using MyBlog.Web.Data;
using Microsoft.EntityFrameworkCore;


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
            return await myblogDbContext.BlogPost.Include(nameof(BlogPost.Tags)).ToListAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync(string tagName)
        {
            return await (myblogDbContext.BlogPost.Include(nameof(BlogPost.Tags))
                .Where(x => x.Tags.Any(y => y.Name == tagName)))
                .ToListAsync();
        }

        public async Task<BlogPost> GetAsync(Guid id)
        {
            return await myblogDbContext.BlogPost.Include(nameof(BlogPost.Tags))
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost> GetAsync(string urlHandle)
        {
            return await myblogDbContext.BlogPost.Include(nameof(BlogPost.Tags))
             .FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = await myblogDbContext.BlogPost.Include(nameof(BlogPost.Tags))
                .FirstOrDefaultAsync(x => x.Id == blogPost.Id);

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

                if (blogPost.Tags != null && blogPost.Tags.Any())
                {
                    // Delete existing tags

                    myblogDbContext.Tags.RemoveRange(existingBlogPost.Tags);

                    // Add new tags
                    blogPost.Tags.ToList().ForEach(x => x.BlogPostId = existingBlogPost.Id);
                    await myblogDbContext.Tags.AddRangeAsync(blogPost.Tags);
                }

            }

            await myblogDbContext.SaveChangesAsync();
            return existingBlogPost;
        }

    }
}
