using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOfSale.Server.Migrations
{
    public partial class AlterSessionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Sessions",
                newName: "SessionNo");

            migrationBuilder.AddColumn<DateTime>(
                name: "ClosedDate",
                table: "Sessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CosedById",
                table: "Sessions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Sessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Sessions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShopId",
                table: "Sessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Sessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_CosedById",
                table: "Sessions",
                column: "CosedById");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_CreatorId",
                table: "Sessions",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ShopId",
                table: "Sessions",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_AspNetUsers_CosedById",
                table: "Sessions",
                column: "CosedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_AspNetUsers_CreatorId",
                table: "Sessions",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Shop_ShopId",
                table: "Sessions",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_AspNetUsers_CosedById",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_AspNetUsers_CreatorId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Shop_ShopId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_CosedById",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_CreatorId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_ShopId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "ClosedDate",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "CosedById",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "SessionNo",
                table: "Sessions",
                newName: "Name");
        }
    }
}
