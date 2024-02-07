using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thread.microservice.Migrations
{
    /// <inheritdoc />
    public partial class dsds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_ThreadContentRatings_RatingsId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_ThreadContentOptions_Contents_ThreadContentContentId",
                table: "ThreadContentOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ThreadContentRatings",
                table: "ThreadContentRatings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ThreadContentOptions",
                table: "ThreadContentOptions");

            migrationBuilder.RenameTable(
                name: "ThreadContentRatings",
                newName: "Ratings");

            migrationBuilder.RenameTable(
                name: "ThreadContentOptions",
                newName: "Options");

            migrationBuilder.RenameIndex(
                name: "IX_ThreadContentOptions_ThreadContentContentId",
                table: "Options",
                newName: "IX_Options_ThreadContentContentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings",
                column: "RatingsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Options",
                table: "Options",
                column: "OptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Ratings_RatingsId",
                table: "Contents",
                column: "RatingsId",
                principalTable: "Ratings",
                principalColumn: "RatingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Contents_ThreadContentContentId",
                table: "Options",
                column: "ThreadContentContentId",
                principalTable: "Contents",
                principalColumn: "ContentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Ratings_RatingsId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Options_Contents_ThreadContentContentId",
                table: "Options");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Options",
                table: "Options");

            migrationBuilder.RenameTable(
                name: "Ratings",
                newName: "ThreadContentRatings");

            migrationBuilder.RenameTable(
                name: "Options",
                newName: "ThreadContentOptions");

            migrationBuilder.RenameIndex(
                name: "IX_Options_ThreadContentContentId",
                table: "ThreadContentOptions",
                newName: "IX_ThreadContentOptions_ThreadContentContentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThreadContentRatings",
                table: "ThreadContentRatings",
                column: "RatingsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThreadContentOptions",
                table: "ThreadContentOptions",
                column: "OptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_ThreadContentRatings_RatingsId",
                table: "Contents",
                column: "RatingsId",
                principalTable: "ThreadContentRatings",
                principalColumn: "RatingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadContentOptions_Contents_ThreadContentContentId",
                table: "ThreadContentOptions",
                column: "ThreadContentContentId",
                principalTable: "Contents",
                principalColumn: "ContentId");
        }
    }
}
