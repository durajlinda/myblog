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

        [BindProperty]
        public Guid BlogPostId { get; set; }
        [BindProperty]
        public string CommentDescription { get; set; }
        public IBlogPostLikeRepository blogPostLikeRepository { get; }
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IBlogPostCommentRepository blogPostCommentRepository;

        public DetailsModel(IBlogRepository blogRepository,
            IBlogPostLikeRepository blogPostLikeRepository,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IBlogPostCommentRepository blogPostCommentRepository
            )

        {
            this.blogRepository = blogRepository;
            this.blogPostLikeRepository = blogPostLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.blogPostCommentRepository = blogPostCommentRepository;
        }

        public async Task<IActionResult> OnGet(string urlHandle)
        {
            BlogPost = await blogRepository.GetAsync(urlHandle);

            if (BlogPost != null)
            {
                BlogPostId = BlogPost.Id;
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

        public async Task<IActionResult> OnPost()
        {

            if (signInManager.IsSignedIn(User)&& !string.IsNullOrWhiteSpace(CommentDescription))
            {
                var userId = userManager.GetUserId(User);
                var comment = new BlogPostComment
                {
                    BlogPostId = BlogPost.Id,
                    Description = CommentDescription,

                    DateAdded = DateTime.Now,
                    UserId = Guid.Parse(userId)
                };
                await blogPostCommentRepository.AddAsync(comment);
            }
            return Page();
           
          
           
        }
    }
}