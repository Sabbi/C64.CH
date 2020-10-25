using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace C64.Data.Migrations
{
    public partial class AddFieldsToScener : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "Sceners",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "BirthdayType",
                table: "Sceners",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CountryId",
                table: "Sceners",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Sceners",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sceners_CountryId",
                table: "Sceners",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sceners_Countries_CountryId",
                table: "Sceners",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sceners_Countries_CountryId",
                table: "Sceners");

            migrationBuilder.DropIndex(
                name: "IX_Sceners_CountryId",
                table: "Sceners");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Sceners");

            migrationBuilder.DropColumn(
                name: "BirthdayType",
                table: "Sceners");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Sceners");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Sceners");
        }
    }
}