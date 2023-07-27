using MyBlog.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBlog.Web.Data;
using System.Runtime.InteropServices;
using MyBlog.Web.Models.ViewModels;
using MyBlog.Web.Repositories;
using MyBlog.Web.Enums;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace MyBlog.Web.Pages.Admin.Blogs
{
    [Authorize(Roles = "Admin")]
    public class AddModel : PageModel
    {
        private readonly MyBlogDbContext myblogDbContext;
        private readonly IBlogRepository blogpostRepository;

        [BindProperty]
        public AddBlogPost AddBlogPostRequest { get; set; }

        [BindProperty]
        public IFormFile FeaturedImage { get; set; }

        [BindProperty]
        public string  Tags { get; set; }

        public AddModel(IBlogRepository blogpostRepository)
        {
            
            this.blogpostRepository = blogpostRepository;
        }

        public void OnGet()
        {
        }

       public async Task<IActionResult> OnPost()
        {
            var blogPost = new BlogPost()
            {
                Heading = AddBlogPostRequest.Heading,
                PageTitle = AddBlogPostRequest.PageTitle,
                Content = AddBlogPostRequest.Content,
                ShortDescription = AddBlogPostRequest.ShortDescription,
                FeaturedImageUrl = AddBlogPostRequest.FeaturedImageUrl,
                UrlHandle = AddBlogPostRequest.UrlHandle,
                PublishedDate = AddBlogPostRequest.PublishedDate,
                Author = AddBlogPostRequest.Author,
                Visible = AddBlogPostRequest.Visible,
                Tags = new List<Tag>(Tags.Split(',').Select(x => new Tag() { Name = x.Trim() }))
            };

            await  blogpostRepository.AddAsync(blogPost);

            var notification= new Notification
            {
                Type = Enums.NotificationType.Success,
               Message = "Blog post added successfully",
               
            };
               TempData["Notification"] = JsonSerializer.Serialize(notification);

            return RedirectToPage("/Admin/Blogs/List");
        }

    }
}
