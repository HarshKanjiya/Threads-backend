using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notification.microservice.Migrations
{
    /// <inheritdoc />
    public partial class fivedsds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CasterImage",
                table: "Notifications",
                newName: "CasterAvatarUrl");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReceiverId",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "CasterId",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "HelperId",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HelperId",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "CasterAvatarUrl",
                table: "Notifications",
                newName: "CasterImage");

            migrationBuilder.AlterColumn<string>(
                name: "ReceiverId",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "CasterId",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
