using MyBlog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace MyBlog.Web.Data
{
    public class MyBlogDbContext : DbContext


    {
        public MyBlogDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPost{ get; set; }
        public DbSet<Tag> Tag { get; set; }
    }
}
