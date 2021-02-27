using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class AddAdminQueue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "adminqueues",
                columns: table => new
                {
                    AdminQueueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Processed = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatorName = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", maxLength: 255, nullable: true),
                    CreatorEMail = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", maxLength: 255, nullable: true),
                    CreatorUserId = table.Column<string>(type: "varchar(36) CHARACTER SET utf8mb4", maxLength: 36, nullable: true),
                    ProcessorUserId = table.Column<string>(type: "varchar(36) CHARACTER SET utf8mb4", maxLength: 36, nullable: true),
                    CreatorComment = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", maxLength: 4095, nullable: true),
                    ProcessorComment = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", maxLength: 4095, nullable: true),
                    ShowProcessorComment = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AdminQueueStatus = table.Column<int>(type: "int", nullable: false),
                    AdminQueueRequestType = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", maxLength: 4095, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adminqueues", x => x.AdminQueueId);
                    table.ForeignKey(
                        name: "FK_adminqueues_aspnetusers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "aspnetusers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_adminqueues_aspnetusers_ProcessorUserId",
                        column: x => x.ProcessorUserId,
                        principalTable: "aspnetusers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_adminqueues_CreatorUserId",
                table: "adminqueues",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_adminqueues_ProcessorUserId",
                table: "adminqueues",
                column: "ProcessorUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "adminqueues");
        }
    }
}
