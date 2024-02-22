using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin.microservice.Migrations
{
    /// <inheritdoc />
    public partial class reports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BugReports",
                columns: table => new
                {
                    ReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HelperId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomReports", x => x.ReportId);
                });

            migrationBuilder.CreateTable(
                name: "EnvironmentVariables",
                columns: table => new
                {
                    VarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecretKey = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentVariables", x => x.VarId);
                });

            migrationBuilder.CreateTable(
                name: "ReportCategories",
                columns: table => new
                {
                    ReportCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportCategories", x => x.ReportCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "BugProofs",
                columns: table => new
                {
                    FileId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    filePublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fileURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BugReportModelReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BugProofs", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_BugProofs_BugReports_BugReportModelReportId",
                        column: x => x.BugReportModelReportId,
                        principalTable: "BugReports",
                        principalColumn: "ReportId");
                });

            migrationBuilder.CreateTable(
                name: "AvailableReports",
                columns: table => new
                {
                    ReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportCategoryModelReportCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableReports", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_AvailableReports_ReportCategories_ReportCategoryModelReportCategoryId",
                        column: x => x.ReportCategoryModelReportCategoryId,
                        principalTable: "ReportCategories",
                        principalColumn: "ReportCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "UserReports",
                columns: table => new
                {
                    UserReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HelperId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportModelReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReports", x => x.UserReportId);
                    table.ForeignKey(
                        name: "FK_UserReports_AvailableReports_ReportModelReportId",
                        column: x => x.ReportModelReportId,
                        principalTable: "AvailableReports",
                        principalColumn: "ReportId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvailableReports_ReportCategoryModelReportCategoryId",
                table: "AvailableReports",
                column: "ReportCategoryModelReportCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BugProofs_BugReportModelReportId",
                table: "BugProofs",
                column: "BugReportModelReportId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReports_ReportModelReportId",
                table: "UserReports",
                column: "ReportModelReportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BugProofs");

            migrationBuilder.DropTable(
                name: "CustomReports");

            migrationBuilder.DropTable(
                name: "EnvironmentVariables");

            migrationBuilder.DropTable(
                name: "UserReports");

            migrationBuilder.DropTable(
                name: "BugReports");

            migrationBuilder.DropTable(
                name: "AvailableReports");

            migrationBuilder.DropTable(
                name: "ReportCategories");
        }
    }
}
