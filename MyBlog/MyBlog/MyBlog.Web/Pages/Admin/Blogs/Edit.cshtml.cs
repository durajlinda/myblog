using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBlog.Web.Data;
using MyBlog.Web.Enums;
using MyBlog.Web.Models.Domain;
using MyBlog.Web.Models.ViewModels;
using MyBlog.Web.Repositories;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace MyBlog.Web.Pages.Admin.Blogs
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        [BindProperty]
        public BlogPost BlogPost { get; set; }

        [BindProperty]
        public IFormFile FeaturedImage { get; set; }

        [BindProperty]
        public string Tags { get; set; }
        public IBlogRepository BlogpostRepository { get; }

        public EditModel(IBlogRepository blogpostRepository)
        {
            BlogpostRepository = blogpostRepository;
        }

        public async Task OnGet(Guid id)
        {
            BlogPost = await BlogpostRepository.GetAsync(id);
            if (BlogPost != null && BlogPost.Tags != null)
            {
                Tags = string.Join(',', BlogPost.Tags.Select(x => x.Name));
            }
        }

        public async Task<IActionResult> OnPostEdit()
        {
            try
            {
                BlogPost.Tags = new List<Tag>(Tags.Split(',').Select(x => new Tag { Name = x.Trim() }));
                await BlogpostRepository.UpdateAsync(BlogPost);
                ViewData["Notification"] = new Notification
                {
                    Message = "Blog post updated successfully",
                    Type = NotificationType.Success
                };

            }
            catch (Exception e)
            {
                ViewData["Notification"] = new

                {

                    Type = NotificationType.Error,
                    Message = "Blog post update failed",
                };


            }
            return Page();
        }

        [HttpPost]
        public async Task<IActionResult> OnPostDelete()
        {
            var deleted = await BlogpostRepository.DeleteAsync(BlogPost.Id);
            if (deleted)

            {
                var notification = new Notification
                {

                    Type = NotificationType.Success,
                    Message = "Blog post deleted successfully"
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/Admin/Blogs/List");
            }

            return Page();
        }
    }
}