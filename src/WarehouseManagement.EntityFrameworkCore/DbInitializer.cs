using WarehouseManagement.Core;
using Microsoft.EntityFrameworkCore;

namespace WarehouseManagement.EntityFrameworkCore
{
    public static class DbInitializer
    {
        public static void Initialize(WarehouseDbContext context)
        {
            Console.WriteLine("Initializing database...");
            context.Database.Migrate();
            Console.WriteLine("Migrations applied.");

            if (context.Products.Any())
            {
                Console.WriteLine("Database already contains products. Skipping seeding.");
                return; // DB already has data
            }

            Console.WriteLine("Seeding data...");
            // 1. Suppliers
            var suppliers = new List<Supplier>
            {
                new Supplier { Name = "NexGen Electronics", LeadTimeDays = 3, IsActive = true },
                new Supplier { Name = "SteelCase Office Systems", LeadTimeDays = 14, IsActive = true },
                new Supplier { Name = "Global Industrial Parts", LeadTimeDays = 7, IsActive = true },
                new Supplier { Name = "Prime Paper & Supplies", LeadTimeDays = 2, IsActive = true }
            };
            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
            Console.WriteLine($"Seeded {suppliers.Count} suppliers.");

            // 2. Warehouses
            var warehouses = new List<Warehouse>
            {
                new Warehouse { WarehouseCode = "NY-DC-001", Name = "New York Distribution Center" },
                new Warehouse { WarehouseCode = "LA-WH-002", Name = "Los Angeles West Coast Hub" },
                new Warehouse { WarehouseCode = "CHI-LOG-003", Name = "Chicago Logistics Hub" },
                new Warehouse { WarehouseCode = "DAL-FUL-004", Name = "Dallas Fulfillment Center" }
            };
            context.Warehouses.AddRange(warehouses);
            context.SaveChanges();
            Console.WriteLine($"Seeded {warehouses.Count} warehouses.");

            // 3. Products
            var products = new List<Product>
            {
                // Tech (NexGen)
                new Product { SKU = "LAP-DL-7420", Name = "Dell Latitude 7420 Business Laptop", ReorderLevel = 15, SupplierId = suppliers[0].Id },
                new Product { SKU = "MON-LG-27-4K", Name = "LG 27\" UltraFine 4K Monitor", ReorderLevel = 20, SupplierId = suppliers[0].Id },
                new Product { SKU = "TAB-IP-AIR", Name = "iPad Air 5th Gen 256GB", ReorderLevel = 10, SupplierId = suppliers[0].Id },
                
                // Furniture (SteelCase)
                new Product { SKU = "CHR-ERG-V2", Name = "Ergonomic Task Chair V2", ReorderLevel = 5, SupplierId = suppliers[1].Id },
                new Product { SKU = "DSK-ST-ADJ", Name = "Electric Standing Desk 60x30", ReorderLevel = 5, SupplierId = suppliers[1].Id },
                
                // Industrial (Global Industrial)
                new Product { SKU = "IND-PLJ-3T", Name = "3-Ton Hydraulic Pallet Jack", ReorderLevel = 2, SupplierId = suppliers[2].Id },
                new Product { SKU = "IND-RACK-HD", Name = "Heavy Duty Pallet Racking Unit", ReorderLevel = 10, SupplierId = suppliers[2].Id },
                
                // Office Supplies (Prime Paper)
                new Product { SKU = "OFF-PAP-A4", Name = "Premium A4 Copy Paper (10 Reams)", ReorderLevel = 100, SupplierId = suppliers[3].Id }
            };
            context.Products.AddRange(products);
            context.SaveChanges();
            Console.WriteLine($"Seeded {products.Count} products.");

            // 4. Users
            var users = new List<User>
            {
                new User { Name = "John Smith", position = "Regional Manager", WarehouseId = warehouses[0].Id },
                new User { Name = "Maria Garcia", position = "Warehouse Lead", WarehouseId = warehouses[1].Id },
                new User { Name = "Robert Chen", position = "Inventory Specialist", WarehouseId = warehouses[2].Id }
            };
            context.Users.AddRange(users);
            context.SaveChanges();
            Console.WriteLine($"Seeded {users.Count} users.");

            // 5. Stock Tracking (Sample Stock levels across warehouses)
            var stockRecords = new List<StockTracking>
            {
                // Stock in NY
                new StockTracking { ProductId = products[0].Id, WarehouseId = warehouses[0].Id, Quantity = 45, Type = MovementType.Inbound, SupplierId = suppliers[0].Id },
                new StockTracking { ProductId = products[3].Id, WarehouseId = warehouses[0].Id, Quantity = 12, Type = MovementType.Inbound, SupplierId = suppliers[1].Id },
                
                // Stock in LA
                new StockTracking { ProductId = products[1].Id, WarehouseId = warehouses[1].Id, Quantity = 30, Type = MovementType.Inbound, SupplierId = suppliers[0].Id },
                new StockTracking { ProductId = products[5].Id, WarehouseId = warehouses[1].Id, Quantity = 4, Type = MovementType.Inbound, SupplierId = suppliers[2].Id },
                
                // Stock in CHI
                new StockTracking { ProductId = products[7].Id, WarehouseId = warehouses[2].Id, Quantity = 500, Type = MovementType.Inbound, SupplierId = suppliers[3].Id }
            };
            context.StockTracking.AddRange(stockRecords);
            context.SaveChanges();
            Console.WriteLine($"Seeded {stockRecords.Count} stock tracking records.");

            // 6. Inventory (Current state)
            var inventories = new List<Inventory>
            {
                new Inventory { ProductId = products[0].Id, WarehouseId = warehouses[0].Id, QuantityOnHand = 45, QuantityAllocated = 5 },
                new Inventory { ProductId = products[3].Id, WarehouseId = warehouses[0].Id, QuantityOnHand = 12, QuantityAllocated = 2 },
                new Inventory { ProductId = products[1].Id, WarehouseId = warehouses[1].Id, QuantityOnHand = 30, QuantityAllocated = 0 },
                new Inventory { ProductId = products[5].Id, WarehouseId = warehouses[1].Id, QuantityOnHand = 4, QuantityAllocated = 1 },
                new Inventory { ProductId = products[7].Id, WarehouseId = warehouses[2].Id, QuantityOnHand = 500, QuantityAllocated = 50 }
            };
            context.Inventories.AddRange(inventories);
            context.SaveChanges();
            Console.WriteLine($"Seeded {inventories.Count} inventory records.");
            Console.WriteLine("Database initialization complete.");
        }
    }
}
