using MyBlog.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBlog.Web.Data;
using System.Runtime.InteropServices;
using MyBlog.Web.Models.ViewModels;
using MyBlog.Web.Repositories;

namespace MyBlog.Web.Pages.Admin.Blogs
{
    public class AddModel : PageModel
    {
        private readonly MyBlogDbContext myblogDbContext;
        private readonly IBlogRepository blogpostRepository;

        [BindProperty]
        public AddBlogPost AddBlogPostRequest { get; set; }

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
                Visible = AddBlogPostRequest.Visible
            };

          await  blogpostRepository.AddAsync(blogPost);

            return RedirectToPage("/Admin/Blogs/List");
        }

    }
}
