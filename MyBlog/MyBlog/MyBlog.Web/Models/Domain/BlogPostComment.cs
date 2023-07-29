﻿namespace MyBlog.Web.Models.Domain
{
    public class BlogPostComment
    {
        public Guid id { get; set; }
        public string Description { get; set; }

        public Guid BlogPostId { get; set; }

        public Guid UserId { get; set; }

        public  DateTime DateAdded { get; set; }
    }
}
