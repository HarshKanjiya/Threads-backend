using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserApi.microservice.Migrations
{
    /// <inheritdoc />
    public partial class avatar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Avatar",
                table: "Users",
                newName: "AvatarURL");

            migrationBuilder.AddColumn<string>(
                name: "AvatarPublicID",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarPublicID",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "AvatarURL",
                table: "Users",
                newName: "Avatar");
        }
    }
}
