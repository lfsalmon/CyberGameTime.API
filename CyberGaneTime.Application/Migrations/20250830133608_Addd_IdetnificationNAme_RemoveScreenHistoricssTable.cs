using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberGameTime.Application.Migrations
{
    /// <inheritdoc />
    public partial class Addd_IdetnificationNAme_RemoveScreenHistoricssTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScreenHistorics");

            migrationBuilder.AddColumn<string>(
                name: "IdentificationName",
                table: "ScreenDevices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentificationName",
                table: "ScreenDevices");

            migrationBuilder.CreateTable(
                name: "ScreenHistorics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentStatus = table.Column<int>(type: "int", nullable: false),
                    IpAddres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenHistorics", x => x.Id);
                });
        }
    }
}
