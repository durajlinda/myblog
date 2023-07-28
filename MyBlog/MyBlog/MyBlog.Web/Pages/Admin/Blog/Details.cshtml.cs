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

        public int TotalLikes { get; set; }
        public IBlogPostLikeRepository blogPostLikeRepository { get; } // Change property name to lowercase

        public DetailsModel(IBlogRepository blogRepository,
            IBlogPostLikeRepository blogPostLikeRepository)
        {
            this.blogRepository = blogRepository;
            this.blogPostLikeRepository = blogPostLikeRepository; // Corrected variable name

        }

        public async Task<IActionResult> OnGet(string urlHandle)
        {
            BlogPost = await blogRepository.GetAsync(urlHandle);

            if (BlogPost != null)
            {
                TotalLikes = await blogPostLikeRepository.GetTotalLikesForBlog(BlogPost.Id);
            }

            return Page();
        }
    }
}
