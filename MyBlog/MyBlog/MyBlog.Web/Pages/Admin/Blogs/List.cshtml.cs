using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyBlog.Web.Data;
using MyBlog.Web.Models.Domain;
using MyBlog.Web.Repositories;
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
            BlogPosts = (await blogRepository.GetAllAsync())?.ToList();
        }
    }
}