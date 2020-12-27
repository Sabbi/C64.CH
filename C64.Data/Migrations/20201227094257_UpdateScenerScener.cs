using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class UpdateScenerScener : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_scenersceners_sceners_ScenerId",
                table: "scenersceners",
                column: "ScenerId",
                principalTable: "sceners",
                principalColumn: "ScenerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_scenersceners_sceners_ScenerId",
                table: "scenersceners");
        }
    }
}
