using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class AddProductionsSceners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "productionssceners",
                columns: table => new
                {
                    ProductionId = table.Column<int>(type: "int", nullable: false),
                    ScenerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productionssceners", x => new { x.ProductionId, x.ScenerId });
                    table.ForeignKey(
                        name: "FK_productionssceners_productions_ProductionId",
                        column: x => x.ProductionId,
                        principalTable: "productions",
                        principalColumn: "ProductionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productionssceners_sceners_ScenerId",
                        column: x => x.ScenerId,
                        principalTable: "sceners",
                        principalColumn: "ScenerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_productionssceners_ScenerId",
                table: "productionssceners",
                column: "ScenerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productionssceners");
        }
    }
}
