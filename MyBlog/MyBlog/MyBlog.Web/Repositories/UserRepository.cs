using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBlog.Web.Data;

namespace MyBlog.Web.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext authDbContext;
        private readonly UserManager<IdentityUser> userManager;

        public UserRepository(AuthDbContext  authDbContext,
            UserManager<IdentityUser>userManager)
        {
            this.authDbContext = authDbContext;
            this.userManager = userManager;
        }

        public async Task <bool>Add(IdentityUser identityuser, string password, List<string> roles)
        {
           var identityResult = await userManager.CreateAsync(identityuser, password);

            if (identityResult.Succeeded)
            {
                identityResult= await userManager.AddToRolesAsync(identityuser, roles);

                if (identityResult.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
           var users = await authDbContext.Users.ToListAsync();
            var superAdminUser = await authDbContext.Users.
                FirstOrDefaultAsync(x => x.Email == "superadmin@myblog.com");

            if (superAdminUser != null)
            {
                users.Remove(superAdminUser);
            }

            return users;
        }
    }
}
