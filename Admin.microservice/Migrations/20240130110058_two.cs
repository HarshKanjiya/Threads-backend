using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin.microservice.Migrations
{
    /// <inheritdoc />
    public partial class two : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "ReportModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BugReports",
                columns: table => new
                {
                    ReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BugReports", x => x.ReportId);
                });

            migrationBuilder.CreateTable(
                name: "CustomReports",
                columns: table => new
                {
                    ReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomReports", x => x.ReportId);
                });

            migrationBuilder.CreateTable(
                name: "FilesModel",
                columns: table => new
                {
                    FileId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    filePublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fileURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BugReportModelReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesModel", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_FilesModel_BugReports_BugReportModelReportId",
                        column: x => x.BugReportModelReportId,
                        principalTable: "BugReports",
                        principalColumn: "ReportId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilesModel_BugReportModelReportId",
                table: "FilesModel",
                column: "BugReportModelReportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomReports");

            migrationBuilder.DropTable(
                name: "FilesModel");

            migrationBuilder.DropTable(
                name: "BugReports");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "ReportModel");
        }
    }
}
