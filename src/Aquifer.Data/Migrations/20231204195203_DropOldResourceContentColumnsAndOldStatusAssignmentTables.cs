using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class DropOldResourceContentColumnsAndOldStatusAssignmentTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceContents_Users_AssignedUserId",
                table: "ResourceContents");

            migrationBuilder.DropTable(
                name: "ResourceContentStatusChanges");

            migrationBuilder.DropTable(
                name: "ResourceContentUserAssignments");

            migrationBuilder.DropIndex(
                name: "IX_ResourceContents_AssignedUserId",
                table: "ResourceContents");

            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "ResourceContents");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "ResourceContents");

            migrationBuilder.DropColumn(
                name: "ContentSize",
                table: "ResourceContents");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "ResourceContents");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "ResourceContents");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "ResourceContents");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "ResourceContents");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "ResourceContents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedUserId",
                table: "ResourceContents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "ResourceContents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ContentSize",
                table: "ResourceContents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "ResourceContents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "ResourceContents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "ResourceContents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "ResourceContents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "ResourceContents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ResourceContentStatusChanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceContentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    FromStatus = table.Column<int>(type: "int", nullable: false),
                    ToStatus = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "ResourceContentUserAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignedByUserId = table.Column<int>(type: "int", nullable: false),
                    AssignedUserId = table.Column<int>(type: "int", nullable: false),
                    ResourceContentId = table.Column<int>(type: "int", nullable: false),
                    Completed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceContentUserAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceContentUserAssignments_ResourceContents_ResourceContentId",
                        column: x => x.ResourceContentId,
                        principalTable: "ResourceContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceContentUserAssignments_Users_AssignedByUserId",
                        column: x => x.AssignedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResourceContentUserAssignments_Users_AssignedUserId",
                        column: x => x.AssignedUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
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

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentUserAssignments_AssignedByUserId",
                table: "ResourceContentUserAssignments",
                column: "AssignedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentUserAssignments_AssignedUserId",
                table: "ResourceContentUserAssignments",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentUserAssignments_ResourceContentId",
                table: "ResourceContentUserAssignments",
                column: "ResourceContentId",
                unique: true,
                filter: "Completed IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceContents_Users_AssignedUserId",
                table: "ResourceContents",
                column: "AssignedUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
