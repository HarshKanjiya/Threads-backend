using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Subscription.microservice.Migrations
{
    /// <inheritdoc />
    public partial class subs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "ReportMessages");

            migrationBuilder.DropTable(
                name: "ReportCategories");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "Subscriptions");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Subscriptions",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "PurchaseStatus",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReceiptId",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseStatus",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "ReceiptId",
                table: "Subscriptions");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "OrderStatus",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ReportCategories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportCategories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "ReportMessages",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportMessages", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_ReportMessages_ReportCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ReportCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportMessageId = table.Column<int>(type: "int", nullable: false),
                    CasterId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverPostId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_Reports_ReportMessages_ReportMessageId",
                        column: x => x.ReportMessageId,
                        principalTable: "ReportMessages",
                        principalColumn: "MessageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportMessages_CategoryId",
                table: "ReportMessages",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ReportMessageId",
                table: "Reports",
                column: "ReportMessageId");
        }
    }
}
