using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBlog.Web.Models.ViewModels;
using MyBlog.Web.Repositories;

namespace MyBlog.Web.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private readonly IUserRepository userRepository;
        public List<User> Users { get; set; }
        

        public IndexModel(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }


       

        [HttpGet]
        public async Task<IActionResult> OnGet()
        {
           var users = await userRepository.GetAll();

            Users = new List<User> ();

            foreach (var user in users)
            {
                Users.Add(new Models.ViewModels.User()
                {
                    Id = Guid.Parse(user.Id),
                    Username = user.UserName,
                    Email = user.Email

                });
            }
            return Page();
        }
    }
}
