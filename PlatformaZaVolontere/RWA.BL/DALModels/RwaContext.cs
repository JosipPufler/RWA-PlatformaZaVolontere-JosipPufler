using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RWA.BL.DALModels;

public partial class RwaContext : DbContext
{
    public RwaContext()
    {
    }

    public RwaContext(DbContextOptions<RwaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectSkillSet> ProjectSkillSets { get; set; }

    public virtual DbSet<ProjectType> ProjectTypes { get; set; }

    public virtual DbSet<ProjectUser> ProjectUsers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SkillSet> SkillSets { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserSkillSet> UserSkillSets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("name=ConnectionStrings:RWAcs");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Genre__3214EC07D950ED42");

            entity.ToTable("Genre");

            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Idlog).HasName("PK__Log__95D002086586FCF1");

            entity.ToTable("Log");

            entity.Property(e => e.Idlog).HasColumnName("IDLog");
            entity.Property(e => e.Level).HasDefaultValueSql("((1))");
            entity.Property(e => e.Timestamp).HasColumnType("datetime");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Idproject).HasName("PK__Project__B0529955D9CD732E");

            entity.ToTable("Project");

            entity.HasIndex(e => e.Title, "UQ__Project__2CB664DC29450936").IsUnique();

            entity.Property(e => e.Idproject).HasColumnName("IDProject");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.PublishDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.TypeId).HasColumnName("TypeID");

            entity.HasOne(d => d.Type).WithMany(p => p.Projects)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Project__TypeID__5B0E7E4A");
        });

        modelBuilder.Entity<ProjectSkillSet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProjectS__3214EC276CF41B0B");

            entity.ToTable("ProjectSkillSet");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.SkillSetId).HasColumnName("SkillSetID");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectSkillSets)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__ProjectSk__Proje__5DEAEAF5");

            entity.HasOne(d => d.SkillSet).WithMany(p => p.ProjectSkillSets)
                .HasForeignKey(d => d.SkillSetId)
                .HasConstraintName("FK__ProjectSk__Skill__5EDF0F2E");
        });

        modelBuilder.Entity<ProjectType>(entity =>
        {
            entity.HasKey(e => e.IdprojectType).HasName("PK__ProjectT__6DBF51BB5F9DE1D1");

            entity.ToTable("ProjectType");

            entity.Property(e => e.IdprojectType).HasColumnName("IDProjectType");
            entity.Property(e => e.Name).HasMaxLength(25);
        });

        modelBuilder.Entity<ProjectUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProjectU__3214EC27DC69C2C4");

            entity.ToTable("ProjectUser");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectUsers)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__ProjectUs__Proje__658C0CBD");

            entity.HasOne(d => d.User).WithMany(p => p.ProjectUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ProjectUs__UserI__668030F6");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Idrole).HasName("PK__Role__A1BA16C4C33BE877");

            entity.ToTable("Role");

            entity.Property(e => e.Idrole).HasColumnName("IDRole");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<SkillSet>(entity =>
        {
            entity.HasKey(e => e.IdskillSet).HasName("PK__SkillSet__5AA3AAA5BB570E9A");

            entity.ToTable("SkillSet");

            entity.Property(e => e.IdskillSet).HasColumnName("IDSkillSet");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.ToTable("Song");

            entity.Property(e => e.Name).HasMaxLength(256);

            entity.HasOne(d => d.Genre).WithMany(p => p.Songs)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Song_Genre");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Iduser).HasName("PK__User__EAE6D9DF9A5F202C");

            entity.ToTable("User");

            entity.HasIndex(e => e.Username, "UQ__User__536C85E450FFB563").IsUnique();

            entity.Property(e => e.Iduser).HasColumnName("IDUser");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.JoinDate).HasColumnType("date");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(256);
            entity.Property(e => e.PasswordSalt).HasMaxLength(256);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Username).HasMaxLength(75);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__RoleID__5555A4F4");
        });

        modelBuilder.Entity<UserSkillSet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserSkil__3214EC27D8E3B4F8");

            entity.ToTable("UserSkillSet");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SkillSetId).HasColumnName("SkillSetID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.SkillSet).WithMany(p => p.UserSkillSets)
                .HasForeignKey(d => d.SkillSetId)
                .HasConstraintName("FK__UserSkill__Skill__62AFA012");

            entity.HasOne(d => d.User).WithMany(p => p.UserSkillSets)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserSkill__UserI__61BB7BD9");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
