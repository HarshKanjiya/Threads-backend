using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thread.microservice.Migrations
{
    /// <inheritdoc />
    public partial class inininfd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Total",
                table: "ThreadContentRatings",
                newName: "TotalResponse");

            migrationBuilder.RenameColumn(
                name: "ResponseCounts",
                table: "ThreadContentRatings",
                newName: "Responses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalResponse",
                table: "ThreadContentRatings",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "Responses",
                table: "ThreadContentRatings",
                newName: "ResponseCounts");
        }
    }
}
