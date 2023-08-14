using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBibleBookContents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VerseContents");

            migrationBuilder.AddColumn<int>(
                name: "MediaType",
                table: "Resources",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContentSizeKb",
                table: "ResourceContents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BibleBookContents",
                columns: table => new
                {
                    BibleId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AudioUrls = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextSizeKb = table.Column<int>(type: "int", nullable: false),
                    AudioSizeKb = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibleBookContents", x => new { x.BibleId, x.BookId });
                    table.ForeignKey(
                        name: "FK_BibleBookContents_Bibles_BibleId",
                        column: x => x.BibleId,
                        principalTable: "Bibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BibleBookContents");

            migrationBuilder.DropColumn(
                name: "MediaType",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "ContentSizeKb",
                table: "ResourceContents");

            migrationBuilder.CreateTable(
                name: "VerseContents",
                columns: table => new
                {
                    VerseId = table.Column<int>(type: "int", nullable: false),
                    BibleId = table.Column<int>(type: "int", nullable: false),
                    AudioEndTime = table.Column<float>(type: "real", nullable: true),
                    AudioStartTime = table.Column<float>(type: "real", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerseContents", x => new { x.VerseId, x.BibleId });
                    table.ForeignKey(
                        name: "FK_VerseContents_Bibles_BibleId",
                        column: x => x.BibleId,
                        principalTable: "Bibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VerseContents_Verses_VerseId",
                        column: x => x.VerseId,
                        principalTable: "Verses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VerseContents_BibleId",
                table: "VerseContents",
                column: "BibleId");
        }
    }
}
