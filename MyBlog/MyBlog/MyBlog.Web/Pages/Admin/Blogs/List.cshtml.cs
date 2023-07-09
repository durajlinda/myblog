using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyBlog.Web.Data;
using MyBlog.Web.Models.Domain;
using MyBlog.Web.Models.ViewModels;
using MyBlog.Web.Repositories;
using System.Text.Json;
using static MyBlog.Web.Data.MyBlogDbContext;

namespace MyBlog.Web.Pages.Admin.Blogs
{
    public class ListModel : PageModel
    {
        private readonly MyBlogDbContext myblogDbContext;
        private readonly IBlogRepository blogRepository;

        public List<BlogPost> BlogPosts { get; set; }

        public ListModel(IBlogRepository blogRepository)
        {
            this.blogRepository = blogRepository;
        }

        public async Task OnGet()
        {
            var notificationJson = TempData["Notification"] as string;
            if (notificationJson != null )
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson.ToString());

            }

            BlogPosts = (await blogRepository.GetAllAsync())?.ToList();
        }
    }
}