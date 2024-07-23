using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Net6
{
    [Table("user")]
    public partial class User
    {
        [Column("username")]
        [StringLength(50)]
        [Unicode(false)]
        public string Username { get; set; } = null!;
        [Column("password")]
        [StringLength(250)]
        [Unicode(false)]
        public string Password { get; set; } = null!;
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("avatarurl")]
        [StringLength(150)]
        [Unicode(false)]
        public string? AvatarUrl { get; set; } = null!;
        [Column("signature")]
        [StringLength(150)]
        [Unicode(false)]
        public string? Signature { get; set; } = null!;
    }
}
