using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thread.microservice.Migrations
{
    /// <inheritdoc />
    public partial class sdsdsd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Threads");

            migrationBuilder.AddColumn<Guid>(
                name: "ContentId",
                table: "Threads",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ThreadContentRatings",
                columns: table => new
                {
                    RatingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    ResponseCounts = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadContentRatings", x => x.RatingsId);
                });

            migrationBuilder.CreateTable(
                name: "ThreadContent",
                columns: table => new
                {
                    ContentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MyProperty = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RatingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "ThreadContentOptions",
                columns: table => new
                {
                    OptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Option = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThreadContentContentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadContentOptions", x => x.OptionId);
                    table.ForeignKey(
                        name: "FK_ThreadContentOptions_ThreadContent_ThreadContentContentId",
                        column: x => x.ThreadContentContentId,
                        principalTable: "ThreadContent",
                        principalColumn: "ContentId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Threads_ContentId",
                table: "Threads",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadContent_RatingsId",
                table: "ThreadContent",
                column: "RatingsId");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadContentOptions_ThreadContentContentId",
                table: "ThreadContentOptions",
                column: "ThreadContentContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_ThreadContent_ContentId",
                table: "Threads",
                column: "ContentId",
                principalTable: "ThreadContent",
                principalColumn: "ContentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Threads_ThreadContent_ContentId",
                table: "Threads");

            migrationBuilder.DropTable(
                name: "ThreadContentOptions");

            migrationBuilder.DropTable(
                name: "ThreadContent");

            migrationBuilder.DropTable(
                name: "ThreadContentRatings");

            migrationBuilder.DropIndex(
                name: "IX_Threads_ContentId",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "Threads");

            migrationBuilder.AddColumn<int>(
                name: "Content",
                table: "Threads",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
