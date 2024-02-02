using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserApi.microservice.Migrations
{
    /// <inheritdoc />
    public partial class qas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeviceLocation",
                table: "Device",
                newName: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Device",
                newName: "DeviceLocation");
        }
    }
}
