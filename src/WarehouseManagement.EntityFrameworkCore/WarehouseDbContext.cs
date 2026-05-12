// using Microsoft.EntityFrameworkCore;
// using WarehouseManagement.Core;
//
// namespace WarehouseManagement.EntityFrameworkCore
// {
//   public class WarehouseDbContext : DbContext
//   {
//     public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options)
//     {
//     }
//
//     public DbSet<Permission> Permissions { get; set; }
//     public DbSet<Product> Products { get; set; }
//     public DbSet<Inventory> Inventories { get; set; }
//     public DbSet<StockTracking> StockTracking { get; set; }
//     public DbSet<Supplier> Suppliers { get; set; }
//     public DbSet<Warehouse> Warehouses { get; set; }
//     public DbSet<User> Users { get; set; }
//
//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//       base.OnModelCreating(modelBuilder);
//
//       modelBuilder.Entity<Warehouse>(entity =>
//       {
//         entity.Property(e => e.Name).HasMaxLength(200);
//       });
//
//       // Add any other specific configurations here if needed
//     }
//   }
// }
using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Models;

namespace WarehouseManagement.Data
{
  public class WarehouseDbContext : DbContext
  {
    public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options)
        : base(options)
    {
    }

    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<StockTracking> StockTrackings { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    public DbSet<PurchaseOrderLine> PurchaseOrderLines { get; set; }
    public DbSet<Transfer> Transfers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Inventory - Composite unique index
      modelBuilder.Entity<Inventory>()
          .HasIndex(i => new { i.ProductId, i.WarehouseId })
          .IsUnique();

      // Inventory - Check constraints
      modelBuilder.Entity<Inventory>()
          .ToTable(t => t.HasCheckConstraint("CK_Inventory_NonNegative",
              "QuantityOnHand >= 0 AND QuantityAllocated >= 0"));

      modelBuilder.Entity<Inventory>()
          .HasCheckConstraint("CK_Inventory_Allocation",
              "QuantityAllocated <= QuantityOnHand");

      // Product - Index on SKU
      modelBuilder.Entity<Product>()
          .HasIndex(p => p.SKU)
          .IsUnique();

      // Purchase Order - Unique PO number
      modelBuilder.Entity<PurchaseOrder>()
          .HasIndex(po => po.PONumber)
          .IsUnique();

      // Transfer - Unique transfer number
      modelBuilder.Entity<Transfer>()
          .HasIndex(t => t.TransferNumber)
          .IsUnique();

      modelBuilder.Entity<Transfer>()
          .HasOne(t => t.FromWarehouse)
          .WithMany(w => w.FromTransfers)
          .HasForeignKey(t => t.FromWarehouseId)
          .OnDelete(DeleteBehavior.Restrict);

      modelBuilder.Entity<Transfer>()
          .HasOne(t => t.ToWarehouse)
          .WithMany(w => w.ToTransfers)
          .HasForeignKey(t => t.ToWarehouseId)
          .OnDelete(DeleteBehavior.Restrict);

      // Configure relationships
      modelBuilder.Entity<Inventory>()
          .HasOne(i => i.Product)
          .WithMany(p => p.Inventories)
          .HasForeignKey(i => i.ProductId)
          .OnDelete(DeleteBehavior.Restrict);

      modelBuilder.Entity<Inventory>()
          .HasOne(i => i.Warehouse)
          .WithMany(w => w.Inventories)
          .HasForeignKey(i => i.WarehouseId)
          .OnDelete(DeleteBehavior.Restrict);

      modelBuilder.Entity<StockTracking>()
          .HasOne(st => st.User)
          .WithMany(u => u.StockTrackings)
          .HasForeignKey(st => st.UserPerformedBy)
          .OnDelete(DeleteBehavior.SetNull);

      // Seed data from your JSON files
      SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
      // Seed Suppliers (from your JSON)
      var suppliers = new[]
      {
                new Supplier { Id = Guid.Parse("316c467d-3945-44bf-b139-b452ad108bda"),
                    Name = "Prime Paper & Supplies", LeadTimeDays = 2, IsActive = true },
                new Supplier { Id = Guid.Parse("c886b05a-ed79-4682-88b1-276f9f573dbb"),
                    Name = "Global Industrial Parts", LeadTimeDays = 7, IsActive = true },
                new Supplier { Id = Guid.Parse("e260c87f-7d35-4bc6-bf3b-7e43103a062f"),
                    Name = "NexGen Electronics", LeadTimeDays = 3, IsActive = true },
                new Supplier { Id = Guid.Parse("f1869c9d-8a17-4f84-a121-194b9aa4b80c"),
                    Name = "SteelCase Office Systems", LeadTimeDays = 14, IsActive = true }
            };
      modelBuilder.Entity<Supplier>().HasData(suppliers);

      // Seed Warehouses (from your JSON)
      var warehouses = new[]
      {
                new Warehouse { Id = Guid.Parse("87261fbf-f45e-4f37-bce9-0ca4108311d2"),
                    WarehouseCode = "CHI-LOG-003", Name = "Chicago Logistics Hub", IsActive = true },
                new Warehouse { Id = Guid.Parse("88ebdcc4-708c-446c-ac98-2bad67233d9f"),
                    WarehouseCode = "DAL-FUL-004", Name = "Dallas Fulfillment Center", IsActive = true },
                new Warehouse { Id = Guid.Parse("93b76b44-5c36-4e0f-9936-01ef16359b5d"),
                    WarehouseCode = "NY-DC-001", Name = "New York Distribution Center", IsActive = true },
                new Warehouse { Id = Guid.Parse("f07f2d6e-5176-47c8-bd34-46499b30556d"),
                    WarehouseCode = "LA-WH-002", Name = "Los Angeles West Coast Hub", IsActive = true }
            };
      modelBuilder.Entity<Warehouse>().HasData(warehouses);

      // Seed Products (from your JSON)
      var products = new[]
      {
                new Product { Id = Guid.Parse("5e83fa4d-5c5b-4a97-bbfc-5794a316bf25"),
                    SKU = "TAB-IP-AIR", Name = "iPad Air 5th Gen 256GB",
                    ReorderLevel = 10, SupplierId = Guid.Parse("e260c87f-7d35-4bc6-bf3b-7e43103a062f") },
                new Product { Id = Guid.Parse("652bafb1-dd00-4525-9b18-7bc138cf4e19"),
                    SKU = "CHR-ERG-V2", Name = "Ergonomic Task Chair V2",
                    ReorderLevel = 5, SupplierId = Guid.Parse("f1869c9d-8a17-4f84-a121-194b9aa4b80c") },
                new Product { Id = Guid.Parse("71e30164-cd71-47c0-b3d0-9e55b46a0a5f"),
                    SKU = "IND-PLJ-3T", Name = "3-Ton Hydraulic Pallet Jack",
                    ReorderLevel = 2, SupplierId = Guid.Parse("c886b05a-ed79-4682-88b1-276f9f573dbb") },
                new Product { Id = Guid.Parse("8863f7cc-ef0f-4686-9d81-b217376f608e"),
                    SKU = "MON-LG-27-4K", Name = "LG 27\" UltraFine 4K Monitor",
                    ReorderLevel = 20, SupplierId = Guid.Parse("e260c87f-7d35-4bc6-bf3b-7e43103a062f") },
                new Product { Id = Guid.Parse("bbbdd36f-811e-4053-9de1-b59fa8bfca39"),
                    SKU = "DSK-ST-ADJ", Name = "Electric Standing Desk 60x30",
                    ReorderLevel = 5, SupplierId = Guid.Parse("f1869c9d-8a17-4f84-a121-194b9aa4b80c") },
                new Product { Id = Guid.Parse("db3a77f4-6571-4459-943f-537a142e0242"),
                    SKU = "IND-RACK-HD", Name = "Heavy Duty Pallet Racking Unit",
                    ReorderLevel = 10, SupplierId = Guid.Parse("c886b05a-ed79-4682-88b1-276f9f573dbb") },
                new Product { Id = Guid.Parse("df1da8d1-8c53-488a-88eb-db24a4ac66bf"),
                    SKU = "OFF-PAP-A4", Name = "Premium A4 Copy Paper (10 Reams)",
                    ReorderLevel = 100, SupplierId = Guid.Parse("316c467d-3945-44bf-b139-b452ad108bda") },
                new Product { Id = Guid.Parse("dfdde47b-d9e7-4f12-9add-0881839f2e01"),
                    SKU = "LAP-DL-7420", Name = "Dell Latitude 7420 Business Laptop",
                    ReorderLevel = 15, SupplierId = Guid.Parse("e260c87f-7d35-4bc6-bf3b-7e43103a062f") }
            };
      modelBuilder.Entity<Product>().HasData(products);

      // Seed Users (from your JSON)
      var users = new[]
      {
                new User { Id = Guid.Parse("2f408d44-a9e1-4cd8-9c16-0f115bd78e60"),
                    Name = "Robert Chen", Position = "Inventory Specialist",
                    WarehouseId = Guid.Parse("87261fbf-f45e-4f37-bce9-0ca4108311d2") },
                new User { Id = Guid.Parse("393e4fa6-84a0-4ea2-990a-d2ab42c641ca"),
                    Name = "John Smith", Position = "Regional Manager",
                    WarehouseId = Guid.Parse("93b76b44-5c36-4e0f-9936-01ef16359b5d") },
                new User { Id = Guid.Parse("799cd594-ac79-4436-abf3-2a1a57d76860"),
                    Name = "Maria Garcia", Position = "Warehouse Lead",
                    WarehouseId = Guid.Parse("f07f2d6e-5176-47c8-bd34-46499b30556d") }
            };
      modelBuilder.Entity<User>().HasData(users);

      // Seed Inventory (from your JSON)
      var inventories = new[]
      {
                new Inventory { Id = Guid.Parse("2d16947b-d100-4847-bc9e-127be8dc53c8"),
                    ProductId = Guid.Parse("652bafb1-dd00-4525-9b18-7bc138cf4e19"),
                    WarehouseId = Guid.Parse("93b76b44-5c36-4e0f-9936-01ef16359b5d"),
                    QuantityOnHand = 12, QuantityAllocated = 2 },
                new Inventory { Id = Guid.Parse("663a1e5c-c0b5-46d6-8f29-461be63e82bf"),
                    ProductId = Guid.Parse("8863f7cc-ef0f-4686-9d81-b217376f608e"),
                    WarehouseId = Guid.Parse("f07f2d6e-5176-47c8-bd34-46499b30556d"),
                    QuantityOnHand = 30, QuantityAllocated = 0 },
                new Inventory { Id = Guid.Parse("8e46d376-2ebf-4036-803c-98b280aaf6e0"),
                    ProductId = Guid.Parse("71e30164-cd71-47c0-b3d0-9e55b46a0a5f"),
                    WarehouseId = Guid.Parse("f07f2d6e-5176-47c8-bd34-46499b30556d"),
                    QuantityOnHand = 4, QuantityAllocated = 1 },
                new Inventory { Id = Guid.Parse("a280cc60-ddb6-41d4-8c77-e7236623e535"),
                    ProductId = Guid.Parse("df1da8d1-8c53-488a-88eb-db24a4ac66bf"),
                    WarehouseId = Guid.Parse("87261fbf-f45e-4f37-bce9-0ca4108311d2"),
                    QuantityOnHand = 500, QuantityAllocated = 50 },
                new Inventory { Id = Guid.Parse("c43b9393-1a2f-4b43-aa3a-7f60e2650e73"),
                    ProductId = Guid.Parse("dfdde47b-d9e7-4f12-9add-0881839f2e01"),
                    WarehouseId = Guid.Parse("93b76b44-5c36-4e0f-9936-01ef16359b5d"),
                    QuantityOnHand = 45, QuantityAllocated = 5 }
            };
      modelBuilder.Entity<Inventory>().HasData(inventories);
    }
  }
}
