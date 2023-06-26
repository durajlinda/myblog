using MyBlog.Web.Models.Domain;

namespace MyBlog.Web.Repositories
{
    public interface IBlogRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost> GetAsync(Guid id);

        Task<BlogPost> AddAsync(BlogPost blogPost);
        Task<BlogPost> UpdateAsync(BlogPost blogPost);

        Task<bool> DeleteAsync(Guid id);

    }

}