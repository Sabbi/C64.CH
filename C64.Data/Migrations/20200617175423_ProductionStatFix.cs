using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class ProductionStatFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductionStat_Productions_ProductionId",
                table: "ProductionStat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductionStat",
                table: "ProductionStat");

            migrationBuilder.RenameTable(
                name: "ProductionStat",
                newName: "ProductionStats");

            migrationBuilder.RenameIndex(
                name: "IX_ProductionStat_ProductionId",
                table: "ProductionStats",
                newName: "IX_ProductionStats_ProductionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductionStats",
                table: "ProductionStats",
                column: "ProductionStatId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionStats_Productions_ProductionId",
                table: "ProductionStats",
                column: "ProductionId",
                principalTable: "Productions",
                principalColumn: "ProductionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductionStats_Productions_ProductionId",
                table: "ProductionStats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductionStats",
                table: "ProductionStats");

            migrationBuilder.RenameTable(
                name: "ProductionStats",
                newName: "ProductionStat");

            migrationBuilder.RenameIndex(
                name: "IX_ProductionStats_ProductionId",
                table: "ProductionStat",
                newName: "IX_ProductionStat_ProductionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductionStat",
                table: "ProductionStat",
                column: "ProductionStatId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionStat_Productions_ProductionId",
                table: "ProductionStat",
                column: "ProductionId",
                principalTable: "Productions",
                principalColumn: "ProductionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
