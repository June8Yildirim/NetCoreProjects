using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace WarehouseManagement.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentitySchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Users",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Users",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Users",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Users",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Users",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Users",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    RoleId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("2d16947b-d100-4847-bc9e-127be8dc53c8"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 19, 31, 38, 603, DateTimeKind.Utc).AddTicks(850));

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("663a1e5c-c0b5-46d6-8f29-461be63e82bf"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 19, 31, 38, 603, DateTimeKind.Utc).AddTicks(1720));

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("8e46d376-2ebf-4036-803c-98b280aaf6e0"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 19, 31, 38, 603, DateTimeKind.Utc).AddTicks(1730));

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("a280cc60-ddb6-41d4-8c77-e7236623e535"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 19, 31, 38, 603, DateTimeKind.Utc).AddTicks(1740));

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("c43b9393-1a2f-4b43-aa3a-7f60e2650e73"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 19, 31, 38, 603, DateTimeKind.Utc).AddTicks(1740));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5e83fa4d-5c5b-4a97-bbfc-5794a316bf25"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 19, 31, 38, 602, DateTimeKind.Utc).AddTicks(3650));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("652bafb1-dd00-4525-9b18-7bc138cf4e19"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 19, 31, 38, 602, DateTimeKind.Utc).AddTicks(4240));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("71e30164-cd71-47c0-b3d0-9e55b46a0a5f"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 19, 31, 38, 602, DateTimeKind.Utc).AddTicks(4250));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8863f7cc-ef0f-4686-9d81-b217376f608e"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 19, 31, 38, 602, DateTimeKind.Utc).AddTicks(4260));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("bbbdd36f-811e-4053-9de1-b59fa8bfca39"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 19, 31, 38, 602, DateTimeKind.Utc).AddTicks(4260));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("db3a77f4-6571-4459-943f-537a142e0242"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 19, 31, 38, 602, DateTimeKind.Utc).AddTicks(4270));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("df1da8d1-8c53-488a-88eb-db24a4ac66bf"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 19, 31, 38, 602, DateTimeKind.Utc).AddTicks(4270));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dfdde47b-d9e7-4f12-9add-0881839f2e01"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 19, 31, 38, 602, DateTimeKind.Utc).AddTicks(4280));

            migrationBuilder.UpdateData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("316c467d-3945-44bf-b139-b452ad108bda"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 19, 31, 38, 601, DateTimeKind.Utc).AddTicks(6720));

            migrationBuilder.UpdateData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("c886b05a-ed79-4682-88b1-276f9f573dbb"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 19, 31, 38, 601, DateTimeKind.Utc).AddTicks(7450));

            migrationBuilder.UpdateData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("e260c87f-7d35-4bc6-bf3b-7e43103a062f"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 19, 31, 38, 601, DateTimeKind.Utc).AddTicks(7510));

            migrationBuilder.UpdateData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("f1869c9d-8a17-4f84-a121-194b9aa4b80c"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 19, 31, 38, 601, DateTimeKind.Utc).AddTicks(7510));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2f408d44-a9e1-4cd8-9c16-0f115bd78e60"),
                columns: new[] { "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 0, "e53c5057-d28d-46e1-be61-2f2439bda7fc", new DateTime(2026, 5, 12, 19, 31, 38, 602, DateTimeKind.Utc).AddTicks(4760), false, false, null, null, null, null, null, false, null, false, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("393e4fa6-84a0-4ea2-990a-d2ab42c641ca"),
                columns: new[] { "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 0, "7ec2ff38-1588-4ee3-a170-e00033286ea5", new DateTime(2026, 5, 12, 19, 31, 38, 603, DateTimeKind.Utc).AddTicks(480), false, false, null, null, null, null, null, false, null, false, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("799cd594-ac79-4436-abf3-2a1a57d76860"),
                columns: new[] { "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 0, "60adbdf3-30b1-4593-bc09-38a0dc06c7ec", new DateTime(2026, 5, 12, 19, 31, 38, 603, DateTimeKind.Utc).AddTicks(510), false, false, null, null, null, null, null, false, null, false, null });

            migrationBuilder.UpdateData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("87261fbf-f45e-4f37-bce9-0ca4108311d2"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 19, 31, 38, 602, DateTimeKind.Utc).AddTicks(2460));

            migrationBuilder.UpdateData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("88ebdcc4-708c-446c-ac98-2bad67233d9f"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 19, 31, 38, 602, DateTimeKind.Utc).AddTicks(2840));

            migrationBuilder.UpdateData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("93b76b44-5c36-4e0f-9936-01ef16359b5d"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 19, 31, 38, 602, DateTimeKind.Utc).AddTicks(2840));

            migrationBuilder.UpdateData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("f07f2d6e-5176-47c8-bd34-46499b30556d"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 12, 19, 31, 38, 602, DateTimeKind.Utc).AddTicks(2850));

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "EmailIndex",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("2d16947b-d100-4847-bc9e-127be8dc53c8"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(5610));

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("663a1e5c-c0b5-46d6-8f29-461be63e82bf"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(6090));

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("8e46d376-2ebf-4036-803c-98b280aaf6e0"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(6090));

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("a280cc60-ddb6-41d4-8c77-e7236623e535"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(6100));

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("c43b9393-1a2f-4b43-aa3a-7f60e2650e73"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(6100));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5e83fa4d-5c5b-4a97-bbfc-5794a316bf25"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(4100));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("652bafb1-dd00-4525-9b18-7bc138cf4e19"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(4610));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("71e30164-cd71-47c0-b3d0-9e55b46a0a5f"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(4620));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8863f7cc-ef0f-4686-9d81-b217376f608e"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(4620));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("bbbdd36f-811e-4053-9de1-b59fa8bfca39"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(4630));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("db3a77f4-6571-4459-943f-537a142e0242"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(4630));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("df1da8d1-8c53-488a-88eb-db24a4ac66bf"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(4640));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dfdde47b-d9e7-4f12-9add-0881839f2e01"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(4640));

            migrationBuilder.UpdateData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("316c467d-3945-44bf-b139-b452ad108bda"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 13, 33, 261, DateTimeKind.Utc).AddTicks(8930));

            migrationBuilder.UpdateData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("c886b05a-ed79-4682-88b1-276f9f573dbb"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 13, 33, 261, DateTimeKind.Utc).AddTicks(9550));

            migrationBuilder.UpdateData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("e260c87f-7d35-4bc6-bf3b-7e43103a062f"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 13, 33, 261, DateTimeKind.Utc).AddTicks(9560));

            migrationBuilder.UpdateData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: new Guid("f1869c9d-8a17-4f84-a121-194b9aa4b80c"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 13, 33, 261, DateTimeKind.Utc).AddTicks(9560));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2f408d44-a9e1-4cd8-9c16-0f115bd78e60"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(4990));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("393e4fa6-84a0-4ea2-990a-d2ab42c641ca"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(5360));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("799cd594-ac79-4436-abf3-2a1a57d76860"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(5370));

            migrationBuilder.UpdateData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("87261fbf-f45e-4f37-bce9-0ca4108311d2"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(3120));

            migrationBuilder.UpdateData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("88ebdcc4-708c-446c-ac98-2bad67233d9f"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(3400));

            migrationBuilder.UpdateData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("93b76b44-5c36-4e0f-9936-01ef16359b5d"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(3410));

            migrationBuilder.UpdateData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("f07f2d6e-5176-47c8-bd34-46499b30556d"),
                column: "CreatedAt",
                value: new DateTime(2026, 5, 11, 22, 13, 33, 262, DateTimeKind.Utc).AddTicks(3410));
        }
    }
}
