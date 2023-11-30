using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddResourceContentStatusChangesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedUserId",
                table: "ResourceContents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ResourceContentStatusChanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceContentId = table.Column<int>(type: "int", nullable: false),
                    FromStatus = table.Column<int>(type: "int", nullable: false),
                    ToStatus = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceContentStatusChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceContentStatusChanges_ResourceContents_ResourceContentId",
                        column: x => x.ResourceContentId,
                        principalTable: "ResourceContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceContentStatusChanges_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContents_AssignedUserId",
                table: "ResourceContents",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentStatusChanges_ResourceContentId",
                table: "ResourceContentStatusChanges",
                column: "ResourceContentId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentStatusChanges_UserId",
                table: "ResourceContentStatusChanges",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceContents_Users_AssignedUserId",
                table: "ResourceContents",
                column: "AssignedUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceContents_Users_AssignedUserId",
                table: "ResourceContents");

            migrationBuilder.DropTable(
                name: "ResourceContentStatusChanges");

            migrationBuilder.DropIndex(
                name: "IX_ResourceContents_AssignedUserId",
                table: "ResourceContents");

            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "ResourceContents");
        }
    }
}
