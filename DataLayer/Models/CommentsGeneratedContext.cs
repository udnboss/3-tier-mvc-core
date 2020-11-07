using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataLayer.Models
{
    public partial class CommentsGeneratedContext : DbContext
    {
        public CommentsGeneratedContext()
        {
        }

        public CommentsGeneratedContext(DbContextOptions<CommentsGeneratedContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TComment> TComment { get; set; }
        public virtual DbSet<TCommentVote> TCommentVote { get; set; }
        public virtual DbSet<TDomain> TDomain { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\sql2019;Database=COMMENTS;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<TComment>(entity =>
            {
                entity.ToTable("T_Comment");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Comment).IsRequired();

                entity.Property(e => e.DatePosted).HasColumnType("datetime");

                entity.Property(e => e.DomainId)
                    .IsRequired()
                    .HasColumnName("DomainID")
                    .HasMaxLength(50);

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasColumnName("IP")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Path).IsRequired();

                entity.HasOne(d => d.Domain)
                    .WithMany(p => p.TComment)
                    .HasForeignKey(d => d.DomainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Comment_T_Domain");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_T_Comment_T_Comment");
            });

            modelBuilder.Entity<TCommentVote>(entity =>
            {
                entity.ToTable("T_CommentVote");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasColumnName("IP")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.TCommentVote)
                    .HasForeignKey(d => d.CommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_CommentVote_T_Comment");
            });

            modelBuilder.Entity<TDomain>(entity =>
            {
                entity.ToTable("T_Domain");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Host).HasMaxLength(50);
            });
        }
    }
}
