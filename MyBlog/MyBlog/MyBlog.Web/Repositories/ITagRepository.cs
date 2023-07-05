using MyBlog.Web.Models.Domain;

namespace MyBlog.Web.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync();
    }
}
