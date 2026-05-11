using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseManagement.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class RemoveInventoryName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Inventories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Inventories",
                type: "longtext",
                nullable: false);
        }
    }
}
