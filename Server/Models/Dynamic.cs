using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Net6
{
    [Table("dynamic")]
    public partial class Dynamic
    {
        [Column("dynamic_time", TypeName = "datetime")]
        public DateTime DynamicTime { get; set; }
        [Column("dynamic_content")]
        [StringLength(50)]
        public string DynamicContent { get; set; } = null!;
        [Key]
        [Column("id")]
        public int Id { get; set; }
    }
}
