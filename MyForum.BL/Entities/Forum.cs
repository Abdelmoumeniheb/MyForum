using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.BL.Entities{
    public class Forum
    {
        [Key]
        public int IdForum { get; set; }
        public string? NameForum { get; set; }
        public byte[]? PictureForum { get; set; }
        public int IdUsercreated { get; set; }
        public DateTime PublishedDateTime { get; set; }
        public virtual List<Post>? Posts { get; set; } 
    }
}