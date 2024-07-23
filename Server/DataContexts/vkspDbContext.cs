using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Net6
{
    public partial class vkspDbContext : DbContext
    {
        public vkspDbContext()
        {
        }

        public vkspDbContext(DbContextOptions<vkspDbContext> options)
            : base(options)
        {
        }
        
        public virtual DbSet<Comments> CommentNew { get; set; } = null!;
        public virtual DbSet<SdxBackup> SdxBackups { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<File> File { get; set; } = null;
        public virtual DbSet<Concept> Concepts { get; set; } = null!;
        public virtual DbSet<Dynamic> Dynamics { get; set; } = null!;
        public virtual DbSet<Todo> Todos { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<CollectionConcept> CollectionConcepts { get; set; } = null!;
        public virtual DbSet<ConceptTag> ConceptTags { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=172.25.13.72;Initial Catalog=vksp;User ID=Vico02;Password=@Vico202204;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Concept>(entity =>
            {
                entity.Property(e => e.Likes).HasDefaultValueSql("((0))");

                entity.Property(e => e.Views).HasDefaultValueSql("((0))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
