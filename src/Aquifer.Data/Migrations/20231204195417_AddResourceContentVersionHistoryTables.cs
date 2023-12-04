using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddResourceContentVersionHistoryTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResourceContentVersionAssignedUserHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceContentVersionId = table.Column<int>(type: "int", nullable: false),
                    AssignedUserId = table.Column<int>(type: "int", nullable: false),
                    ChangedByUserId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceContentVersionAssignedUserHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceContentVersionAssignedUserHistory_ResourceContentVersions_ResourceContentVersionId",
                        column: x => x.ResourceContentVersionId,
                        principalTable: "ResourceContentVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceContentVersionAssignedUserHistory_Users_AssignedUserId",
                        column: x => x.AssignedUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResourceContentVersionAssignedUserHistory_Users_ChangedByUserId",
                        column: x => x.ChangedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ResourceContentVersionStatusHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceContentVersionId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ChangedByUserId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceContentVersionStatusHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceContentVersionStatusHistory_ResourceContentVersions_ResourceContentVersionId",
                        column: x => x.ResourceContentVersionId,
                        principalTable: "ResourceContentVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceContentVersionStatusHistory_Users_ChangedByUserId",
                        column: x => x.ChangedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersionAssignedUserHistory_AssignedUserId",
                table: "ResourceContentVersionAssignedUserHistory",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersionAssignedUserHistory_ChangedByUserId",
                table: "ResourceContentVersionAssignedUserHistory",
                column: "ChangedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersionAssignedUserHistory_ResourceContentVersionId",
                table: "ResourceContentVersionAssignedUserHistory",
                column: "ResourceContentVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersionStatusHistory_ChangedByUserId",
                table: "ResourceContentVersionStatusHistory",
                column: "ChangedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersionStatusHistory_ResourceContentVersionId",
                table: "ResourceContentVersionStatusHistory",
                column: "ResourceContentVersionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResourceContentVersionAssignedUserHistory");

            migrationBuilder.DropTable(
                name: "ResourceContentVersionStatusHistory");
        }
    }
}
