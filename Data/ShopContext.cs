using Microsoft.EntityFrameworkCore;
using ScrinTestApp.Data.Models;

namespace ScrinTestApp.Data;

public class ShopContext : DbContext
{
    public ShopContext()
    {
    }

    public ShopContext(DbContextOptions<ShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; } = null!;
    public virtual DbSet<Product> Products { get; set; } = null!;
    public virtual DbSet<Purchase> Purchases { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            optionsBuilder.UseSqlServer("Server=.;Database=InternetShop;Integrated Security=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.Date).HasMaxLength(50);

            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Users");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Description).HasMaxLength(50);

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.Property(e => e.OrderId).HasColumnName("Order_id");

            entity.Property(e => e.ProductId).HasColumnName("Product_id");

            entity.HasOne(d => d.Order)
                .WithMany(p => p.Purchases)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchases_Orders");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.Purchases)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchases_Products");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(50);

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
        });

    }
}