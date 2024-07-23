using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Net6
{
    [Table("collectionconcept")]
    public partial class CollectionConcept
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("userid")]
        public int UserId { get; set; }
        [Column("conceptid")]
        public int ConceptId { get; set; }
        [ForeignKey("ConceptId")]
        public Concept? LikeConcept { get; set; } = null!;
        [ForeignKey("UserId")]
        public User? LikeUser { get; set; } = null!;
    }
}