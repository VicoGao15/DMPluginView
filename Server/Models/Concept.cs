using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Net6
{
    [Table("concept")]
    public partial class Concept
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("module")]
        [StringLength(50)]
        public string? Module { get; set; }
        [Column("creater")]
        [StringLength(50)]
        public string? Creater { get; set; }
        [Column("create_Date", TypeName = "datetime")]
        public DateTime? Create_Date { get; set; }
        [Column("header")]
        [StringLength(50)]
        public string Header { get; set; } = null!;
        [Column("contents")]
        
        public string? Contents { get; set; }
        [Column("likes")]
        public int? Likes { get; set; }
        [Column("views")]
        public int? Views { get; set; }
        [Column("createrid")]
        public int? CreaterId { get; set; }
        [ForeignKey("CreaterId")]
        public User? UserInfo { get; set; } = null!;
        [Column("private")]
        public int? IsPrivate { get; set; }
    }
}
