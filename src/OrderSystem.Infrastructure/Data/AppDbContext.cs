using Microsoft.EntityFrameworkCore;
using OrderSystem.Application.Interfaces;
using OrderSystem.Domain.Entities;

namespace OrderSystem.Infrastructure.Data;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureProduct(modelBuilder);
        ConfigureOrder(modelBuilder);
        ConfigureOrderItem(modelBuilder);
    }

    private void ConfigureProduct(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Product>();

        entity.HasKey(p => p.Id);

        entity.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        entity.Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        entity.Property(p => p.Stock)
            .IsRequired();

        // 🔥 CONCORRÊNCIA (ESSENCIAL)
        entity.Property(p => p.RowVersion)
            .IsRowVersion();
    }

    private void ConfigureOrder(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Order>();

        entity.HasKey(o => o.Id);

        entity.Property(o => o.CreatedAt)
            .IsRequired();

        entity.Property(o => o.Total)
            .HasColumnType("decimal(18,2)");

        entity.HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey("OrderId")
            .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigureOrderItem(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<OrderItem>();

        entity.HasKey(oi => oi.Id);

        entity.Property(oi => oi.Quantity)
            .IsRequired();

        entity.Property(oi => oi.UnitPrice)
            .HasColumnType("decimal(18,2)");
    }
}