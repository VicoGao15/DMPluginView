using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Net6
{
    [Table("comment")]
    public partial class Comment
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
        [Column("userid")]
        public int? Userid { get; set; }
        public User? UserInfo { get; set; } = null!;
    }
}
