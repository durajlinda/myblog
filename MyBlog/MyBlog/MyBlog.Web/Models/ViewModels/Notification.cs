using MyBlog.Web.Enums;

namespace MyBlog.Web.Models.ViewModels
{
    public class Notification
    {
        public string Message { get; set; }

        public NotificationType Type{ get; set; }
    }
}
