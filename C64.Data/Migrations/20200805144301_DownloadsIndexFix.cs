using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class DownloadsIndexFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Downloads_ProductionFileId_DownloadedOn",
                table: "Downloads",
                columns: new[] { "ProductionFileId", "DownloadedOn" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Downloads_ProductionFileId_DownloadedOn",
                table: "Downloads");
        }
    }
}
