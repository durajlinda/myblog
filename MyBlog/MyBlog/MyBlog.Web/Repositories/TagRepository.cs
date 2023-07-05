using MyBlog.Web.Data;
using MyBlog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace MyBlog.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly MyBlogDbContext myBlogDbContext;

        public TagRepository(MyBlogDbContext myBlogDbContext)
        {
             this.myBlogDbContext = myBlogDbContext;
        }

        

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
          var tags = await myBlogDbContext.Tags.ToListAsync();

           return tags.DistinctBy(x => x.Name.ToLower());

        }
    }
}
