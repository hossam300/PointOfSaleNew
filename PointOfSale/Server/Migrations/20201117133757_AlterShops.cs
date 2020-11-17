using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOfSale.Server.Migrations
{
    public partial class AlterShops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultPricelistId",
                table: "Shop");

            migrationBuilder.RenameColumn(
                name: "AdvancedPricelists",
                table: "Shop",
                newName: "AdvancedPriceICollections");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdvancedPriceICollections",
                table: "Shop",
                newName: "AdvancedPricelists");

            migrationBuilder.AddColumn<int>(
                name: "DefaultPricelistId",
                table: "Shop",
                type: "int",
                nullable: true);
        }
    }
}
