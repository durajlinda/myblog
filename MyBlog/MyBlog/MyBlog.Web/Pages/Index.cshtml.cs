using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBlog.Web.Models.Domain;
using MyBlog.Web.Repositories;

namespace MyBlog.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBlogRepository blogRepository;

        public List<BlogPost> Blogs { get; set; }

        public IndexModel(ILogger<IndexModel> logger , IBlogRepository blogRepository)
        {
            _logger = logger;
            this.blogRepository = blogRepository;
        }

        public async Task<IActionResult> OnGet()
        {
           Blogs= (await blogRepository.GetAllAsync()).ToList();
            return Page();
           
        }
    }
}