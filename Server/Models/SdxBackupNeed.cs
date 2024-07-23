using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Net6
{
    [Table("sdxbackup")]
    public partial class SdxBackupNeed
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("build")]
        [StringLength(20)]
        public string Build { get; set; } = null!;
        [Column("version")]
        [StringLength(20)]
        public string Version { get; set; }
        [Column("iskeyversion")]
        public bool IsKeyVersion { get; set; }
        [Column("keyversion")]
        [StringLength(20)]
        public string KeyVersion { get; set; }
        [Column("remark")]
        [StringLength(200)]
        public string Remark { get; set; }
        [Column("offlineinstaller")]
        [StringLength(100)]
        public string Offlineinstaller { get; set; }

    }
}
