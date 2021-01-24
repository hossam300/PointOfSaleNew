using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOfSale.Server.Migrations
{
    public partial class AlterShopTableAlterColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shop_FiscalPointOfSaleitions_FiscalPointOfSaleitionId",
                table: "Shop");

            migrationBuilder.RenameColumn(
                name: "SpecificFiscalPointOfSaleition",
                table: "Shop",
                newName: "SpecificFiscalPosition");

            migrationBuilder.RenameColumn(
                name: "FiscalPointOfSaleitionPerOrder",
                table: "Shop",
                newName: "FiscalPositionPerOrder");

            migrationBuilder.RenameColumn(
                name: "FiscalPointOfSaleitionId",
                table: "Shop",
                newName: "FiscalPositionId");

            migrationBuilder.RenameIndex(
                name: "IX_Shop_FiscalPointOfSaleitionId",
                table: "Shop",
                newName: "IX_Shop_FiscalPositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shop_FiscalPointOfSaleitions_FiscalPositionId",
                table: "Shop",
                column: "FiscalPositionId",
                principalTable: "FiscalPointOfSaleitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shop_FiscalPointOfSaleitions_FiscalPositionId",
                table: "Shop");

            migrationBuilder.RenameColumn(
                name: "SpecificFiscalPosition",
                table: "Shop",
                newName: "SpecificFiscalPointOfSaleition");

            migrationBuilder.RenameColumn(
                name: "FiscalPositionPerOrder",
                table: "Shop",
                newName: "FiscalPointOfSaleitionPerOrder");

            migrationBuilder.RenameColumn(
                name: "FiscalPositionId",
                table: "Shop",
                newName: "FiscalPointOfSaleitionId");

            migrationBuilder.RenameIndex(
                name: "IX_Shop_FiscalPositionId",
                table: "Shop",
                newName: "IX_Shop_FiscalPointOfSaleitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shop_FiscalPointOfSaleitions_FiscalPointOfSaleitionId",
                table: "Shop",
                column: "FiscalPointOfSaleitionId",
                principalTable: "FiscalPointOfSaleitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
