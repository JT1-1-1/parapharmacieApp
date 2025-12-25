using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ParapharmacieApp.Models;

public partial class ParapharmacieDbContext : DbContext
{
    public ParapharmacieDbContext()
    {
    }

    public ParapharmacieDbContext(DbContextOptions<ParapharmacieDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<CommandeFournisseur> CommandeFournisseurs { get; set; }

    public virtual DbSet<Fournisseur> Fournisseurs { get; set; }

    public virtual DbSet<LigneCommande> LigneCommandes { get; set; }

    public virtual DbSet<LigneVente> LigneVentes { get; set; }

    public virtual DbSet<Produit> Produits { get; set; }

    public virtual DbSet<Vente> Ventes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=ParapharmacieDB;Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("PK__Client__C1961B338EECDFF8");

            entity.ToTable("Client");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.PointsFidelite).HasDefaultValue(0);
            entity.Property(e => e.Prenom).HasMaxLength(50);
            entity.Property(e => e.Telephone).HasMaxLength(20);
        });

        modelBuilder.Entity<CommandeFournisseur>(entity =>
        {
            entity.HasKey(e => e.IdCommande).HasName("PK__Commande__6828586C930388A9");

            entity.ToTable("CommandeFournisseur");

            entity.Property(e => e.DateCommande).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Statut).HasMaxLength(30);

            entity.HasOne(d => d.IdFournisseurNavigation).WithMany(p => p.CommandeFournisseurs)
                .HasForeignKey(d => d.IdFournisseur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Commande_Fournisseur");
        });

        modelBuilder.Entity<Fournisseur>(entity =>
        {
            entity.HasKey(e => e.IdFournisseur).HasName("PK__Fourniss__A63C9FB981C5CAE0");

            entity.ToTable("Fournisseur");

            entity.Property(e => e.Adresse).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nom).HasMaxLength(100);
            entity.Property(e => e.Telephone).HasMaxLength(20);
        });

        modelBuilder.Entity<LigneCommande>(entity =>
        {
            entity.HasKey(e => e.IdLigneCommande).HasName("PK__LigneCom__42B960A108DCF714");

            entity.ToTable("LigneCommande");

            entity.HasOne(d => d.IdCommandeNavigation).WithMany(p => p.LigneCommandes)
                .HasForeignKey(d => d.IdCommande)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LigneCommande_Commande");

            entity.HasOne(d => d.IdProduitNavigation).WithMany(p => p.LigneCommandes)
                .HasForeignKey(d => d.IdProduit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LigneCommande_Produit");
        });

        modelBuilder.Entity<LigneVente>(entity =>
        {
            entity.HasKey(e => e.IdLigneVente).HasName("PK__LigneVen__3D1EFB0318D1D05C");

            entity.ToTable("LigneVente");

            entity.Property(e => e.PrixUnitaire).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdProduitNavigation).WithMany(p => p.LigneVentes)
                .HasForeignKey(d => d.IdProduit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LigneVente_Produit");

            entity.HasOne(d => d.IdVenteNavigation).WithMany(p => p.LigneVentes)
                .HasForeignKey(d => d.IdVente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LigneVente_Vente");
        });

        modelBuilder.Entity<Produit>(entity =>
        {
            entity.HasKey(e => e.IdProduit).HasName("PK__Produit__2E8997F0A97159C2");

            entity.ToTable("Produit");

            entity.HasIndex(e => e.Reference, "UQ__Produit__062B9EB86B884CA9").IsUnique();

            entity.Property(e => e.Categorie).HasMaxLength(50);
            entity.Property(e => e.Nom).HasMaxLength(100);
            entity.Property(e => e.PrixAchat).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PrixVente).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Reference).HasMaxLength(50);
            entity.Property(e => e.SeuilAlerte).HasDefaultValue(5);
        });

        modelBuilder.Entity<Vente>(entity =>
        {
            entity.HasKey(e => e.IdVente).HasName("PK__Vente__BC1240B10347C43B");

            entity.ToTable("Vente");

            entity.Property(e => e.DateVente)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Remise)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Ventes)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK_Vente_Client");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
