using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class GroupStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupStats",
                columns: table => new
                {
                    GroupStatId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Downloads = table.Column<int>(nullable: false),
                    NumberOfRatings = table.Column<int>(nullable: false),
                    SumOfRatings = table.Column<int>(nullable: false),
                    AverageRating = table.Column<decimal>(nullable: false),
                    Views = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false)
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
                name: "IX_GroupStats_GroupId",
                table: "GroupStats",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupStats");
        }
    }
}
