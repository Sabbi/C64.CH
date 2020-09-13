using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class AddCountryToGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryId",
                table: "Groups",
                maxLength: 2,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CountryId",
                table: "Groups",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Countries_CountryId",
                table: "Groups",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Countries_CountryId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_CountryId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Groups");
        }
    }
}
