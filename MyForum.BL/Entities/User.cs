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
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? UsernameChangeLimit { get; set; } = 10;
        public string? ProfilePicture { get; set; }
        public virtual List<Forum> Forums { get; set; }
    }
}
