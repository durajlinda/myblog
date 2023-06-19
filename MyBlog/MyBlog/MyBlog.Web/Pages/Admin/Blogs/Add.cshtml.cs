using MyBlog.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBlog.Web.Data;
using System.Runtime.InteropServices;
using Bloggie.Web.Models.ViewModels;

namespace MyBlog.Web.Pages.Admin.Blogs
{
    public class AddModel : PageModel
    {
        private readonly MyBlogDbContext myblogDbContext;

        [BindProperty]
        public AddBlogPost AddBlogPostRequest { get; set; }

        public AddModel(MyBlogDbContext myblogDbContext)
        {
            this.myblogDbContext = myblogDbContext;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
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

            myblogDbContext.BlogPost.Add(blogPost);
            myblogDbContext.SaveChanges();

            return RedirectToPage("/Admin/Blogs/List");
        }

    }
}
