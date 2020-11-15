using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOfSale.Server.Migrations
{
    public partial class AlterShopRelationShips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopEmployee_AspNetUsers_UserId",
                table: "ShopEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopEmployee_Shop_ShopId",
                table: "ShopEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopFloor_Floors_FloorId",
                table: "ShopFloor");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopFloor_Shop_ShopId",
                table: "ShopFloor");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopPaymentMethod_PaymentMethods_PaymentMethodId",
                table: "ShopPaymentMethod");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopPaymentMethod_Shop_ShopId",
                table: "ShopPaymentMethod");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopPricelist_Pricelists_PricelistId",
                table: "ShopPricelist");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopPricelist_Shop_ShopId",
                table: "ShopPricelist");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopProductCategory_ProductCategories_ProductCategoryId",
                table: "ShopProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopProductCategory_Shop_ShopId",
                table: "ShopProductCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopProductCategory",
                table: "ShopProductCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopPricelist",
                table: "ShopPricelist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopPaymentMethod",
                table: "ShopPaymentMethod");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopFloor",
                table: "ShopFloor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopEmployee",
                table: "ShopEmployee");

            migrationBuilder.RenameTable(
                name: "ShopProductCategory",
                newName: "ShopProductCategories");

            migrationBuilder.RenameTable(
                name: "ShopPricelist",
                newName: "ShopPricelists");

            migrationBuilder.RenameTable(
                name: "ShopPaymentMethod",
                newName: "ShopPaymentMethods");

            migrationBuilder.RenameTable(
                name: "ShopFloor",
                newName: "ShopFloors");

            migrationBuilder.RenameTable(
                name: "ShopEmployee",
                newName: "ShopEmployees");

            migrationBuilder.RenameIndex(
                name: "IX_ShopProductCategory_ShopId",
                table: "ShopProductCategories",
                newName: "IX_ShopProductCategories_ShopId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopProductCategory_ProductCategoryId",
                table: "ShopProductCategories",
                newName: "IX_ShopProductCategories_ProductCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopPricelist_ShopId",
                table: "ShopPricelists",
                newName: "IX_ShopPricelists_ShopId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopPricelist_PricelistId",
                table: "ShopPricelists",
                newName: "IX_ShopPricelists_PricelistId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopPaymentMethod_ShopId",
                table: "ShopPaymentMethods",
                newName: "IX_ShopPaymentMethods_ShopId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopPaymentMethod_PaymentMethodId",
                table: "ShopPaymentMethods",
                newName: "IX_ShopPaymentMethods_PaymentMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopFloor_ShopId",
                table: "ShopFloors",
                newName: "IX_ShopFloors_ShopId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopFloor_FloorId",
                table: "ShopFloors",
                newName: "IX_ShopFloors_FloorId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopEmployee_UserId",
                table: "ShopEmployees",
                newName: "IX_ShopEmployees_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopEmployee_ShopId",
                table: "ShopEmployees",
                newName: "IX_ShopEmployees_ShopId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopProductCategories",
                table: "ShopProductCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopPricelists",
                table: "ShopPricelists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopPaymentMethods",
                table: "ShopPaymentMethods",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopFloors",
                table: "ShopFloors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopEmployees",
                table: "ShopEmployees",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopEmployees_AspNetUsers_UserId",
                table: "ShopEmployees",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopEmployees_Shop_ShopId",
                table: "ShopEmployees",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopFloors_Floors_FloorId",
                table: "ShopFloors",
                column: "FloorId",
                principalTable: "Floors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopFloors_Shop_ShopId",
                table: "ShopFloors",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopPaymentMethods_PaymentMethods_PaymentMethodId",
                table: "ShopPaymentMethods",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopPaymentMethods_Shop_ShopId",
                table: "ShopPaymentMethods",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopPricelists_Pricelists_PricelistId",
                table: "ShopPricelists",
                column: "PricelistId",
                principalTable: "Pricelists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopPricelists_Shop_ShopId",
                table: "ShopPricelists",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopProductCategories_ProductCategories_ProductCategoryId",
                table: "ShopProductCategories",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopProductCategories_Shop_ShopId",
                table: "ShopProductCategories",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopEmployees_AspNetUsers_UserId",
                table: "ShopEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopEmployees_Shop_ShopId",
                table: "ShopEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopFloors_Floors_FloorId",
                table: "ShopFloors");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopFloors_Shop_ShopId",
                table: "ShopFloors");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopPaymentMethods_PaymentMethods_PaymentMethodId",
                table: "ShopPaymentMethods");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopPaymentMethods_Shop_ShopId",
                table: "ShopPaymentMethods");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopPricelists_Pricelists_PricelistId",
                table: "ShopPricelists");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopPricelists_Shop_ShopId",
                table: "ShopPricelists");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopProductCategories_ProductCategories_ProductCategoryId",
                table: "ShopProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopProductCategories_Shop_ShopId",
                table: "ShopProductCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopProductCategories",
                table: "ShopProductCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopPricelists",
                table: "ShopPricelists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopPaymentMethods",
                table: "ShopPaymentMethods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopFloors",
                table: "ShopFloors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopEmployees",
                table: "ShopEmployees");

            migrationBuilder.RenameTable(
                name: "ShopProductCategories",
                newName: "ShopProductCategory");

            migrationBuilder.RenameTable(
                name: "ShopPricelists",
                newName: "ShopPricelist");

            migrationBuilder.RenameTable(
                name: "ShopPaymentMethods",
                newName: "ShopPaymentMethod");

            migrationBuilder.RenameTable(
                name: "ShopFloors",
                newName: "ShopFloor");

            migrationBuilder.RenameTable(
                name: "ShopEmployees",
                newName: "ShopEmployee");

            migrationBuilder.RenameIndex(
                name: "IX_ShopProductCategories_ShopId",
                table: "ShopProductCategory",
                newName: "IX_ShopProductCategory_ShopId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopProductCategories_ProductCategoryId",
                table: "ShopProductCategory",
                newName: "IX_ShopProductCategory_ProductCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopPricelists_ShopId",
                table: "ShopPricelist",
                newName: "IX_ShopPricelist_ShopId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopPricelists_PricelistId",
                table: "ShopPricelist",
                newName: "IX_ShopPricelist_PricelistId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopPaymentMethods_ShopId",
                table: "ShopPaymentMethod",
                newName: "IX_ShopPaymentMethod_ShopId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopPaymentMethods_PaymentMethodId",
                table: "ShopPaymentMethod",
                newName: "IX_ShopPaymentMethod_PaymentMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopFloors_ShopId",
                table: "ShopFloor",
                newName: "IX_ShopFloor_ShopId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopFloors_FloorId",
                table: "ShopFloor",
                newName: "IX_ShopFloor_FloorId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopEmployees_UserId",
                table: "ShopEmployee",
                newName: "IX_ShopEmployee_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopEmployees_ShopId",
                table: "ShopEmployee",
                newName: "IX_ShopEmployee_ShopId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopProductCategory",
                table: "ShopProductCategory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopPricelist",
                table: "ShopPricelist",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopPaymentMethod",
                table: "ShopPaymentMethod",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopFloor",
                table: "ShopFloor",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopEmployee",
                table: "ShopEmployee",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopEmployee_AspNetUsers_UserId",
                table: "ShopEmployee",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopEmployee_Shop_ShopId",
                table: "ShopEmployee",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopFloor_Floors_FloorId",
                table: "ShopFloor",
                column: "FloorId",
                principalTable: "Floors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopFloor_Shop_ShopId",
                table: "ShopFloor",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopPaymentMethod_PaymentMethods_PaymentMethodId",
                table: "ShopPaymentMethod",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopPaymentMethod_Shop_ShopId",
                table: "ShopPaymentMethod",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopPricelist_Pricelists_PricelistId",
                table: "ShopPricelist",
                column: "PricelistId",
                principalTable: "Pricelists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopPricelist_Shop_ShopId",
                table: "ShopPricelist",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopProductCategory_ProductCategories_ProductCategoryId",
                table: "ShopProductCategory",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopProductCategory_Shop_ShopId",
                table: "ShopProductCategory",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
