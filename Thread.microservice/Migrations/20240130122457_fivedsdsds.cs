using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thread.microservice.Migrations
{
    /// <inheritdoc />
    public partial class fivedsdsds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BanStatus",
                table: "Threads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BanStatus",
                table: "Threads");
        }
    }
}
