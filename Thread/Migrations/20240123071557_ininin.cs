using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thread.microservice.Migrations
{
    /// <inheritdoc />
    public partial class ininin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadContentOptions_ThreadContent_ThreadContentContentId",
                table: "ThreadContentOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_ThreadContent_ContentId",
                table: "Threads");

            migrationBuilder.DropTable(
                name: "ThreadContent");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Threads",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ReplyAccess",
                table: "Threads",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    ContentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Files = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RatingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.ContentId);
                    table.ForeignKey(
                        name: "FK_Contents_ThreadContentRatings_RatingsId",
                        column: x => x.RatingsId,
                        principalTable: "ThreadContentRatings",
                        principalColumn: "RatingsId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contents_RatingsId",
                table: "Contents",
                column: "RatingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadContentOptions_Contents_ThreadContentContentId",
                table: "ThreadContentOptions",
                column: "ThreadContentContentId",
                principalTable: "Contents",
                principalColumn: "ContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Contents_ContentId",
                table: "Threads",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "ContentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadContentOptions_Contents_ThreadContentContentId",
                table: "ThreadContentOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Contents_ContentId",
                table: "Threads");

            migrationBuilder.DropTable(
                name: "Contents");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Threads",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ReplyAccess",
                table: "Threads",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "ThreadContent",
                columns: table => new
                {
                    ContentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RatingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyProperty = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadContent", x => x.ContentId);
                    table.ForeignKey(
                        name: "FK_ThreadContent_ThreadContentRatings_RatingsId",
                        column: x => x.RatingsId,
                        principalTable: "ThreadContentRatings",
                        principalColumn: "RatingsId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThreadContent_RatingsId",
                table: "ThreadContent",
                column: "RatingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadContentOptions_ThreadContent_ThreadContentContentId",
                table: "ThreadContentOptions",
                column: "ThreadContentContentId",
                principalTable: "ThreadContent",
                principalColumn: "ContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_ThreadContent_ContentId",
                table: "Threads",
                column: "ContentId",
                principalTable: "ThreadContent",
                principalColumn: "ContentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
