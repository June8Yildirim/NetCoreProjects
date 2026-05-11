using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Core;

namespace WarehouseManagement.EntityFrameworkCore
{
  public class WarehouseDbContext : DbContext
  {
    public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options)
    {
    }

    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<StockTracking> StockTracking { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Warehouse>(entity =>
      {
        entity.Property(e => e.Name).HasMaxLength(200);
      });

      // Add any other specific configurations here if needed
    }
  }
}
