using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class AddDescriptionToHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripion",
                table: "HistoryProductions",
                maxLength: 4096,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripion",
                table: "HistoryProductions");
        }
    }
}
