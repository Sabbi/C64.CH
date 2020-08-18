using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class PartyCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PartyCategoryId",
                table: "ProductionsParties",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PartyCategories",
                columns: table => new
                {
                    PartyCategoryId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Description = table.Column<string>(maxLength: 1024, nullable: true),
                    Selectable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyCategories", x => x.PartyCategoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductionsParties_PartyCategoryId",
                table: "ProductionsParties",
                column: "PartyCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionsParties_PartyCategories_PartyCategoryId",
                table: "ProductionsParties",
                column: "PartyCategoryId",
                principalTable: "PartyCategories",
                principalColumn: "PartyCategoryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductionsParties_PartyCategories_PartyCategoryId",
                table: "ProductionsParties");

            migrationBuilder.DropTable(
                name: "PartyCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductionsParties_PartyCategoryId",
                table: "ProductionsParties");

            migrationBuilder.DropColumn(
                name: "PartyCategoryId",
                table: "ProductionsParties");
        }
    }
}
