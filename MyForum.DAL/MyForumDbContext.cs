using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MyForum.BL.Entities;
namespace MyForum.DAL
{
    public class MyForumDbContext : IdentityDbContext<User>
    {
        public DbSet<Forum>? Forums { get; set; }
        public DbSet<Post>? Posts { get; set; }
        public DbSet<Comment>? Comments { get; set; }
        public override DbSet<User>? Users { get; set; }
        public MyForumDbContext(DbContextOptions<MyForumDbContext> options) : base(options)
        {
        }

    }
}
