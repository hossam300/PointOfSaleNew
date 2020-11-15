using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOfSale.Server.Migrations
{
    public partial class AlterShopTableaddnewTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethods_Shop_ShopId",
                table: "PaymentMethods");

            migrationBuilder.DropForeignKey(
                name: "FK_Pricelists_Shop_ShopId",
                table: "Pricelists");

            migrationBuilder.DropForeignKey(
                name: "FK_Printers_Shop_ShopId",
                table: "Printers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Printers_PrinterId",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Shop_ShopId",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_ShopId",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_Pricelists_ShopId",
                table: "Pricelists");

            migrationBuilder.DropIndex(
                name: "IX_PaymentMethods_ShopId",
                table: "PaymentMethods");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "ProxyIPAddress",
                table: "Printers");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "Pricelists");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "PaymentMethods");

            migrationBuilder.RenameColumn(
                name: "PrinterType",
                table: "Printers",
                newName: "PrinterId");

            migrationBuilder.AlterColumn<int>(
                name: "ShopId",
                table: "Printers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Printer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrinterType = table.Column<int>(type: "int", nullable: false),
                    ProxyIPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Printer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShopFloor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FloorId = table.Column<int>(type: "int", nullable: false),
                    ShopId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopFloor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopFloor_Floors_FloorId",
                        column: x => x.FloorId,
                        principalTable: "Floors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopFloor_Shop_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ShopPaymentMethod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false),
                    ShopId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopPaymentMethod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopPaymentMethod_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopPaymentMethod_Shop_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShopPricelist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PricelistId = table.Column<int>(type: "int", nullable: false),
                    ShopId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopPricelist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopPricelist_Pricelists_PricelistId",
                        column: x => x.PricelistId,
                        principalTable: "Pricelists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopPricelist_Shop_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShopProductCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false),
                    ShopId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopProductCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopProductCategory_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopProductCategory_Shop_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Printers_PrinterId",
                table: "Printers",
                column: "PrinterId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopFloor_FloorId",
                table: "ShopFloor",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopFloor_ShopId",
                table: "ShopFloor",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopPaymentMethod_PaymentMethodId",
                table: "ShopPaymentMethod",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopPaymentMethod_ShopId",
                table: "ShopPaymentMethod",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopPricelist_PricelistId",
                table: "ShopPricelist",
                column: "PricelistId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopPricelist_ShopId",
                table: "ShopPricelist",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopProductCategory_ProductCategoryId",
                table: "ShopProductCategory",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopProductCategory_ShopId",
                table: "ShopProductCategory",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "ShopFloor");

            migrationBuilder.DropTable(
                name: "ShopPaymentMethod");

            migrationBuilder.DropTable(
                name: "ShopPricelist");

            migrationBuilder.DropTable(
                name: "ShopProductCategory");

            migrationBuilder.DropIndex(
                name: "IX_Printers_PrinterId",
                table: "Printers");

            migrationBuilder.RenameColumn(
                name: "PrinterId",
                table: "Printers",
                newName: "PrinterType");

            migrationBuilder.AddColumn<int>(
                name: "ShopId",
                table: "ProductCategories",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ShopId",
                table: "Printers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ProxyIPAddress",
                table: "Printers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShopId",
                table: "Pricelists",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShopId",
                table: "PaymentMethods",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ShopId",
                table: "ProductCategories",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Pricelists_ShopId",
                table: "Pricelists",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_ShopId",
                table: "PaymentMethods",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_Shop_ShopId",
                table: "PaymentMethods",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pricelists_Shop_ShopId",
                table: "Pricelists",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Printers_Shop_ShopId",
                table: "Printers",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Printers_PrinterId",
                table: "ProductCategories",
                column: "PrinterId",
                principalTable: "Printers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Shop_ShopId",
                table: "ProductCategories",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
