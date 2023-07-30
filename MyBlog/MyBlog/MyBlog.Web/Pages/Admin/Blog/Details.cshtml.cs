using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Server.IIS.Core;
using MyBlog.Web.Models.Domain;
using MyBlog.Web.Models.ViewModels;
using MyBlog.Web.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Web.Pages.Admin.Blog
{
    public class DetailsModel : PageModel
    {
        private readonly IBlogRepository blogRepository;

        public BlogPost BlogPost { get; set; }

        public List<BlogComment> Comments { get; set; }


        public int TotalLikes { get; set; }
        public bool Liked { get; set; }

        [BindProperty]
        public Guid BlogPostId { get; set; }

        [BindProperty]
        [Required]
        [MinLength(5)]
        [MaxLength(200)]
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

            await GetBlog(urlHandle);

            return Page();
        }


        public async Task<IActionResult> OnPost(string urlHandle)
        {
            if (signInManager.IsSignedIn(User) && !string.IsNullOrWhiteSpace(CommentDescription))
            {
                var userId = userManager.GetUserId(User);

                var comment = new BlogPostComment()
                {
                    BlogPostId = BlogPostId,
                    Description = CommentDescription,
                    DateAdded = DateTime.Now,
                    UserId = Guid.Parse(userId)
                };

                await blogPostCommentRepository.AddAsync(comment);

                // After adding the comment, redirect to the correct blog details page using the provided urlHandle
                return RedirectToPage("/Blog/Details", new { urlHandle = urlHandle });
            }

            // If the comment was not added (e.g., the user is not signed in or the comment is empty), continue with the regular flow
            await GetBlog(urlHandle);
            return Page();
        }



        private async Task GetComments()
        {
            var blogPostComments = await blogPostCommentRepository.GetAllAsync(BlogPost.Id);

            var blogCommentsViewModel = new List<BlogComment>();
            foreach (var blogPostComment in blogPostComments)
            {
                blogCommentsViewModel.Add(new BlogComment
                {
                    DateAdded = blogPostComment.DateAdded,
                    Description = blogPostComment.Description,
                    Username = (await userManager.FindByIdAsync(blogPostComment.UserId.ToString())).UserName
                });
            }

            Comments = blogCommentsViewModel;
        }


        private async Task GetBlog(string urlHandle)
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

                    await GetComments();


                }


                TotalLikes = await blogPostLikeRepository.GetTotalLikesForBlog(BlogPost.Id);
            }


        }
    }
}