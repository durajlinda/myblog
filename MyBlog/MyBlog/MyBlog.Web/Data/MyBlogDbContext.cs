﻿using MyBlog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace MyBlog.Web.Data
{
    public class MyBlogDbContext : DbContext


    {
        public MyBlogDbContext(DbContextOptions<MyBlogDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPost{ get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogPostLike> BlogPostLike { get; set; }

        public DbSet<BlogPostComment> BlogPostComment { get; set; }


        /*public class myblogDbContext
        {
        }*/
    }
}
