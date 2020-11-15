using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOfSale.Server.Migrations
{
    public partial class AlterPrinterTableColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FiscalPointOfSaleitionName",
                table: "FiscalPointOfSaleitions",
                newName: "FiscalPositionName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FiscalPositionName",
                table: "FiscalPointOfSaleitions",
                newName: "FiscalPointOfSaleitionName");
        }
    }
}
