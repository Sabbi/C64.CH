using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class ProductionStatsToMainTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductionStats");

            migrationBuilder.AddColumn<decimal>(
                name: "AverageRating",
                table: "Productions",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Downloads",
                table: "Productions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfRatings",
                table: "Productions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SumOfRatings",
                table: "Productions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "Productions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "Downloads",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "NumberOfRatings",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "SumOfRatings",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "Views",
                table: "Productions");

            migrationBuilder.CreateTable(
                name: "ProductionStats",
                columns: table => new
                {
                    ProductionStatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AverageRating = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Downloads = table.Column<int>(type: "int", nullable: false),
                    NumberOfRatings = table.Column<int>(type: "int", nullable: false),
                    ProductionId = table.Column<int>(type: "int", nullable: false),
                    SumOfRatings = table.Column<int>(type: "int", nullable: false),
                    Views = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionStats", x => x.ProductionStatId);
                    table.ForeignKey(
                        name: "FK_ProductionStats_Productions_ProductionId",
                        column: x => x.ProductionId,
                        principalTable: "Productions",
                        principalColumn: "ProductionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductionStats_ProductionId",
                table: "ProductionStats",
                column: "ProductionId",
                unique: true);
        }
    }
}
