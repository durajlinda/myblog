using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBlog.Web.Data;
using MyBlog.Web.Models.Domain;
using static MyBlog.Web.Data.MyBlogDbContext;

namespace MyBlog.Web.Pages.Admin.Blogs
{
    public class ListModel : PageModel
    {
        private readonly MyBlogDbContext myblogDbContext;

        public List<BlogPost> BlogPosts { get; set; }

        public ListModel(MyBlogDbContext myblogDbContext)
        {
            this.myblogDbContext = myblogDbContext;
        }

        public void OnGet()
        {
            BlogPosts = myblogDbContext.BlogPost.ToList();
        }
    }
}