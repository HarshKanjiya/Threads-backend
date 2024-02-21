using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin.microservice.Migrations
{
    /// <inheritdoc />
    public partial class qwwqwqqw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SecretKey",
                table: "EnvironmentVariables",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecretKey",
                table: "EnvironmentVariables");
        }
    }
}
