using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin.microservice.Migrations
{
    /// <inheritdoc />
    public partial class four : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportModel_ReportCategories_ReportCategoryModelReportCategoryId",
                table: "ReportModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReportModel",
                table: "ReportModel");

            migrationBuilder.RenameTable(
                name: "ReportModel",
                newName: "AvailableReports");

            migrationBuilder.RenameIndex(
                name: "IX_ReportModel_ReportCategoryModelReportCategoryId",
                table: "AvailableReports",
                newName: "IX_AvailableReports_ReportCategoryModelReportCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvailableReports",
                table: "AvailableReports",
                column: "ReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_AvailableReports_ReportCategories_ReportCategoryModelReportCategoryId",
                table: "AvailableReports",
                column: "ReportCategoryModelReportCategoryId",
                principalTable: "ReportCategories",
                principalColumn: "ReportCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvailableReports_ReportCategories_ReportCategoryModelReportCategoryId",
                table: "AvailableReports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AvailableReports",
                table: "AvailableReports");

            migrationBuilder.RenameTable(
                name: "AvailableReports",
                newName: "ReportModel");

            migrationBuilder.RenameIndex(
                name: "IX_AvailableReports_ReportCategoryModelReportCategoryId",
                table: "ReportModel",
                newName: "IX_ReportModel_ReportCategoryModelReportCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReportModel",
                table: "ReportModel",
                column: "ReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportModel_ReportCategories_ReportCategoryModelReportCategoryId",
                table: "ReportModel",
                column: "ReportCategoryModelReportCategoryId",
                principalTable: "ReportCategories",
                principalColumn: "ReportCategoryId");
        }
    }
}
