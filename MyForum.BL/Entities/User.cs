using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace MyForum.BL.Entities
{
    public class User : IdentityUser 
    {
        public string? UserN { get; set; }
        public string? ProfilePicture { get; set; } = "Userdafault.png";
        public virtual List<Forum> Forums { get; set; }
        public virtual List<Post> Posts { get; set; }
    }
}
