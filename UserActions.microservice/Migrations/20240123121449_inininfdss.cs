using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserActions.microservice.Migrations
{
    /// <inheritdoc />
    public partial class inininfdss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PollResponses");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Relationships",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Relationships",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "PollResponses",
                columns: table => new
                {
                    PollResponseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Selection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThreadId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollResponses", x => x.PollResponseId);
                });
        }
    }
}
