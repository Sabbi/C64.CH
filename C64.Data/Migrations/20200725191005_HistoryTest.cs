using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class HistoryTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoryProductions",
                columns: table => new
                {
                    HistoryProductionId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AffectedId = table.Column<int>(nullable: false),
                    OldValue = table.Column<string>(nullable: true),
                    NewValue = table.Column<string>(nullable: true),
                    Property = table.Column<string>(nullable: true),
                    Applied = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryProductions", x => x.HistoryProductionId);
                    table.ForeignKey(
                        name: "FK_HistoryProductions_Productions_AffectedId",
                        column: x => x.AffectedId,
                        principalTable: "Productions",
                        principalColumn: "ProductionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryProductions_AffectedId",
                table: "HistoryProductions",
                column: "AffectedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryProductions");
        }
    }
}
