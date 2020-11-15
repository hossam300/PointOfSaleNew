using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOfSale.Server.Migrations
{
    public partial class AlterPrinterTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaxMappings_FiscalPointOfSaleitions_FiscalPointOfSaleitionId",
                table: "TaxMappings");

            migrationBuilder.RenameColumn(
                name: "FiscalPointOfSaleitionId",
                table: "TaxMappings",
                newName: "FiscalPositionId");

            migrationBuilder.RenameIndex(
                name: "IX_TaxMappings_FiscalPointOfSaleitionId",
                table: "TaxMappings",
                newName: "IX_TaxMappings_FiscalPositionId");

            migrationBuilder.AddColumn<string>(
                name: "PrinterName",
                table: "Printer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxMappings_FiscalPointOfSaleitions_FiscalPositionId",
                table: "TaxMappings",
                column: "FiscalPositionId",
                principalTable: "FiscalPointOfSaleitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaxMappings_FiscalPointOfSaleitions_FiscalPositionId",
                table: "TaxMappings");

            migrationBuilder.DropColumn(
                name: "PrinterName",
                table: "Printer");

            migrationBuilder.RenameColumn(
                name: "FiscalPositionId",
                table: "TaxMappings",
                newName: "FiscalPointOfSaleitionId");

            migrationBuilder.RenameIndex(
                name: "IX_TaxMappings_FiscalPositionId",
                table: "TaxMappings",
                newName: "IX_TaxMappings_FiscalPointOfSaleitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaxMappings_FiscalPointOfSaleitions_FiscalPointOfSaleitionId",
                table: "TaxMappings",
                column: "FiscalPointOfSaleitionId",
                principalTable: "FiscalPointOfSaleitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
