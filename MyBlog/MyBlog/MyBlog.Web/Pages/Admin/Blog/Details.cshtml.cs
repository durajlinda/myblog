using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBlog.Web.Models.Domain;
using MyBlog.Web.Repositories;

namespace MyBlog.Web.Pages.Admin.Blog
{
    public class DetailsModel : PageModel
    {
        private readonly IBlogRepository blogRepository;

        public BlogPost BlogPost { get; set; }

        public DetailsModel(IBlogRepository blogRepository)
        {
            this.blogRepository = blogRepository;
        }

        public async Task<IActionResult> OnGet(string urlHandle)
        {
            BlogPost = await blogRepository.GetAsync(urlHandle);


            return Page();
        }

    }

}
