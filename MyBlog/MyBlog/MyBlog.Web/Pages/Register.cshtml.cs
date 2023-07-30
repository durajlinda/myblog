using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBlog.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace MyBlog.Web.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManger;

        [BindProperty]
        public Register RegisterViewModel { get; set; }

        public RegisterModel(UserManager<IdentityUser> userManger)
        {
            this.userManger = userManger;
        }
     
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = RegisterViewModel.Username,
                    Email = RegisterViewModel.Email
                };
                var identityResult = await userManger.CreateAsync(user, RegisterViewModel.Password);

                if (identityResult.Succeeded)

                {
                    var addRolesResult = await userManger.AddToRoleAsync(user, "User");

                    if (addRolesResult.Succeeded)
                    {
                        ViewData["Notification"] = new Notification
                        {
                            Type = Enums.NotificationType.Success,
                            Message = "You have been registered successfully"
                        };
                        return Page();
                    }
                }
                ViewData["Notification"] = new Notification
                {
                    Type = Enums.NotificationType.Error,
                    Message = "Something went wrong!"
                };
                return Page();
            }

            else
            {
                return Page();
            }


        }
    }

}
