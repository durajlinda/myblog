using Microsoft.AspNetCore.Mvc;
using MyBlog.Web.Models.ViewModels;
using MyBlog.Web.Repositories;

namespace MyBlog.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogPostLikeController : Controller
    {
        private readonly IBlogPostLikeRepository blogPostLikeRepository;

        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository)
        {
            this.blogPostLikeRepository = blogPostLikeRepository;
        }
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddBlogPostLikeRequest addBlogPostLikeRequest)
        {
            await blogPostLikeRepository.AddLikeForBlog(addBlogPostLikeRequest.BlogPostId,
                addBlogPostLikeRequest.UserId);
            return Ok();
        }
    }
}
