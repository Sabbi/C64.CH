using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class GroupStatsToMainTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupStats");

            migrationBuilder.AddColumn<decimal>(
                name: "AverageRating",
                table: "Groups",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Downloads",
                table: "Groups",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfRatings",
                table: "Groups",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfReleases",
                table: "Groups",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SumOfRatings",
                table: "Groups",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "Groups",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Downloads",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "NumberOfRatings",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "NumberOfReleases",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "SumOfRatings",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Views",
                table: "Groups");

            migrationBuilder.CreateTable(
                name: "GroupStats",
                columns: table => new
                {
                    GroupStatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AverageRating = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Downloads = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    NumberOfRatings = table.Column<int>(type: "int", nullable: false),
                    NumberOfReleases = table.Column<int>(type: "int", nullable: false),
                    SumOfRatings = table.Column<int>(type: "int", nullable: false),
                    Views = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupStats", x => x.GroupStatId);
                    table.ForeignKey(
                        name: "FK_GroupStats_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupStats_AverageRating",
                table: "GroupStats",
                column: "AverageRating");

            migrationBuilder.CreateIndex(
                name: "IX_GroupStats_Downloads",
                table: "GroupStats",
                column: "Downloads");

            migrationBuilder.CreateIndex(
                name: "IX_GroupStats_GroupId",
                table: "GroupStats",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupStats_NumberOfRatings",
                table: "GroupStats",
                column: "NumberOfRatings");

            migrationBuilder.CreateIndex(
                name: "IX_GroupStats_NumberOfReleases",
                table: "GroupStats",
                column: "NumberOfReleases");

            migrationBuilder.CreateIndex(
                name: "IX_GroupStats_Views",
                table: "GroupStats",
                column: "Views");
        }
    }
}
