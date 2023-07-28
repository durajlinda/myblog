using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Server.IIS.Core;
using MyBlog.Web.Models.Domain;
using MyBlog.Web.Repositories;

namespace MyBlog.Web.Pages.Admin.Blog
{
    public class DetailsModel : PageModel
    {
        private readonly IBlogRepository blogRepository;

        public BlogPost BlogPost { get; set; }

        public int TotalLikes { get; set; }
        public bool Liked { get; set; }
        public IBlogPostLikeRepository blogPostLikeRepository { get; }
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public DetailsModel(IBlogRepository blogRepository,
            IBlogPostLikeRepository blogPostLikeRepository,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)

        {
            this.blogRepository = blogRepository;
            this.blogPostLikeRepository = blogPostLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> OnGet(string urlHandle)
        {
            BlogPost = await blogRepository.GetAsync(urlHandle);

            if (BlogPost != null)
            {
                if (signInManager.IsSignedIn(User))
                {
                    var likes = await blogPostLikeRepository.GetLikesForBlog(BlogPost.Id);

                    var userId = userManager.GetUserId(User);

                    Liked = likes.Any(x => x.UserId == Guid.Parse(userId));
                }


                TotalLikes = await blogPostLikeRepository.GetTotalLikesForBlog(BlogPost.Id);
            }




            return Page();
        }
    }
}