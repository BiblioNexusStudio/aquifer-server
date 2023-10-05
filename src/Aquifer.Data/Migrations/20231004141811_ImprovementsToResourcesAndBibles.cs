using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class ImprovementsToResourcesAndBibles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Resources_Type_MediaType_EnglishLabel",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "MediaType",
                table: "Resources");

            migrationBuilder.RenameColumn(
                name: "Completed",
                table: "ResourceContents",
                newName: "Enabled");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ResourceContents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "Bibles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AssociatedResources",
                columns: table => new
                {
                    AssociatedResourceId = table.Column<int>(type: "int", nullable: false),
                    ResourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociatedResources", x => new { x.AssociatedResourceId, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_AssociatedResources_Resources_AssociatedResourceId",
                        column: x => x.AssociatedResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssociatedResources_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ResourceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resources_Type_EnglishLabel",
                table: "Resources",
                columns: new[] { "Type", "EnglishLabel" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssociatedResources_ResourceId",
                table: "AssociatedResources",
                column: "ResourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssociatedResources");

            migrationBuilder.DropTable(
                name: "ResourceTypes");

            migrationBuilder.DropIndex(
                name: "IX_Resources_Type_EnglishLabel",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ResourceContents");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "Bibles");

            migrationBuilder.RenameColumn(
                name: "Enabled",
                table: "ResourceContents",
                newName: "Completed");

            migrationBuilder.AddColumn<int>(
                name: "MediaType",
                table: "Resources",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_Type_MediaType_EnglishLabel",
                table: "Resources",
                columns: new[] { "Type", "MediaType", "EnglishLabel" },
                unique: true);
        }
    }
}
