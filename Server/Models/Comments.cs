using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Net6
{
    [Table("comments")]
    public partial class Comments
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("commentperson")]
        [StringLength(50)]
        public string Commentperson { get; set; } = null!;
        [Column("commentcontent")]
        public string? Commentcontent { get; set; }
        [Column("commenttime", TypeName = "datetime")]
        public DateTime? Commenttime { get; set; }
        [Column("conceptid")]
        public int Conceptid { get; set; }
        [Column("replycomment")]
        public Comments? ReplyComment { get; set; }
        //[Column("up_user")]
        //public ICollection<User>? Up_User { get; set; } = new HashSet<User>();
        //[Column("down_user")]
        //public ICollection<User>? Down_User { get; set; } = new HashSet<User>();
    }
}
