using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserActions.microservice.Migrations
{
    /// <inheritdoc />
    public partial class qweqw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RelationId",
                table: "Relationships",
                newName: "RelationshipId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RelationshipId",
                table: "Relationships",
                newName: "RelationId");
        }
    }
}
