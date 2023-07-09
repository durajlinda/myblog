using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyBlog.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override  void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var superAdminRoleId = "9313e084-b72f-4b10-bd85-f8354afeea9e";
            var AdminRoleId = "6f2ec4fa-859b-4e8b-a72c-ea156cfef1f1";
            var UserRoleId = "abcc9364-8856-41da-9aef-b43f683dddab";

            //Seed roles (Super Admin,Admin, User)
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {Name = "SuperAdmin", NormalizedName = "SUPERADMIN ",Id =superAdminRoleId, ConcurrencyStamp =superAdminRoleId},
                new IdentityRole()
                {Name = "Admin", NormalizedName = "ADMIN" , Id=AdminRoleId , ConcurrencyStamp =AdminRoleId},
                new IdentityRole()
                {Name = "User", NormalizedName = "USER" ,Id =UserRoleId , ConcurrencyStamp = UserRoleId}
            };
            builder.Entity<IdentityRole>().HasData(roles);  

            //Seed the Super Admin users
            var superAdminId = "ad085342-f8e6-411e-8c9b-3e41dfb4a1a2";
            var superAdminUser = new IdentityUser()
            {
                Id = superAdminId,
                UserName = "superadmin@myblog.com",

                Email = "superadmin@myblog.com"


            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "superadmin123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //Add all roles to Super Admin  users
            var superAdminUserRoles = new List<IdentityUserRole<string>>()   
            {
                new IdentityUserRole<string>()
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>()
                {
                    RoleId = AdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>()
                {
                    RoleId = UserRoleId,
                    UserId = superAdminId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminUserRoles);


        }
    }
}
