using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOfSale.Server.Migrations
{
    public partial class AlterShopTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FloorsTables",
                table: "Shop",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FloorsTables",
                table: "Shop");
        }
    }
}
