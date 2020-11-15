using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOfSale.Server.Migrations
{
    public partial class AlterFloorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Floors_Shop_ShopId",
                table: "Floors");

            migrationBuilder.DropIndex(
                name: "IX_Floors_ShopId",
                table: "Floors");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "Floors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShopId",
                table: "Floors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Floors_ShopId",
                table: "Floors",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Floors_Shop_ShopId",
                table: "Floors",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
