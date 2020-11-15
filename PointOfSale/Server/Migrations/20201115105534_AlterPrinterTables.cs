using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOfSale.Server.Migrations
{
    public partial class AlterPrinterTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Printers_Printer_PrinterId",
                table: "Printers");

            migrationBuilder.DropForeignKey(
                name: "FK_Printers_Shop_ShopId",
                table: "Printers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Printer_PrinterId",
                table: "ProductCategories");

            migrationBuilder.DropTable(
                name: "Printer");

            migrationBuilder.DropIndex(
                name: "IX_Printers_PrinterId",
                table: "Printers");

            migrationBuilder.DropIndex(
                name: "IX_Printers_ShopId",
                table: "Printers");

            migrationBuilder.DropColumn(
                name: "PrinterId",
                table: "Printers");

            migrationBuilder.RenameColumn(
                name: "ShopId",
                table: "Printers",
                newName: "PrinterType");

            migrationBuilder.AddColumn<string>(
                name: "PrinterName",
                table: "Printers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProxyIPAddress",
                table: "Printers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ShopPrinters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrinterId = table.Column<int>(type: "int", nullable: false),
                    ShopId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopPrinters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopPrinters_Printers_PrinterId",
                        column: x => x.PrinterId,
                        principalTable: "Printers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopPrinters_Shop_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopPrinters_PrinterId",
                table: "ShopPrinters",
                column: "PrinterId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopPrinters_ShopId",
                table: "ShopPrinters",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Printers_PrinterId",
                table: "ProductCategories",
                column: "PrinterId",
                principalTable: "Printers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Printers_PrinterId",
                table: "ProductCategories");

            migrationBuilder.DropTable(
                name: "ShopPrinters");

            migrationBuilder.DropColumn(
                name: "PrinterName",
                table: "Printers");

            migrationBuilder.DropColumn(
                name: "ProxyIPAddress",
                table: "Printers");

            migrationBuilder.RenameColumn(
                name: "PrinterType",
                table: "Printers",
                newName: "ShopId");

            migrationBuilder.AddColumn<int>(
                name: "PrinterId",
                table: "Printers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Printer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrinterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrinterType = table.Column<int>(type: "int", nullable: false),
                    ProxyIPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Printer", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Printers_PrinterId",
                table: "Printers",
                column: "PrinterId");

            migrationBuilder.CreateIndex(
                name: "IX_Printers_ShopId",
                table: "Printers",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Printers_Printer_PrinterId",
                table: "Printers",
                column: "PrinterId",
                principalTable: "Printer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Printers_Shop_ShopId",
                table: "Printers",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Printer_PrinterId",
                table: "ProductCategories",
                column: "PrinterId",
                principalTable: "Printer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
