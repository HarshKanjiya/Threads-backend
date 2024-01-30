using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserApi.microservice.Migrations
{
    /// <inheritdoc />
    public partial class accban : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BanDuration",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BanStartTime",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BanStatus",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BanDuration",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BanStartTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BanStatus",
                table: "Users");
        }
    }
}
