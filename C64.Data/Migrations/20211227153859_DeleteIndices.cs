using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class DeleteIndices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_sceners_Deleted",
                table: "sceners",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_productions_Deleted",
                table: "productions",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_parties_Deleted",
                table: "parties",
                column: "Deleted");

            migrationBuilder.CreateIndex(
                name: "IX_groups_Deleted",
                table: "groups",
                column: "Deleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_sceners_Deleted",
                table: "sceners");

            migrationBuilder.DropIndex(
                name: "IX_productions_Deleted",
                table: "productions");

            migrationBuilder.DropIndex(
                name: "IX_parties_Deleted",
                table: "parties");

            migrationBuilder.DropIndex(
                name: "IX_groups_Deleted",
                table: "groups");
        }
    }
}
