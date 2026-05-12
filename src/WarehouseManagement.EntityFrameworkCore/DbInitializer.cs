
using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Data;
using WarehouseManagement.Models;

namespace WarehouseManagement.EntityFrameworkCore
{
  public static class DbInitializer
  {
    public static async Task Initialize(WarehouseDbContext context)
    {
      Console.WriteLine("Initializing database...");
      await context.Database.MigrateAsync();
      Console.WriteLine("Migrations applied.");

      if (await context.Products.AnyAsync())
      {
        Console.WriteLine("Database already contains products. Skipping seeding.");
        return; // DB already has data
      }

      Console.WriteLine("Seeding data...");

      // Use a transaction to ensure data consistency
      using var transaction = await context.Database.BeginTransactionAsync();

      try
      {
        // 1. Suppliers (with IDs matching your original JSON)
        var suppliers = new List<Supplier>
                {
                    new Supplier
                    {
                        Id = Guid.Parse("316c467d-3945-44bf-b139-b452ad108bda"),
                        Name = "Prime Paper & Supplies",
                        LeadTimeDays = 2,
                        IsActive = true,
                        ContactEmail = "orders@primepaper.com",
                        ContactPhone = "(312) 555-0123"
                    },
                    new Supplier
                    {
                        Id = Guid.Parse("c886b05a-ed79-4682-88b1-276f9f573dbb"),
                        Name = "Global Industrial Parts",
                        LeadTimeDays = 7,
                        IsActive = true,
                        ContactEmail = "sales@globalindustrial.com",
                        ContactPhone = "(800) 555-0456"
                    },
                    new Supplier
                    {
                        Id = Guid.Parse("e260c87f-7d35-4bc6-bf3b-7e43103a062f"),
                        Name = "NexGen Electronics",
                        LeadTimeDays = 3,
                        IsActive = true,
                        ContactEmail = "support@nexgen.com",
                        ContactPhone = "(408) 555-0789"
                    },
                    new Supplier
                    {
                        Id = Guid.Parse("f1869c9d-8a17-4f84-a121-194b9aa4b80c"),
                        Name = "SteelCase Office Systems",
                        LeadTimeDays = 14,
                        IsActive = true,
                        ContactEmail = "orders@steelcase.com",
                        ContactPhone = "(616) 555-0987"
                    }
                };
        context.Suppliers.AddRange(suppliers);
        await context.SaveChangesAsync();
        Console.WriteLine($"✓ Seeded {suppliers.Count} suppliers");

        // 2. Warehouses (with enhanced fields)
        var warehouses = new List<Warehouse>
                {
                    new Warehouse
                    {
                        Id = Guid.Parse("93b76b44-5c36-4e0f-9936-01ef16359b5d"),
                        WarehouseCode = "NY-DC-001",
                        Name = "New York Distribution Center",
                        CapacitySquareFeet = 50000,
                        CurrentUtilizationPercent = 72.5m,
                        Timezone = "America/New_York",
                        IsActive = true
                    },
                    new Warehouse
                    {
                        Id = Guid.Parse("f07f2d6e-5176-47c8-bd34-46499b30556d"),
                        WarehouseCode = "LA-WH-002",
                        Name = "Los Angeles West Coast Hub",
                        CapacitySquareFeet = 75000,
                        CurrentUtilizationPercent = 45.2m,
                        Timezone = "America/Los_Angeles",
                        IsActive = true
                    },
                    new Warehouse
                    {
                        Id = Guid.Parse("87261fbf-f45e-4f37-bce9-0ca4108311d2"),
                        WarehouseCode = "CHI-LOG-003",
                        Name = "Chicago Logistics Hub",
                        CapacitySquareFeet = 35000,
                        CurrentUtilizationPercent = 88.3m,
                        Timezone = "America/Chicago",
                        IsActive = true
                    },
                    new Warehouse
                    {
                        Id = Guid.Parse("88ebdcc4-708c-446c-ac98-2bad67233d9f"),
                        WarehouseCode = "DAL-FUL-004",
                        Name = "Dallas Fulfillment Center",
                        CapacitySquareFeet = 45000,
                        CurrentUtilizationPercent = 34.6m,
                        Timezone = "America/Chicago",
                        IsActive = true
                    }
                };
        context.Warehouses.AddRange(warehouses);
        await context.SaveChangesAsync();
        Console.WriteLine($"✓ Seeded {warehouses.Count} warehouses");

        // 3. Products (with enhanced fields - all 8 from your JSON)
        var products = new List<Product>
                {
                    // Tech products (NexGen Electronics)
                    new Product
                    {
                        Id = Guid.Parse("dfdde47b-d9e7-4f12-9add-0881839f2e01"),
                        SKU = "LAP-DL-7420",
                        Name = "Dell Latitude 7420 Business Laptop",
                        ReorderLevel = 15,
                        SupplierId = suppliers[2].Id, // NexGen
                        UnitCost = 1249.99m,
                        WeightLbs = 3.5m,
                        Category = "Electronics",
                        Barcode = "847568123456",
                        IsActive = true
                    },
                    new Product
                    {
                        Id = Guid.Parse("8863f7cc-ef0f-4686-9d81-b217376f608e"),
                        SKU = "MON-LG-27-4K",
                        Name = "LG 27\" UltraFine 4K Monitor",
                        ReorderLevel = 20,
                        SupplierId = suppliers[2].Id, // NexGen
                        UnitCost = 399.99m,
                        WeightLbs = 12.0m,
                        Category = "Electronics",
                        Barcode = "847568123457",
                        IsActive = true
                    },
                    new Product
                    {
                        Id = Guid.Parse("5e83fa4d-5c5b-4a97-bbfc-5794a316bf25"),
                        SKU = "TAB-IP-AIR",
                        Name = "iPad Air 5th Gen 256GB",
                        ReorderLevel = 10,
                        SupplierId = suppliers[2].Id, // NexGen
                        UnitCost = 749.99m,
                        WeightLbs = 1.0m,
                        Category = "Electronics",
                        Barcode = "847568123458",
                        IsActive = true
                    },
                    
                    // Furniture products (SteelCase)
                    new Product
                    {
                        Id = Guid.Parse("652bafb1-dd00-4525-9b18-7bc138cf4e19"),
                        SKU = "CHR-ERG-V2",
                        Name = "Ergonomic Task Chair V2",
                        ReorderLevel = 5,
                        SupplierId = suppliers[3].Id, // SteelCase
                        UnitCost = 425.00m,
                        WeightLbs = 45.0m,
                        Category = "Furniture",
                        Barcode = "847568123459",
                        IsActive = true
                    },
                    new Product
                    {
                        Id = Guid.Parse("bbbdd36f-811e-4053-9de1-b59fa8bfca39"),
                        SKU = "DSK-ST-ADJ",
                        Name = "Electric Standing Desk 60x30",
                        ReorderLevel = 5,
                        SupplierId = suppliers[3].Id, // SteelCase
                        UnitCost = 675.00m,
                        WeightLbs = 85.0m,
                        Category = "Furniture",
                        Barcode = "847568123460",
                        IsActive = true
                    },
                    
                    // Industrial products (Global Industrial)
                    new Product
                    {
                        Id = Guid.Parse("71e30164-cd71-47c0-b3d0-9e55b46a0a5f"),
                        SKU = "IND-PLJ-3T",
                        Name = "3-Ton Hydraulic Pallet Jack",
                        ReorderLevel = 2,
                        SupplierId = suppliers[1].Id, // Global Industrial
                        UnitCost = 895.00m,
                        WeightLbs = 175.0m,
                        Category = "Industrial Equipment",
                        Barcode = "847568123461",
                        IsActive = true
                    },
                    new Product
                    {
                        Id = Guid.Parse("db3a77f4-6571-4459-943f-537a142e0242"),
                        SKU = "IND-RACK-HD",
                        Name = "Heavy Duty Pallet Racking Unit",
                        ReorderLevel = 10,
                        SupplierId = suppliers[1].Id, // Global Industrial
                        UnitCost = 1250.00m,
                        WeightLbs = 350.0m,
                        Category = "Industrial Equipment",
                        Barcode = "847568123462",
                        IsActive = true
                    },
                    
                    // Office Supplies (Prime Paper)
                    new Product
                    {
                        Id = Guid.Parse("df1da8d1-8c53-488a-88eb-db24a4ac66bf"),
                        SKU = "OFF-PAP-A4",
                        Name = "Premium A4 Copy Paper (10 Reams)",
                        ReorderLevel = 100,
                        SupplierId = suppliers[0].Id, // Prime Paper
                        UnitCost = 45.99m,
                        WeightLbs = 27.5m,
                        Category = "Office Supplies",
                        Barcode = "847568123463",
                        IsActive = true
                    }
                };
        context.Products.AddRange(products);
        await context.SaveChangesAsync();
        Console.WriteLine($"✓ Seeded {products.Count} products");

        // 4. Users (with all 3 from your JSON)
        var users = new List<User>
                {
                    new User
                    {
                        Id = Guid.Parse("393e4fa6-84a0-4ea2-990a-d2ab42c641ca"),
                        Name = "John Smith",
                        Position = "Regional Manager",
                        WarehouseId = warehouses[0].Id, // New York
                        Email = "john.smith@warehouse.com",
                        IsActive = true
                    },
                    new User
                    {
                        Id = Guid.Parse("799cd594-ac79-4436-abf3-2a1a57d76860"),
                        Name = "Maria Garcia",
                        Position = "Warehouse Lead",
                        WarehouseId = warehouses[1].Id, // Los Angeles
                        Email = "maria.garcia@warehouse.com",
                        IsActive = true
                    },
                    new User
                    {
                        Id = Guid.Parse("2f408d44-a9e1-4cd8-9c16-0f115bd78e60"),
                        Name = "Robert Chen",
                        Position = "Inventory Specialist",
                        WarehouseId = warehouses[2].Id, // Chicago
                        Email = "robert.chen@warehouse.com",
                        IsActive = true
                    }
                };
        context.Users.AddRange(users);
        await context.SaveChangesAsync();
        Console.WriteLine($"✓ Seeded {users.Count} users");

        // 5. Inventory (Current state from your JSON)
        var inventories = new List<Inventory>
                {
                    new Inventory
                    {
                        Id = Guid.Parse("2d16947b-d100-4847-bc9e-127be8dc53c8"),
                        ProductId = products[3].Id, // Ergonomic Task Chair
                        WarehouseId = warehouses[0].Id, // New York
                        QuantityOnHand = 12,
                        QuantityAllocated = 2,
                        MinSafetyStock = 3,
                        LastCounted = DateTime.UtcNow.AddDays(-5),
                        IsActive = true
                    },
                    new Inventory
                    {
                        Id = Guid.Parse("663a1e5c-c0b5-46d6-8f29-461be63e82bf"),
                        ProductId = products[1].Id, // LG Monitor
                        WarehouseId = warehouses[1].Id, // Los Angeles
                        QuantityOnHand = 30,
                        QuantityAllocated = 0,
                        MinSafetyStock = 5,
                        LastCounted = DateTime.UtcNow.AddDays(-2),
                        IsActive = true
                    },
                    new Inventory
                    {
                        Id = Guid.Parse("8e46d376-2ebf-4036-803c-98b280aaf6e0"),
                        ProductId = products[5].Id, // Pallet Jack
                        WarehouseId = warehouses[1].Id, // Los Angeles
                        QuantityOnHand = 4,
                        QuantityAllocated = 1,
                        MinSafetyStock = 1,
                        LastCounted = DateTime.UtcNow.AddDays(-7),
                        IsActive = true
                    },
                    new Inventory
                    {
                        Id = Guid.Parse("a280cc60-ddb6-41d4-8c77-e7236623e535"),
                        ProductId = products[7].Id, // Copy Paper
                        WarehouseId = warehouses[2].Id, // Chicago
                        QuantityOnHand = 500,
                        QuantityAllocated = 50,
                        MinSafetyStock = 25,
                        LastCounted = DateTime.UtcNow.AddDays(-1),
                        IsActive = true
                    },
                    new Inventory
                    {
                        Id = Guid.Parse("c43b9393-1a2f-4b43-aa3a-7f60e2650e73"),
                        ProductId = products[0].Id, // Dell Laptop
                        WarehouseId = warehouses[0].Id, // New York
                        QuantityOnHand = 45,
                        QuantityAllocated = 5,
                        MinSafetyStock = 10,
                        LastCounted = DateTime.UtcNow.AddDays(-3),
                        IsActive = true
                    },
                    // Additional inventory to cover all warehouses
                    new Inventory
                    {
                        ProductId = products[2].Id, // iPad
                        WarehouseId = warehouses[0].Id, // New York
                        QuantityOnHand = 8,
                        QuantityAllocated = 2,
                        MinSafetyStock = 3,
                        IsActive = true
                    },
                    new Inventory
                    {
                        ProductId = products[4].Id, // Standing Desk
                        WarehouseId = warehouses[1].Id, // Los Angeles
                        QuantityOnHand = 15,
                        QuantityAllocated = 3,
                        MinSafetyStock = 2,
                        IsActive = true
                    }
                };
        context.Inventories.AddRange(inventories);
        await context.SaveChangesAsync();
        Console.WriteLine($"✓ Seeded {inventories.Count} inventory records");

        // 6. Stock Tracking (Historical records for audit trail)
        var stockTrackings = new List<StockTracking>
                {
                    // Initial stock - NY Warehouse
                    new StockTracking
                    {
                        Id = Guid.Parse("fc99ba58-91b7-4b78-8969-580c104c590c"),
                        ProductId = products[3].Id, // Ergonomic Chair
                        WarehouseId = warehouses[0].Id,
                        Quantity = 12,
                        Type = StockTrackingType.Receipt,
                        SupplierId = suppliers[3].Id,
                        UserPerformedBy = users[0].Id,
                        Notes = "Initial stock receipt",
                        CreatedAt = DateTime.UtcNow.AddDays(-30)
                    },
                    new StockTracking
                    {
                        Id = Guid.Parse("2b911fcb-01fb-405c-b0cb-3834ef54bd13"),
                        ProductId = products[0].Id, // Dell Laptop
                        WarehouseId = warehouses[0].Id,
                        Quantity = 45,
                        Type = StockTrackingType.Receipt,
                        SupplierId = suppliers[2].Id,
                        UserPerformedBy = users[0].Id,
                        Notes = "Initial stock receipt",
                        CreatedAt = DateTime.UtcNow.AddDays(-30)
                    },
                    
                    // Initial stock - LA Warehouse
                    new StockTracking
                    {
                        Id = Guid.Parse("bdf07f45-812c-4386-93e7-0ed84588bf10"),
                        ProductId = products[1].Id, // LG Monitor
                        WarehouseId = warehouses[1].Id,
                        Quantity = 30,
                        Type = StockTrackingType.Receipt,
                        SupplierId = suppliers[2].Id,
                        UserPerformedBy = users[1].Id,
                        Notes = "Initial stock receipt",
                        CreatedAt = DateTime.UtcNow.AddDays(-30)
                    },
                    new StockTracking
                    {
                        Id = Guid.Parse("ddbb2a1e-dd3a-4583-8f77-fbe45c3f954f"),
                        ProductId = products[5].Id, // Pallet Jack
                        WarehouseId = warehouses[1].Id,
                        Quantity = 4,
                        Type = StockTrackingType.Receipt,
                        SupplierId = suppliers[1].Id,
                        UserPerformedBy = users[1].Id,
                        Notes = "Initial stock receipt",
                        CreatedAt = DateTime.UtcNow.AddDays(-30)
                    },
                    
                    // Initial stock - CHI Warehouse
                    new StockTracking
                    {
                        Id = Guid.Parse("87ee19f0-a705-40d0-842e-345957048b99"),
                        ProductId = products[7].Id, // Copy Paper
                        WarehouseId = warehouses[2].Id,
                        Quantity = 500,
                        Type = StockTrackingType.Receipt,
                        SupplierId = suppliers[0].Id,
                        UserPerformedBy = users[2].Id,
                        Notes = "Initial stock receipt",
                        CreatedAt = DateTime.UtcNow.AddDays(-30)
                    },
                    
                    // Recent activities (last 30 days)
                    new StockTracking
                    {
                        ProductId = products[0].Id, // Dell Laptop
                        WarehouseId = warehouses[0].Id,
                        Quantity = 2,
                        Type = StockTrackingType.Sale,
                        SupplierId = suppliers[2].Id,
                        UserPerformedBy = users[0].Id,
                        ReferenceId = "SO-2026-001",
                        Notes = "Customer order shipment",
                        CreatedAt = DateTime.UtcNow.AddDays(-15)
                    },
                    new StockTracking
                    {
                        ProductId = products[7].Id, // Copy Paper
                        WarehouseId = warehouses[2].Id,
                        Quantity = 25,
                        Type = StockTrackingType.Sale,
                        SupplierId = suppliers[0].Id,
                        UserPerformedBy = users[2].Id,
                        ReferenceId = "SO-2026-002",
                        Notes = "Bulk order to office supply store",
                        CreatedAt = DateTime.UtcNow.AddDays(-10)
                    },
                    new StockTracking
                    {
                        ProductId = products[1].Id, // LG Monitor
                        WarehouseId = warehouses[1].Id,
                        Quantity = 5,
                        Type = StockTrackingType.Adjustment,
                        SupplierId = suppliers[2].Id,
                        UserPerformedBy = users[1].Id,
                        Notes = "Cycle count adjustment - damaged units written off",
                        CreatedAt = DateTime.UtcNow.AddDays(-5)
                    }
                };
        context.StockTrackings.AddRange(stockTrackings);
        await context.SaveChangesAsync();
        Console.WriteLine($"✓ Seeded {stockTrackings.Count} stock tracking records");

        // 7. Purchase Orders (for open orders)
        var purchaseOrders = new List<PurchaseOrder>
                {
                    new PurchaseOrder
                    {
                        PONumber = "PO-20260501-001",
                        SupplierId = suppliers[2].Id, // NexGen
                        WarehouseId = warehouses[0].Id,
                        OrderDate = DateTime.UtcNow.AddDays(-10),
                        ExpectedDeliveryDate = DateTime.UtcNow.AddDays(4),
                        Status = PurchaseOrderStatus.Shipped,
                        TotalAmount = 12499.90m
                    },
                    new PurchaseOrder
                    {
                        PONumber = "PO-20260505-002",
                        SupplierId = suppliers[0].Id, // Prime Paper
                        WarehouseId = warehouses[2].Id,
                        OrderDate = DateTime.UtcNow.AddDays(-6),
                        ExpectedDeliveryDate = DateTime.UtcNow.AddDays(-4),
                        Status = PurchaseOrderStatus.Received,
                        TotalAmount = 4599.00m
                    }
                };
        context.PurchaseOrders.AddRange(purchaseOrders);
        await context.SaveChangesAsync();

        // Purchase Order Lines
        var purchaseOrderLines = new List<PurchaseOrderLine>
                {
                    new PurchaseOrderLine
                    {
                        PurchaseOrderId = purchaseOrders[0].Id,
                        ProductId = products[0].Id, // Dell Laptop
                        QuantityOrdered = 10,
                        QuantityReceived = 0,
                        UnitPrice = 1249.99m
                    },
                    new PurchaseOrderLine
                    {
                        PurchaseOrderId = purchaseOrders[1].Id,
                        ProductId = products[7].Id, // Copy Paper
                        QuantityOrdered = 100,
                        QuantityReceived = 100,
                        UnitPrice = 45.99m
                    }
                };
        context.PurchaseOrderLines.AddRange(purchaseOrderLines);
        await context.SaveChangesAsync();
        Console.WriteLine($"✓ Seeded {purchaseOrders.Count} purchase orders");

        // 8. Transfers (in-transit inventory)
        var transfers = new List<Transfer>
                {
                    new Transfer
                    {
                        TransferNumber = "TRF-20260510-001",
                        FromWarehouseId = warehouses[0].Id, // NY to LA
                        ToWarehouseId = warehouses[1].Id,
                        ProductId = products[3].Id, // Ergonomic Chair
                        Quantity = 5,
                        Status = TransferStatus.InTransit,
                        ShippedDate = DateTime.UtcNow.AddDays(-3)
                    },
                    new Transfer
                    {
                        TransferNumber = "TRF-20260511-002",
                        FromWarehouseId = warehouses[2].Id, // CHI to DAL
                        ToWarehouseId = warehouses[3].Id,
                        ProductId = products[7].Id, // Copy Paper
                        Quantity = 50,
                        Status = TransferStatus.Pending
                    }
                };
        context.Transfers.AddRange(transfers);
        await context.SaveChangesAsync();
        Console.WriteLine($"✓ Seeded {transfers.Count} transfers");

        // Commit the transaction
        await transaction.CommitAsync();
        Console.WriteLine("✅ Database initialization completed successfully!");
        Console.WriteLine($"\nSummary:");
        Console.WriteLine($"  └─ Suppliers: {suppliers.Count}");
        Console.WriteLine($"  └─ Warehouses: {warehouses.Count}");
        Console.WriteLine($"  └─ Products: {products.Count}");
        Console.WriteLine($"  └─ Users: {users.Count}");
        Console.WriteLine($"  └─ Inventory Records: {inventories.Count}");
        Console.WriteLine($"  └─ Stock Transactions: {stockTrackings.Count}");
        Console.WriteLine($"  └─ Purchase Orders: {purchaseOrders.Count}");
        Console.WriteLine($"  └─ Transfers: {transfers.Count}");
      }
      catch (Exception ex)
      {
        await transaction.RollbackAsync();
        Console.WriteLine($"❌ Error seeding database: {ex.Message}");
        throw;
      }
    }
  }
}
// using WarehouseManagement.Core;
// using Microsoft.EntityFrameworkCore;
//
// namespace WarehouseManagement.EntityFrameworkCore
// {
//     public static class DbInitializer
//     {
//         public static void Initialize(WarehouseDbContext context)
//         {
//             Console.WriteLine("Initializing database...");
//             context.Database.Migrate();
//             Console.WriteLine("Migrations applied.");
//
//             if (context.Products.Any())
//             {
//                 Console.WriteLine("Database already contains products. Skipping seeding.");
//                 return; // DB already has data
//             }
//
//             Console.WriteLine("Seeding data...");
//             // 1. Suppliers
//             var suppliers = new List<Supplier>
//             {
//                 new Supplier { Name = "NexGen Electronics", LeadTimeDays = 3, IsActive = true },
//                 new Supplier { Name = "SteelCase Office Systems", LeadTimeDays = 14, IsActive = true },
//                 new Supplier { Name = "Global Industrial Parts", LeadTimeDays = 7, IsActive = true },
//                 new Supplier { Name = "Prime Paper & Supplies", LeadTimeDays = 2, IsActive = true }
//             };
//             context.Suppliers.AddRange(suppliers);
//             context.SaveChanges();
//             Console.WriteLine($"Seeded {suppliers.Count} suppliers.");
//
//             // 2. Warehouses
//             var warehouses = new List<Warehouse>
//             {
//                 new Warehouse { WarehouseCode = "NY-DC-001", Name = "New York Distribution Center" },
//                 new Warehouse { WarehouseCode = "LA-WH-002", Name = "Los Angeles West Coast Hub" },
//                 new Warehouse { WarehouseCode = "CHI-LOG-003", Name = "Chicago Logistics Hub" },
//                 new Warehouse { WarehouseCode = "DAL-FUL-004", Name = "Dallas Fulfillment Center" }
//             };
//             context.Warehouses.AddRange(warehouses);
//             context.SaveChanges();
//             Console.WriteLine($"Seeded {warehouses.Count} warehouses.");
//
//             // 3. Products
//             var products = new List<Product>
//             {
//                 // Tech (NexGen)
//                 new Product { SKU = "LAP-DL-7420", Name = "Dell Latitude 7420 Business Laptop", ReorderLevel = 15, SupplierId = suppliers[0].Id },
//                 new Product { SKU = "MON-LG-27-4K", Name = "LG 27\" UltraFine 4K Monitor", ReorderLevel = 20, SupplierId = suppliers[0].Id },
//                 new Product { SKU = "TAB-IP-AIR", Name = "iPad Air 5th Gen 256GB", ReorderLevel = 10, SupplierId = suppliers[0].Id },
//
//                 // Furniture (SteelCase)
//                 new Product { SKU = "CHR-ERG-V2", Name = "Ergonomic Task Chair V2", ReorderLevel = 5, SupplierId = suppliers[1].Id },
//                 new Product { SKU = "DSK-ST-ADJ", Name = "Electric Standing Desk 60x30", ReorderLevel = 5, SupplierId = suppliers[1].Id },
//
//                 // Industrial (Global Industrial)
//                 new Product { SKU = "IND-PLJ-3T", Name = "3-Ton Hydraulic Pallet Jack", ReorderLevel = 2, SupplierId = suppliers[2].Id },
//                 new Product { SKU = "IND-RACK-HD", Name = "Heavy Duty Pallet Racking Unit", ReorderLevel = 10, SupplierId = suppliers[2].Id },
//
//                 // Office Supplies (Prime Paper)
//                 new Product { SKU = "OFF-PAP-A4", Name = "Premium A4 Copy Paper (10 Reams)", ReorderLevel = 100, SupplierId = suppliers[3].Id }
//             };
//             context.Products.AddRange(products);
//             context.SaveChanges();
//             Console.WriteLine($"Seeded {products.Count} products.");
//
//             // 4. Users
//             var users = new List<User>
//             {
//                 new User { Name = "John Smith", position = "Regional Manager", WarehouseId = warehouses[0].Id },
//                 new User { Name = "Maria Garcia", position = "Warehouse Lead", WarehouseId = warehouses[1].Id },
//                 new User { Name = "Robert Chen", position = "Inventory Specialist", WarehouseId = warehouses[2].Id }
//             };
//             context.Users.AddRange(users);
//             context.SaveChanges();
//             Console.WriteLine($"Seeded {users.Count} users.");
//
//             // 5. Stock Tracking (Sample Stock levels across warehouses)
//             var stockRecords = new List<StockTracking>
//             {
//                 // Stock in NY
//                 new StockTracking { ProductId = products[0].Id, WarehouseId = warehouses[0].Id, Quantity = 45, Type = MovementType.Inbound, SupplierId = suppliers[0].Id },
//                 new StockTracking { ProductId = products[3].Id, WarehouseId = warehouses[0].Id, Quantity = 12, Type = MovementType.Inbound, SupplierId = suppliers[1].Id },
//
//                 // Stock in LA
//                 new StockTracking { ProductId = products[1].Id, WarehouseId = warehouses[1].Id, Quantity = 30, Type = MovementType.Inbound, SupplierId = suppliers[0].Id },
//                 new StockTracking { ProductId = products[5].Id, WarehouseId = warehouses[1].Id, Quantity = 4, Type = MovementType.Inbound, SupplierId = suppliers[2].Id },
//
//                 // Stock in CHI
//                 new StockTracking { ProductId = products[7].Id, WarehouseId = warehouses[2].Id, Quantity = 500, Type = MovementType.Inbound, SupplierId = suppliers[3].Id }
//             };
//             context.StockTracking.AddRange(stockRecords);
//             context.SaveChanges();
//             Console.WriteLine($"Seeded {stockRecords.Count} stock tracking records.");
//
//             // 6. Inventory (Current state)
//             var inventories = new List<Inventory>
//             {
//                 new Inventory { ProductId = products[0].Id, WarehouseId = warehouses[0].Id, QuantityOnHand = 45, QuantityAllocated = 5 },
//                 new Inventory { ProductId = products[3].Id, WarehouseId = warehouses[0].Id, QuantityOnHand = 12, QuantityAllocated = 2 },
//                 new Inventory { ProductId = products[1].Id, WarehouseId = warehouses[1].Id, QuantityOnHand = 30, QuantityAllocated = 0 },
//                 new Inventory { ProductId = products[5].Id, WarehouseId = warehouses[1].Id, QuantityOnHand = 4, QuantityAllocated = 1 },
//                 new Inventory { ProductId = products[7].Id, WarehouseId = warehouses[2].Id, QuantityOnHand = 500, QuantityAllocated = 50 }
//             };
//             context.Inventories.AddRange(inventories);
//             context.SaveChanges();
//             Console.WriteLine($"Seeded {inventories.Count} inventory records.");
//             Console.WriteLine("Database initialization complete.");
//         }
//     }
// }
