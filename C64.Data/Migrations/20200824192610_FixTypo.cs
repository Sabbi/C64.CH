using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class FixTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripion",
                table: "HistoryProductions");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "HistoryProductions",
                maxLength: 4096,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "HistoryProductions");

            migrationBuilder.AddColumn<string>(
                name: "Descripion",
                table: "HistoryProductions",
                type: "longtext CHARACTER SET utf8mb4",
                maxLength: 4096,
                nullable: true);
        }
    }
}
