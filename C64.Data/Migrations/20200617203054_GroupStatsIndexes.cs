using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class GroupStatsIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_GroupStats_AverageRating",
                table: "GroupStats",
                column: "AverageRating");

            migrationBuilder.CreateIndex(
                name: "IX_GroupStats_Downloads",
                table: "GroupStats",
                column: "Downloads");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GroupStats_AverageRating",
                table: "GroupStats");

            migrationBuilder.DropIndex(
                name: "IX_GroupStats_Downloads",
                table: "GroupStats");

            migrationBuilder.DropIndex(
                name: "IX_GroupStats_NumberOfRatings",
                table: "GroupStats");

            migrationBuilder.DropIndex(
                name: "IX_GroupStats_NumberOfReleases",
                table: "GroupStats");

            migrationBuilder.DropIndex(
                name: "IX_GroupStats_Views",
                table: "GroupStats");
        }
    }
}
