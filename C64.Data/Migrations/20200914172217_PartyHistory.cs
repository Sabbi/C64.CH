using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class PartyHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AffectedPartyId",
                table: "HistoryRecords",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryRecords_AffectedPartyId",
                table: "HistoryRecords",
                column: "AffectedPartyId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryRecords_Parties_AffectedPartyId",
                table: "HistoryRecords",
                column: "AffectedPartyId",
                principalTable: "Parties",
                principalColumn: "PartyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryRecords_Parties_AffectedPartyId",
                table: "HistoryRecords");

            migrationBuilder.DropIndex(
                name: "IX_HistoryRecords_AffectedPartyId",
                table: "HistoryRecords");

            migrationBuilder.DropColumn(
                name: "AffectedPartyId",
                table: "HistoryRecords");
        }
    }
}
