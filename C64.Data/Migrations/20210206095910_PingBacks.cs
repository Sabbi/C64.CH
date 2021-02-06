using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class PingBacks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CsdbId",
                table: "sceners",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DemoZooId",
                table: "sceners",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LemonId",
                table: "sceners",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PouetId",
                table: "sceners",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CsdbId",
                table: "productions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DemoZooId",
                table: "productions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LemonId",
                table: "productions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PouetId",
                table: "productions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CsdbId",
                table: "parties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DemoZooId",
                table: "parties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LemonId",
                table: "parties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PouetId",
                table: "parties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CsdbId",
                table: "groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DemoZooId",
                table: "groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LemonId",
                table: "groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PouetId",
                table: "groups",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CsdbId",
                table: "sceners");

            migrationBuilder.DropColumn(
                name: "DemoZooId",
                table: "sceners");

            migrationBuilder.DropColumn(
                name: "LemonId",
                table: "sceners");

            migrationBuilder.DropColumn(
                name: "PouetId",
                table: "sceners");

            migrationBuilder.DropColumn(
                name: "CsdbId",
                table: "productions");

            migrationBuilder.DropColumn(
                name: "DemoZooId",
                table: "productions");

            migrationBuilder.DropColumn(
                name: "LemonId",
                table: "productions");

            migrationBuilder.DropColumn(
                name: "PouetId",
                table: "productions");

            migrationBuilder.DropColumn(
                name: "CsdbId",
                table: "parties");

            migrationBuilder.DropColumn(
                name: "DemoZooId",
                table: "parties");

            migrationBuilder.DropColumn(
                name: "LemonId",
                table: "parties");

            migrationBuilder.DropColumn(
                name: "PouetId",
                table: "parties");

            migrationBuilder.DropColumn(
                name: "CsdbId",
                table: "groups");

            migrationBuilder.DropColumn(
                name: "DemoZooId",
                table: "groups");

            migrationBuilder.DropColumn(
                name: "LemonId",
                table: "groups");

            migrationBuilder.DropColumn(
                name: "PouetId",
                table: "groups");
        }
    }
}
