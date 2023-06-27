using MyBlog.Web.Enums;

namespace MyBlog.Web.Models.ViewModels
{
    public class Notifications
    {
        public string Message { get; set; }

        public NotificationType Type{ get; set; }
    }
}
