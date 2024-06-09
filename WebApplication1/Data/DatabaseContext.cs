using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<TblCentro> TblCentros { get; set; }

    public virtual DbSet<TblCentroDato> TblCentroDatos { get; set; }

    public virtual DbSet<TblCentroRedSocial> TblCentroRedSocials { get; set; }

    public virtual DbSet<TblPersona> TblPersonas { get; set; }

    public virtual DbSet<TblPersonaDato> TblPersonaDatos { get; set; }

    public virtual DbSet<TblPersonaProfesion> TblPersonaProfesions { get; set; }

    public virtual DbSet<TblPersonaRedSocial> TblPersonaRedSocials { get; set; }

    public virtual DbSet<TblProfesion> TblProfesions { get; set; }

    public virtual DbSet<TblRedSocial> TblRedSocials { get; set; }

    public virtual DbSet<TblTipoCentro> TblTipoCentros { get; set; }

    public virtual DbSet<ViewCentro> ViewCentros { get; set; }

    public virtual DbSet<ViewPersona> ViewPersonas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=srv01;Database=database;Trusted_Connection=True;User Id=sa_database;Password=Aeiou1234;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<TblCentro>(entity =>
        {
            entity.HasKey(e => e.IdCentro).HasName("PK_tblCentro");

            entity.ToTable("TblCentro");

            entity.Property(e => e.Direccion).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(50);
            entity.Property(e => e.Web).HasMaxLength(50);

            entity.HasOne(d => d.IdTipoCentroNavigation).WithMany(p => p.TblCentros)
                .HasForeignKey(d => d.IdTipoCentro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TblTipoCentro_TblCentro");
        });

        modelBuilder.Entity<TblCentroDato>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TblCentro_Dato");

            entity.ToTable("TblCentroDato");

            entity.Property(e => e.Dato).HasMaxLength(50);
            entity.Property(e => e.Texto).HasMaxLength(500);

            entity.HasOne(d => d.IdCentroNavigation).WithMany(p => p.TblCentroDatos)
                .HasForeignKey(d => d.IdCentro)
                .HasConstraintName("FK_TblCentro_Dato_TblCentro");
        });

        modelBuilder.Entity<TblCentroRedSocial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TblCentro_Red_Social");

            entity.ToTable("TblCentroRedSocial");

            entity.Property(e => e.RedSocial).HasMaxLength(50);

            entity.HasOne(d => d.IdCentroNavigation).WithMany(p => p.TblCentroRedSocials)
                .HasForeignKey(d => d.IdCentro)
                .HasConstraintName("FK_TblCentro_Red_Social_TblCentro");

            entity.HasOne(d => d.IdRedSocialNavigation).WithMany(p => p.TblCentroRedSocials)
                .HasForeignKey(d => d.IdRedSocial)
                .HasConstraintName("FK_TblCentro_Red_Social_TblRedSocial");
        });

        modelBuilder.Entity<TblPersona>(entity =>
        {
            entity.HasKey(e => e.IdPersona);

            entity.ToTable("TblPersona");

            entity.Property(e => e.Apellidos).HasMaxLength(50);
            entity.Property(e => e.Direccion).HasMaxLength(50);
            entity.Property(e => e.Nombres).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(50);
        });

        modelBuilder.Entity<TblPersonaDato>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TblPersona_Dato");

            entity.ToTable("TblPersonaDato");

            entity.Property(e => e.Dato).HasMaxLength(50);
            entity.Property(e => e.Texto).HasMaxLength(500);

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.TblPersonaDatos)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("FK_TblPersona_Dato_TblPersona");
        });

        modelBuilder.Entity<TblPersonaProfesion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TblPersona_Profesion");

            entity.ToTable("TblPersonaProfesion");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.TblPersonaProfesions)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("FK_TblPersona_Profesion_TblPersona");

            entity.HasOne(d => d.IdProfesionNavigation).WithMany(p => p.TblPersonaProfesions)
                .HasForeignKey(d => d.IdProfesion)
                .HasConstraintName("FK_TblPersona_Profesion_TblProfesion");
        });

        modelBuilder.Entity<TblPersonaRedSocial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TblPersona_Red_Social");

            entity.ToTable("TblPersonaRedSocial");

            entity.Property(e => e.RedSocial).HasMaxLength(50);

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.TblPersonaRedSocials)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("FK_TblPersona_Red_Social_TblPersona");

            entity.HasOne(d => d.IdRedSocialNavigation).WithMany(p => p.TblPersonaRedSocials)
                .HasForeignKey(d => d.IdRedSocial)
                .HasConstraintName("FK_TblPersona_Red_Social_TblRedSocial");
        });

        modelBuilder.Entity<TblProfesion>(entity =>
        {
            entity.HasKey(e => e.IdProfesion);

            entity.ToTable("TblProfesion");

            entity.Property(e => e.Profesion).HasMaxLength(50);
        });

        modelBuilder.Entity<TblRedSocial>(entity =>
        {
            entity.HasKey(e => e.IdRedSocial);

            entity.ToTable("TblRedSocial");

            entity.Property(e => e.RedSocial).HasMaxLength(50);
        });

        modelBuilder.Entity<TblTipoCentro>(entity =>
        {
            entity.HasKey(e => e.IdTipoCentro);

            entity.ToTable("TblTipoCentro");

            entity.Property(e => e.TipoCentro).HasMaxLength(50);
        });

        modelBuilder.Entity<ViewCentro>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewCentro");

            entity.Property(e => e.Dato).HasMaxLength(50);
            entity.Property(e => e.Direccion).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.NombreRedSocial).HasMaxLength(50);
            entity.Property(e => e.RedSocial).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(50);
            entity.Property(e => e.Texto).HasMaxLength(500);
            entity.Property(e => e.TipoCentro).HasMaxLength(50);
            entity.Property(e => e.Web).HasMaxLength(50);
        });

        modelBuilder.Entity<ViewPersona>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewPersona");

            entity.Property(e => e.Apellidos).HasMaxLength(50);
            entity.Property(e => e.Direccion).HasMaxLength(50);
            entity.Property(e => e.NombreRedSocial).HasMaxLength(50);
            entity.Property(e => e.Nombres).HasMaxLength(50);
            entity.Property(e => e.Profesion).HasMaxLength(50);
            entity.Property(e => e.RedSocial).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
