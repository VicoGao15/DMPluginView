using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Net6
{
    [Table("tag")]
    public partial class Tag
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("tagname")]
        [StringLength(50)]
        public string? TagName { get; set; } = null!;
    }
}
