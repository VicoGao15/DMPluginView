using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Net6
{
    [Table("file")]
    public partial class File
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("filename")]
        [StringLength(50)]
        public string FileName { get; set; } = null!;
        [Column("suffix")]
        [StringLength(10)]
        public string? Suddix { get; set; }
        [Column("conceptid")]
        public int Conceptid { get; set; }
        [Column("zise")]
        public int? Szie { get; set; }
        [Column("downloadurl")]
        [StringLength(100)]
        public string DownloadUrl { get; set; }
    }
}
