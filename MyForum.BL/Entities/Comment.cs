using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.BL.Entities
{
    public class Comment
    {
        [Key]
        public int IdComment { get; set; }
        public string? Content { get; set; }
        public string? IdUsercreated { get; set; }
        public DateTime Datecreated { get; set; }
        public int IdPost { get; set; }
        public Post? Post { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
