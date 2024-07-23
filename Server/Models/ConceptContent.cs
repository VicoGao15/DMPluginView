using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Net6
{
    [Table("concept")]
    public partial class ConceptContent
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
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
    }
}
