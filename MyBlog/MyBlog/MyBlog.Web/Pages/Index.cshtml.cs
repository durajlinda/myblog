using MyBlog.Web.Models.Domain;
using MyBlog.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace MyBlog.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBlogRepository blogRepository;
        private readonly ITagRepository tagRepository;

        public List<BlogPost> Blogs { get; set; }
        public List<Tag> Tags { get; set; }

        public IndexModel(ILogger<IndexModel> logger , IBlogRepository blogRepository , ITagRepository tagRepository)
        {
            _logger = logger;
            this.blogRepository = blogRepository;
            this.tagRepository = tagRepository;
        }

        public async Task<IActionResult> OnGet()
        {
           Blogs= (await blogRepository.GetAllAsync()).ToList();
          Tags = (await tagRepository.GetAllAsync()).ToList();
            return Page();
           
        }
    }
}