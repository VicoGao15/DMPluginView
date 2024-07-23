using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Net6
{
    [Table("todoneed")]
    public partial class TodoNeed
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("title")]
        public string Title { get; set; } = null!;
        [Column("contents")]
        public DateTime? Deadline { get; set; }
        [Column("status")]
        public int Status { get; set; }
        [Column("types")]
        public int Types { get; set; }
        [Column("remark")]
        public int? Priority { get; set; }
        [Column("author")]
        [StringLength(50)]
        public string? Author { get; set; }
        [Column("userid")]
        public int UserId { get; set; }
        [Column("creattime")]
        public DateTime? Creattime { get; set; }
        [Column("edittime")]
        public DateTime? Edittime { get; set; }
        [Column("needworkhour")]
        public int? Needworkhour { get; set; }
        [Column("schedule")]
        public int? Schedule { get; set; }
    }
}
