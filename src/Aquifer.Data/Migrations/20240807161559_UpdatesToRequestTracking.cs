using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatesToRequestTracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EndpointId",
                table: "ResourceContentRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "ResourceContentRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubscriptionName",
                table: "ResourceContentRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ResourceContentRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ResourceContentVersionEditTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceContentVersionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceContentVersionEditTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceContentVersionEditTimes_ResourceContentVersions_ResourceContentVersionId",
                        column: x => x.ResourceContentVersionId,
                        principalTable: "ResourceContentVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceContentVersionEditTimes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResourceContentVersionEditTimes");

            migrationBuilder.DropColumn(
                name: "EndpointId",
                table: "ResourceContentRequests");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "ResourceContentRequests");

            migrationBuilder.DropColumn(
                name: "SubscriptionName",
                table: "ResourceContentRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ResourceContentRequests");
        }
    }
}
