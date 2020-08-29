using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class HistoryRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryProductions");

            migrationBuilder.CreateTable(
                name: "HistoryRecords",
                columns: table => new
                {
                    HistoryRecordId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TransactionId = table.Column<string>(maxLength: 36, nullable: false),
                    AffectedEntity = table.Column<int>(nullable: false),
                    AffectedProductionId = table.Column<int>(nullable: true),
                    AffectedGroupId = table.Column<int>(nullable: true),
                    AffectedScenerId = table.Column<int>(nullable: true),
                    OldValue = table.Column<string>(nullable: true),
                    NewValue = table.Column<string>(nullable: true),
                    Property = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(maxLength: 36, nullable: true),
                    IpAdress = table.Column<string>(maxLength: 46, nullable: true),
                    Applied = table.Column<DateTime>(nullable: true),
                    Undid = table.Column<DateTime>(nullable: true),
                    Version = table.Column<decimal>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 4096, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryRecords", x => x.HistoryRecordId);
                    table.ForeignKey(
                        name: "FK_HistoryRecords_Groups_AffectedGroupId",
                        column: x => x.AffectedGroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryRecords_Productions_AffectedProductionId",
                        column: x => x.AffectedProductionId,
                        principalTable: "Productions",
                        principalColumn: "ProductionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryRecords_Sceners_AffectedScenerId",
                        column: x => x.AffectedScenerId,
                        principalTable: "Sceners",
                        principalColumn: "ScenerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryRecords_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryRecords_AffectedGroupId",
                table: "HistoryRecords",
                column: "AffectedGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryRecords_AffectedProductionId",
                table: "HistoryRecords",
                column: "AffectedProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryRecords_AffectedScenerId",
                table: "HistoryRecords",
                column: "AffectedScenerId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryRecords_UserId",
                table: "HistoryRecords",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryRecords");

            migrationBuilder.CreateTable(
                name: "HistoryProductions",
                columns: table => new
                {
                    HistoryProductionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AffectedId = table.Column<int>(type: "int", nullable: false),
                    Applied = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Description = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", maxLength: 4096, nullable: true),
                    IpAdress = table.Column<string>(type: "varchar(46) CHARACTER SET utf8mb4", maxLength: 46, nullable: true),
                    NewValue = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    OldValue = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Property = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<string>(type: "varchar(36) CHARACTER SET utf8mb4", maxLength: 36, nullable: false),
                    Type = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Undid = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UserId = table.Column<string>(type: "varchar(36) CHARACTER SET utf8mb4", maxLength: 36, nullable: true),
                    Version = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_HistoryProductions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryProductions_AffectedId",
                table: "HistoryProductions",
                column: "AffectedId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryProductions_UserId",
                table: "HistoryProductions",
                column: "UserId");
        }
    }
}
