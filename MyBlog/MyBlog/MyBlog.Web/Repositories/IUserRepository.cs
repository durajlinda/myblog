using Microsoft.AspNetCore.Identity;

namespace MyBlog.Web.Repositories
{
    public interface IUserRepository
    {
       Task<IEnumerable<IdentityUser>> GetAll();
    }
}
