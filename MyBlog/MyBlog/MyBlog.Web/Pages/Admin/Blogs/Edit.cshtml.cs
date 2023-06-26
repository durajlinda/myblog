using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBlog.Web.Data;
using MyBlog.Web.Models.Domain;
using MyBlog.Web.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace MyBlog.Web.Pages.Admin.Blogs
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public BlogPost BlogPost { get; set; }
        public IBlogRepository BlogpostRepository { get; }

        public EditModel(IBlogRepository blogpostRepository)
        {
            BlogpostRepository = blogpostRepository;
        }

        public async Task OnGet(Guid id)
        {
            BlogPost = await BlogpostRepository.GetAsync(id);
        }

        public async Task<IActionResult> OnPostEdit()
        {
            await BlogpostRepository.UpdateAsync(BlogPost);
            return RedirectToPage("/Admin/Blogs/List");
        }

        [HttpPost]
        public async Task<IActionResult> OnPostDelete()
        {
            var deleted = await BlogpostRepository.DeleteAsync(BlogPost.Id);
            if (deleted)
            {
                return RedirectToPage("/Admin/Blogs/List");
            }

            return Page();
        }
    }
}