using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WSSale.Models
{
    public partial class ActualSaleContext : DbContext
    {
        public ActualSaleContext()
        {
        }

        public ActualSaleContext(DbContextOptions<ActualSaleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Concept> Concepts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;DataBase=ActualSale;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Concept>(entity =>
            {
                entity.HasKey(e => e.IdConcept);

                entity.ToTable("Concept");

                entity.Property(e => e.IdConcept).HasColumnName("Id_Concept");

                entity.Property(e => e.AmountConcept).HasColumnName("Amount_Concept");

                entity.Property(e => e.IdProduct).HasColumnName("Id_Product");

                entity.Property(e => e.IdSale).HasColumnName("Id_Sale");

                entity.Property(e => e.TotalProductsConcept)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("Total_Products_Concept");

                entity.Property(e => e.UnitPriceConcept)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("Unit_Price_Concept");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Concepts)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Concept_Product");

                entity.HasOne(d => d.IdSaleNavigation)
                    .WithMany(p => p.Concepts)
                    .HasForeignKey(d => d.IdSale)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Concept_Sale");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.IdCustomer);

                entity.Property(e => e.IdCustomer).HasColumnName("Id_Customer");

                entity.Property(e => e.NameCustomer)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Customer")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProduct);

                entity.ToTable("Product");

                entity.Property(e => e.IdProduct).HasColumnName("Id_Product");

                entity.Property(e => e.CostProduct)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("Cost_Product");

                entity.Property(e => e.NameProduct)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Product")
                    .IsFixedLength(true);

                entity.Property(e => e.UnitPriceProduct)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("Unit_Price_Product");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.IdSale);

                entity.ToTable("Sale");

                entity.Property(e => e.IdSale).HasColumnName("Id_Sale");

                entity.Property(e => e.DateSale)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Sale");

                entity.Property(e => e.IdCustomer).HasColumnName("Id_Customer");

                entity.Property(e => e.TotalSale)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("Total_Sale");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sale_Customers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
