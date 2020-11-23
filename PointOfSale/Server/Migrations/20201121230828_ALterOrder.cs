using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOfSale.Server.Migrations
{
    public partial class ALterOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_LoyaltyPrograms_LoyaltyProgramId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "LoyaltyProgramId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_LoyaltyPrograms_LoyaltyProgramId",
                table: "Orders",
                column: "LoyaltyProgramId",
                principalTable: "LoyaltyPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_LoyaltyPrograms_LoyaltyProgramId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "LoyaltyProgramId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_LoyaltyPrograms_LoyaltyProgramId",
                table: "Orders",
                column: "LoyaltyProgramId",
                principalTable: "LoyaltyPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
