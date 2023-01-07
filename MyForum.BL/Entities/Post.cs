using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.BL.Entities
{
    public class Post{
    
    [Key]
    public int IdPost { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateTime PublishedDateTime { get; set; }
    public string? IdUsercreated { get; set; }
    public virtual List<Comment>? Comments { get; set; }
    public int IdForum { get; set; }
    public Forum? Forum { get; set; }
    }
}
