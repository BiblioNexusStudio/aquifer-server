using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddVersificationMappingAndExclusion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VersificationExclusions",
                columns: table => new
                {
                    BibleId = table.Column<int>(type: "int", nullable: false),
                    BibleVerseId = table.Column<int>(type: "int", nullable: false),
                    VerseId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VersificationExclusions", x => new { x.BibleId, x.BibleVerseId });
                    table.ForeignKey(
                        name: "FK_VersificationExclusions_Bibles_BibleId",
                        column: x => x.BibleId,
                        principalTable: "Bibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VersificationExclusions_Verses_VerseId",
                        column: x => x.VerseId,
                        principalTable: "Verses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VersificationMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BibleId = table.Column<int>(type: "int", nullable: false),
                    BibleVerseId = table.Column<int>(type: "int", nullable: false),
                    BaseVerseId = table.Column<int>(type: "int", nullable: false),
                    VerseIdPart = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    BaseVerseIdPart = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VersificationMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VersificationMappings_Bibles_BibleId",
                        column: x => x.BibleId,
                        principalTable: "Bibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VersificationMappings_Verses_BaseVerseId",
                        column: x => x.BaseVerseId,
                        principalTable: "Verses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VersificationMappings_Verses_BibleVerseId",
                        column: x => x.BibleVerseId,
                        principalTable: "Verses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VersificationExclusions");

            migrationBuilder.DropTable(
                name: "VersificationMappings");
        }
    }
}
