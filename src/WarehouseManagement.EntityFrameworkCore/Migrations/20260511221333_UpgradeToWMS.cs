using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WarehouseManagement.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class UpgradeToWMS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    LeadTimeDays = table.Column<int>(type: "int", nullable: false),
                    ContactEmail = table.Column<string>(type: "longtext", nullable: true),
                    ContactPhone = table.Column<string>(type: "longtext", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    WarehouseCode = table.Column<string>(type: "longtext", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    CapacitySquareFeet = table.Column<int>(type: "int", nullable: true),
                    CurrentUtilizationPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Timezone = table.Column<string>(type: "longtext", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    SKU = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    ReorderLevel = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<Guid>(type: "char(36)", nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WeightLbs = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Category = table.Column<string>(type: "longtext", nullable: true),
                    Barcode = table.Column<string>(type: "longtext", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    PONumber = table.Column<string>(type: "varchar(255)", nullable: false),
                    SupplierId = table.Column<Guid>(type: "char(36)", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "char(36)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ExpectedDeliveryDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Position = table.Column<string>(type: "longtext", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    ProductId = table.Column<Guid>(type: "char(36)", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "char(36)", nullable: false),
                    QuantityOnHand = table.Column<int>(type: "int", nullable: false),
                    QuantityAllocated = table.Column<int>(type: "int", nullable: false),
                    MinSafetyStock = table.Column<int>(type: "int", nullable: true),
                    LastCounted = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                    table.CheckConstraint("CK_Inventory_Allocation", "QuantityAllocated <= QuantityOnHand");
                    table.CheckConstraint("CK_Inventory_NonNegative", "QuantityOnHand >= 0 AND QuantityAllocated >= 0");
                    table.ForeignKey(
                        name: "FK_Inventories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventories_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    TransferNumber = table.Column<string>(type: "varchar(255)", nullable: false),
                    FromWarehouseId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ToWarehouseId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ProductId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ShippedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ReceivedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfers_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transfers_Warehouses_FromWarehouseId",
                        column: x => x.FromWarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transfers_Warehouses_ToWarehouseId",
                        column: x => x.ToWarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PurchaseOrderLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    PurchaseOrderId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ProductId = table.Column<Guid>(type: "char(36)", nullable: false),
                    QuantityOrdered = table.Column<int>(type: "int", nullable: false),
                    QuantityReceived = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderLines_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderLines_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StockTrackings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    ProductId = table.Column<Guid>(type: "char(36)", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ReferenceId = table.Column<string>(type: "longtext", nullable: true),
                    UserPerformedBy = table.Column<Guid>(type: "char(36)", nullable: true),
                    Notes = table.Column<string>(type: "longtext", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTrackings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockTrackings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockTrackings_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockTrackings_Users_UserPerformedBy",
                        column: x => x.UserPerformedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_StockTrackings_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "ContactEmail", "ContactPhone", "CreatedAt", "IsActive", "LeadTimeDays", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("316c467d-3945-44bf-b139-b452ad108bda"), null, null, new DateTime(2026, 5, 11, 22, 13, 33, 261, DateTimeKind.Utc).AddTicks(8930), true, 2, "Prime Paper & Supplies", null },
                    { new Guid("c886b05a-ed79-4682-88b1-276f9f573dbb"), null, null, new DateTime(2026, 5, 11, 22, 13, 33, 261, DateTimeKind.Utc).AddTicks(9550), true, 7, "Global Industrial Parts", null },
                    { new Guid("e260c87f-7d35-4bc6-bf3b-7e43103a062f"), null, null, new DateTime(2026, 5, 11, 22, 13, 33, 261, DateTimeKind.Utc).AddTicks(9560), true, 3, "NexGen Electronics", null },
                    { new Guid("f1869c9d-8a17-4f84-a121-194b9aa4b80c"), null, null, new DateTime(2026, 5, 11, 22, 13, 33, 261, DateTimeKind.Utc).AddTicks(9560), true, 14, "SteelCase Office Systems", null }
                });

            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "CapacitySquareFeet", "CreatedAt", "CurrentUtilizationPercent", "IsActive", "Name", "Timezone", "UpdatedAt", "WarehouseCode" },
                values: new object[,]
                {
                    { new Guid("87261fbf-f45e-4f37-bce9-0ca4108311d2"), null, new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(3120), null, true, "Chicago Logistics Hub", null, null, "CHI-LOG-003" },
                    { new Guid("88ebdcc4-708c-446c-ac98-2bad67233d9f"), null, new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(3400), null, true, "Dallas Fulfillment Center", null, null, "DAL-FUL-004" },
                    { new Guid("93b76b44-5c36-4e0f-9936-01ef16359b5d"), null, new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(3410), null, true, "New York Distribution Center", null, null, "NY-DC-001" },
                    { new Guid("f07f2d6e-5176-47c8-bd34-46499b30556d"), null, new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(3410), null, true, "Los Angeles West Coast Hub", null, null, "LA-WH-002" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Barcode", "Category", "CreatedAt", "IsActive", "Name", "ReorderLevel", "SKU", "SupplierId", "UnitCost", "UpdatedAt", "WeightLbs" },
                values: new object[,]
                {
                    { new Guid("5e83fa4d-5c5b-4a97-bbfc-5794a316bf25"), null, null, new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(4100), true, "iPad Air 5th Gen 256GB", 10, "TAB-IP-AIR", new Guid("e260c87f-7d35-4bc6-bf3b-7e43103a062f"), null, null, null },
                    { new Guid("652bafb1-dd00-4525-9b18-7bc138cf4e19"), null, null, new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(4610), true, "Ergonomic Task Chair V2", 5, "CHR-ERG-V2", new Guid("f1869c9d-8a17-4f84-a121-194b9aa4b80c"), null, null, null },
                    { new Guid("71e30164-cd71-47c0-b3d0-9e55b46a0a5f"), null, null, new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(4620), true, "3-Ton Hydraulic Pallet Jack", 2, "IND-PLJ-3T", new Guid("c886b05a-ed79-4682-88b1-276f9f573dbb"), null, null, null },
                    { new Guid("8863f7cc-ef0f-4686-9d81-b217376f608e"), null, null, new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(4620), true, "LG 27\" UltraFine 4K Monitor", 20, "MON-LG-27-4K", new Guid("e260c87f-7d35-4bc6-bf3b-7e43103a062f"), null, null, null },
                    { new Guid("bbbdd36f-811e-4053-9de1-b59fa8bfca39"), null, null, new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(4630), true, "Electric Standing Desk 60x30", 5, "DSK-ST-ADJ", new Guid("f1869c9d-8a17-4f84-a121-194b9aa4b80c"), null, null, null },
                    { new Guid("db3a77f4-6571-4459-943f-537a142e0242"), null, null, new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(4630), true, "Heavy Duty Pallet Racking Unit", 10, "IND-RACK-HD", new Guid("c886b05a-ed79-4682-88b1-276f9f573dbb"), null, null, null },
                    { new Guid("df1da8d1-8c53-488a-88eb-db24a4ac66bf"), null, null, new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(4640), true, "Premium A4 Copy Paper (10 Reams)", 100, "OFF-PAP-A4", new Guid("316c467d-3945-44bf-b139-b452ad108bda"), null, null, null },
                    { new Guid("dfdde47b-d9e7-4f12-9add-0881839f2e01"), null, null, new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(4640), true, "Dell Latitude 7420 Business Laptop", 15, "LAP-DL-7420", new Guid("e260c87f-7d35-4bc6-bf3b-7e43103a062f"), null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "Name", "Position", "UpdatedAt", "WarehouseId" },
                values: new object[,]
                {
                    { new Guid("2f408d44-a9e1-4cd8-9c16-0f115bd78e60"), new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(4990), null, true, "Robert Chen", "Inventory Specialist", null, new Guid("87261fbf-f45e-4f37-bce9-0ca4108311d2") },
                    { new Guid("393e4fa6-84a0-4ea2-990a-d2ab42c641ca"), new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(5360), null, true, "John Smith", "Regional Manager", null, new Guid("93b76b44-5c36-4e0f-9936-01ef16359b5d") },
                    { new Guid("799cd594-ac79-4436-abf3-2a1a57d76860"), new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(5370), null, true, "Maria Garcia", "Warehouse Lead", null, new Guid("f07f2d6e-5176-47c8-bd34-46499b30556d") }
                });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "Id", "CreatedAt", "IsActive", "LastCounted", "MinSafetyStock", "ProductId", "QuantityAllocated", "QuantityOnHand", "UpdatedAt", "WarehouseId" },
                values: new object[,]
                {
                    { new Guid("2d16947b-d100-4847-bc9e-127be8dc53c8"), new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(5610), true, null, null, new Guid("652bafb1-dd00-4525-9b18-7bc138cf4e19"), 2, 12, null, new Guid("93b76b44-5c36-4e0f-9936-01ef16359b5d") },
                    { new Guid("663a1e5c-c0b5-46d6-8f29-461be63e82bf"), new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(6090), true, null, null, new Guid("8863f7cc-ef0f-4686-9d81-b217376f608e"), 0, 30, null, new Guid("f07f2d6e-5176-47c8-bd34-46499b30556d") },
                    { new Guid("8e46d376-2ebf-4036-803c-98b280aaf6e0"), new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(6090), true, null, null, new Guid("71e30164-cd71-47c0-b3d0-9e55b46a0a5f"), 1, 4, null, new Guid("f07f2d6e-5176-47c8-bd34-46499b30556d") },
                    { new Guid("a280cc60-ddb6-41d4-8c77-e7236623e535"), new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(6100), true, null, null, new Guid("df1da8d1-8c53-488a-88eb-db24a4ac66bf"), 50, 500, null, new Guid("87261fbf-f45e-4f37-bce9-0ca4108311d2") },
                    { new Guid("c43b9393-1a2f-4b43-aa3a-7f60e2650e73"), new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(6100), true, null, null, new Guid("dfdde47b-d9e7-4f12-9add-0881839f2e01"), 5, 45, null, new Guid("93b76b44-5c36-4e0f-9936-01ef16359b5d") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_ProductId_WarehouseId",
                table: "Inventories",
                columns: new[] { "ProductId", "WarehouseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_WarehouseId",
                table: "Inventories",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SKU",
                table: "Products",
                column: "SKU",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierId",
                table: "Products",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderLines_ProductId",
                table: "PurchaseOrderLines",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderLines_PurchaseOrderId",
                table: "PurchaseOrderLines",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_PONumber",
                table: "PurchaseOrders",
                column: "PONumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_SupplierId",
                table: "PurchaseOrders",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_WarehouseId",
                table: "PurchaseOrders",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTrackings_ProductId",
                table: "StockTrackings",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTrackings_SupplierId",
                table: "StockTrackings",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTrackings_UserPerformedBy",
                table: "StockTrackings",
                column: "UserPerformedBy");

            migrationBuilder.CreateIndex(
                name: "IX_StockTrackings_WarehouseId",
                table: "StockTrackings",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_FromWarehouseId",
                table: "Transfers",
                column: "FromWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_ProductId",
                table: "Transfers",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_ToWarehouseId",
                table: "Transfers",
                column: "ToWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_TransferNumber",
                table: "Transfers",
                column: "TransferNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_WarehouseId",
                table: "Users",
                column: "WarehouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "PurchaseOrderLines");

            migrationBuilder.DropTable(
                name: "StockTrackings");

            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
