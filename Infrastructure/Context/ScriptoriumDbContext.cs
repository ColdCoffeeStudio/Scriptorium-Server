using System;
using System.Collections.Generic;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Infrastructure.Context;

public partial class ScriptoriumDbContext : DbContext
{
    public ScriptoriumDbContext()
    {
    }

    public ScriptoriumDbContext(DbContextOptions<ScriptoriumDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Annotation> Annotations { get; set; }

    public virtual DbSet<Encyclopedium> Encyclopedia { get; set; }

    public virtual DbSet<Scribe> Scribes { get; set; }

    public virtual DbSet<Theme> Themes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_uca1400_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Annotation>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.ToTable("Annotation");

            entity.HasIndex(e => e.encyclopediaId, "Annotation_Encyclopedia_id_fk");

            entity.HasIndex(e => e.themeId, "Annotation_Theme_Id_fk");

            entity.Property(e => e.id).HasColumnType("int(11)");
            entity.Property(e => e.contentUrl)
                .HasMaxLength(255)
                .HasDefaultValueSql("'notImplemented.md'");
            entity.Property(e => e.encyclopediaId).HasColumnType("int(11)");
            entity.Property(e => e.endPage).HasColumnType("int(11)");
            entity.Property(e => e.startPage).HasColumnType("int(11)");
            entity.Property(e => e.tags)
                .HasMaxLength(50)
                .HasDefaultValueSql("''");
            entity.Property(e => e.themeId).HasColumnType("int(11)");
            entity.Property(e => e.title).HasMaxLength(255);

            entity.HasOne(d => d.encyclopedia).WithMany(p => p.Annotations)
                .HasForeignKey(d => d.encyclopediaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Annotation_Encyclopedia_id_fk");

            entity.HasOne(d => d.theme).WithMany(p => p.Annotations)
                .HasForeignKey(d => d.themeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Annotation_Theme_Id_fk");
        });

        modelBuilder.Entity<Encyclopedium>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasIndex(e => e.scribeId, "Encyclopedia_Scribe_id_fk");

            entity.Property(e => e.id).HasColumnType("int(11)");
            entity.Property(e => e.scribeId).HasMaxLength(36);
            entity.Property(e => e.title).HasMaxLength(50);

            entity.HasOne(d => d.scribe).WithMany(p => p.Encyclopedia)
                .HasForeignKey(d => d.scribeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Encyclopedia_Scribe_id_fk");
        });

        modelBuilder.Entity<Scribe>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.ToTable("Scribe");

            entity.HasIndex(e => e.username, "Scribe_username_uk").IsUnique();

            entity.Property(e => e.id).HasMaxLength(36);
            entity.Property(e => e.username).HasMaxLength(50);
        });

        modelBuilder.Entity<Theme>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.ToTable("Theme");

            entity.HasIndex(e => e.name, "Theme_name_uk").IsUnique();

            entity.Property(e => e.id).HasColumnType("int(11)");
            entity.Property(e => e.folder).HasMaxLength(50);
            entity.Property(e => e.name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
