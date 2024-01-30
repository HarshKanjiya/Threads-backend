using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin.microservice.Migrations
{
    /// <inheritdoc />
    public partial class fiveds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "HelperId",
                table: "UserReports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "HelperId",
                table: "CustomReports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "CustomReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "CustomReports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "BugReports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HelperId",
                table: "UserReports");

            migrationBuilder.DropColumn(
                name: "HelperId",
                table: "CustomReports");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "CustomReports");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CustomReports");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BugReports");
        }
    }
}
