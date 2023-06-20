using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBlog.Web.Data;
using MyBlog.Web.Models.Domain;
using System.Reflection.Metadata.Ecma335;

namespace MyBlog.Web.Pages.Admin.Blogs
{

    
    public class EditModel : PageModel
    {
        private readonly MyBlogDbContext myblogdbcontext;

        [BindProperty]
        public BlogPost BlogPost { get; set; }


        public EditModel(MyBlogDbContext myblogdbcontext )
        {
            this.myblogdbcontext = myblogdbcontext;
        }
        public void OnGet(Guid id)
        {
           BlogPost = myblogdbcontext.BlogPost.Find(id);

            
        }

        public IActionResult OnPostEdit()
        {
          var  existingBlogPost = myblogdbcontext.BlogPost.Find(BlogPost.Id);

            if (existingBlogPost != null)
            {
                existingBlogPost.Heading = BlogPost.Heading;
                existingBlogPost.PageTitle = BlogPost.PageTitle;
                existingBlogPost.Content = BlogPost.Content;
                existingBlogPost.ShortDescription = BlogPost.ShortDescription;
                existingBlogPost.FeaturedImageUrl = BlogPost.FeaturedImageUrl;
                existingBlogPost.UrlHandle = BlogPost.UrlHandle;
                existingBlogPost.PublishedDate = BlogPost.PublishedDate;
                existingBlogPost.Author = BlogPost.Author;
                existingBlogPost.Visible = BlogPost.Visible;
            }
           myblogdbcontext.SaveChanges();

            return RedirectToPage("/Admin/Blogs/List");
        }

        [HttpPost]
        public IActionResult OnPostDelete()
        {
            var existingBlogPost = myblogdbcontext.BlogPost.Find(BlogPost.Id);
            if (existingBlogPost != null)
            {
                myblogdbcontext.BlogPost.Remove(existingBlogPost);
                myblogdbcontext.SaveChanges();
                return RedirectToPage("/Admin/Blogs/List");
            }
            return Page();


        }


    }
}
