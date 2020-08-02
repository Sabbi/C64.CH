using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class MoreProductionStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasComments",
                table: "Productions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasHiddenParts",
                table: "Productions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasVideo",
                table: "Productions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPartyRelease",
                table: "Productions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasComments",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "HasHiddenParts",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "HasVideo",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "IsPartyRelease",
                table: "Productions");
        }
    }
}
