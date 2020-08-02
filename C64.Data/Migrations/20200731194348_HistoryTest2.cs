using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class HistoryTest2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Applied",
                table: "HistoryProductions",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "HistoryProductions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "HistoryProductions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "HistoryProductions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Undid",
                table: "HistoryProductions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "HistoryProductions",
                maxLength: 36,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryProductions_UserId",
                table: "HistoryProductions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryProductions_AspNetUsers_UserId",
                table: "HistoryProductions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryProductions_AspNetUsers_UserId",
                table: "HistoryProductions");

            migrationBuilder.DropIndex(
                name: "IX_HistoryProductions_UserId",
                table: "HistoryProductions");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "HistoryProductions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "HistoryProductions");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "HistoryProductions");

            migrationBuilder.DropColumn(
                name: "Undid",
                table: "HistoryProductions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "HistoryProductions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Applied",
                table: "HistoryProductions",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
