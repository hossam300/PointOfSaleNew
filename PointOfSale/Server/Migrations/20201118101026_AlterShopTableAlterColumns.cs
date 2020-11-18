using Microsoft.EntityFrameworkCore.Migrations;
using PointOfSale.DAL.Domains;

namespace PointOfSale.Server.Migrations
{
    public partial class AlterShopTableAlterColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProductPrices",
                table: "Shop",
                type: "int",
                nullable: false,
                defaultValue: ProductPrices.TaxExcludedPrice,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProductPrices",
                table: "Shop",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
