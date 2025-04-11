using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberGameTime.Application.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityPropertiesInRentalScreenAndScreenHistoric : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "ScreenHistorics");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "ScreenDevices");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "RentalScreens",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "RentalScreens");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "ScreenHistorics",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "ScreenDevices",
                type: "datetime2",
                nullable: true);
        }
    }
}
