using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOfSale.Server.Migrations
{
    public partial class AlterPurchaseOrderAddShopId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShopId",
                table: "PurchaseOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "PurchaseOrders");
        }
    }
}
