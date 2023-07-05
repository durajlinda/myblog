using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBlog.Web.Models.Domain;
using MyBlog.Web.Repositories;

namespace MyBlog.Web.Pages.Tags
{
    public class DetailsModel : PageModel
    {
        private readonly IBlogRepository blogRepository;
        public List<BlogPost> Blogs{ get; set; }

        public DetailsModel(IBlogRepository blogRepository)
        {
            this.blogRepository = blogRepository;
        }

        public async Task<IActionResult> OnGet(string tagName)
        {
            Blogs = (await blogRepository.GetAllAsync(tagName)).ToList();
            return Page();
        }
    }
}
