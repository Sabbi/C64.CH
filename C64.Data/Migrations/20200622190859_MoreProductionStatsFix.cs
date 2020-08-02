using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class MoreProductionStatsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "NumberOfComments",
                table: "Productions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfHiddenParts",
                table: "Productions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfVideos",
                table: "Productions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfComments",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "NumberOfHiddenParts",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "NumberOfVideos",
                table: "Productions");

            migrationBuilder.AddColumn<bool>(
                name: "HasComments",
                table: "Productions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasHiddenParts",
                table: "Productions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasVideo",
                table: "Productions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
