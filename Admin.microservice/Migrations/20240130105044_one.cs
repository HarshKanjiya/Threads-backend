using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin.microservice.Migrations
{
    /// <inheritdoc />
    public partial class one : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportCategories",
                columns: table => new
                {
                    ReportCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportCategories", x => x.ReportCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "UserReports",
                columns: table => new
                {
                    UserReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReports", x => x.UserReportId);
                });

            migrationBuilder.CreateTable(
                name: "ReportModel",
                columns: table => new
                {
                    ReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportCategoryModelReportCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportModel", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_ReportModel_ReportCategories_ReportCategoryModelReportCategoryId",
                        column: x => x.ReportCategoryModelReportCategoryId,
                        principalTable: "ReportCategories",
                        principalColumn: "ReportCategoryId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportModel_ReportCategoryModelReportCategoryId",
                table: "ReportModel",
                column: "ReportCategoryModelReportCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportModel");

            migrationBuilder.DropTable(
                name: "UserReports");

            migrationBuilder.DropTable(
                name: "ReportCategories");
        }
    }
}
