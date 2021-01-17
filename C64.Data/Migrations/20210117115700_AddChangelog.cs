using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class AddChangelog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "changelogentries",
                columns: table => new
                {
                    ChangeLogEntryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Change = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", maxLength: 65535, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_changelogentries", x => x.ChangeLogEntryId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "changelogentries");
        }
    }
}
