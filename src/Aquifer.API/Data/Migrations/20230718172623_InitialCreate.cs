using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.API.Data.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Languages",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ISO6393Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                EnglishDisplay = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Languages", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Passages",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                StartBnVerse = table.Column<int>(type: "int", nullable: false),
                EndBnVerse = table.Column<int>(type: "int", nullable: false),
                Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Passages", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Resources",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Type = table.Column<int>(type: "int", nullable: false),
                EnglishLabel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Tag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Resources", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "PassageResources",
            columns: table => new
            {
                PassageId = table.Column<int>(type: "int", nullable: false),
                ResourceId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PassageResources", x => new { x.PassageId, x.ResourceId });
                table.ForeignKey(
                    name: "FK_PassageResources_Passages_PassageId",
                    column: x => x.PassageId,
                    principalTable: "Passages",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_PassageResources_Resources_ResourceId",
                    column: x => x.ResourceId,
                    principalTable: "Resources",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ResourceContents",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ResourceId = table.Column<int>(type: "int", nullable: false),
                LanguageId = table.Column<int>(type: "int", nullable: false),
                DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Version = table.Column<int>(type: "int", nullable: false),
                Completed = table.Column<bool>(type: "bit", nullable: false),
                Trusted = table.Column<bool>(type: "bit", nullable: false),
                Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ResourceContents", x => x.Id);
                table.ForeignKey(
                    name: "FK_ResourceContents_Languages_LanguageId",
                    column: x => x.LanguageId,
                    principalTable: "Languages",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_ResourceContents_Resources_ResourceId",
                    column: x => x.ResourceId,
                    principalTable: "Resources",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_PassageResources_ResourceId",
            table: "PassageResources",
            column: "ResourceId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_ResourceContents_LanguageId",
            table: "ResourceContents",
            column: "LanguageId");

        migrationBuilder.CreateIndex(
            name: "IX_ResourceContents_ResourceId",
            table: "ResourceContents",
            column: "ResourceId",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "PassageResources");

        migrationBuilder.DropTable(
            name: "ResourceContents");

        migrationBuilder.DropTable(
            name: "Passages");

        migrationBuilder.DropTable(
            name: "Languages");

        migrationBuilder.DropTable(
            name: "Resources");
    }
}
