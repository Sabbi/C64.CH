using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class CleanupEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VideoId",
                table: "productionvideos",
                type: "varchar(127) CHARACTER SET utf8mb4",
                maxLength: 127,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "historyrecords",
                type: "varchar(255) CHARACTER SET utf8mb4",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Property",
                table: "historyrecords",
                type: "varchar(255) CHARACTER SET utf8mb4",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LogoFile",
                table: "groups",
                type: "varchar(511) CHARACTER SET utf8mb4",
                maxLength: 511,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "dbfiles",
                type: "varchar(255) CHARACTER SET utf8mb4",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(254) CHARACTER SET utf8mb4",
                oldMaxLength: 254);

            migrationBuilder.AlterColumn<string>(
                name: "PublicEmail",
                table: "aspnetusers",
                type: "varchar(255) CHARACTER SET utf8mb4",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VideoId",
                table: "productionvideos",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(127) CHARACTER SET utf8mb4",
                oldMaxLength: 127,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "historyrecords",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Property",
                table: "historyrecords",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LogoFile",
                table: "groups",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(511) CHARACTER SET utf8mb4",
                oldMaxLength: 511,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "dbfiles",
                type: "varchar(254) CHARACTER SET utf8mb4",
                maxLength: 254,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "PublicEmail",
                table: "aspnetusers",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4",
                oldMaxLength: 255,
                oldNullable: true);
        }
    }
}
