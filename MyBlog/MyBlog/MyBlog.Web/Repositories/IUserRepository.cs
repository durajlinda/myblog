using Microsoft.AspNetCore.Identity;

namespace MyBlog.Web.Repositories
{
    public interface IUserRepository
    {
       Task<IEnumerable<IdentityUser>> GetAll();

        Task <bool>Add(IdentityUser identityuser, string password, List<string>roles);
    }
}
