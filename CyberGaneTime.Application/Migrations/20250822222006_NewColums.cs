using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberGameTime.Application.Migrations
{
    /// <inheritdoc />
    public partial class NewColums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "URL_IFTTT_Off",
                table: "ScreenDevices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "URL_IFTTT_On",
                table: "ScreenDevices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "URL_IFTTT_Off",
                table: "ScreenDevices");

            migrationBuilder.DropColumn(
                name: "URL_IFTTT_On",
                table: "ScreenDevices");
        }
    }
}
