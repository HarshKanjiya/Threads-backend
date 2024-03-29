using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserApi.microservice.Migrations
{
    /// <inheritdoc />
    public partial class mentions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Mention",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mention",
                table: "Users");
        }
    }
}
