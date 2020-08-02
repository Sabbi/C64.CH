using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class RemoveUserFromHiddenparts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HiddenParts_AspNetUsers_SubmittedById",
                table: "HiddenParts");

            migrationBuilder.DropIndex(
                name: "IX_HiddenParts_SubmittedById",
                table: "HiddenParts");

            migrationBuilder.DropColumn(
                name: "SubmittedById",
                table: "HiddenParts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubmittedById",
                table: "HiddenParts",
                type: "varchar(36) CHARACTER SET utf8mb4",
                maxLength: 36,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HiddenParts_SubmittedById",
                table: "HiddenParts",
                column: "SubmittedById");

            migrationBuilder.AddForeignKey(
                name: "FK_HiddenParts_AspNetUsers_SubmittedById",
                table: "HiddenParts",
                column: "SubmittedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
