using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddResourceContentVersionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResourceContentVersions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceContentId = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    IsDraft = table.Column<bool>(type: "bit", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentSize = table.Column<int>(type: "int", nullable: false),
                    AssignedUserId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceContentVersions", x => x.Id);
                    table.CheckConstraint("CK_ResourceContentVersions_IsPublishedOrIsDraftNotBoth", "IsPublished = 0 OR IsDraft = 0");
                    table.ForeignKey(
                        name: "FK_ResourceContentVersions_ResourceContents_ResourceContentId",
                        column: x => x.ResourceContentId,
                        principalTable: "ResourceContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceContentVersions_Users_AssignedUserId",
                        column: x => x.AssignedUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersions_AssignedUserId",
                table: "ResourceContentVersions",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersions_ResourceContentId_IsDraft",
                table: "ResourceContentVersions",
                column: "ResourceContentId",
                unique: true,
                filter: "IsDraft = 1");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersions_ResourceContentId_IsPublished",
                table: "ResourceContentVersions",
                column: "ResourceContentId",
                unique: true,
                filter: "IsPublished = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResourceContentVersions");
        }
    }
}
