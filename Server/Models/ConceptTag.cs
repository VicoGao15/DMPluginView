using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Net6
{
    [Table("concepttag")]
    public partial class ConceptTag
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("conceptid")]
        public int ConceptId { get; set; }
        [Column("tagid")]
        public int TagId { get; set; }
        [ForeignKey("ConceptId")]
        public Concept? ConceptDetail { get; set; } = null!;
        [ForeignKey("TagId")]
        public Tag? TagDetail { get; set; } = null!;
    }
}