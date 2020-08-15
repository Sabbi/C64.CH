using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class UpdatePorofileBirthdayToPartialDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BirthdayType",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthdayType",
                table: "AspNetUsers");
        }
    }
}
