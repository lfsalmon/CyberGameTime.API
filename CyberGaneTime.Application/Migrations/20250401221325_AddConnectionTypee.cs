using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberGameTime.Application.Migrations
{
    /// <inheritdoc />
    public partial class AddConnectionTypee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConnectionType",
                table: "ScreenDevices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnectionType",
                table: "ScreenDevices");
        }
    }
}
