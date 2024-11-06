using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameBibleTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BibleTexts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BibleId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    ChapterNumber = table.Column<int>(type: "int", nullable: false),
                    VerseNumber = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibleTexts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BibleTexts_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookChapters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    VerseCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookChapters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookChapters_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookResources",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    ResourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookResources", x => new { x.BookId, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_BookResources_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookResources_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookChapterResources",
                columns: table => new
                {
                    BookChapterId = table.Column<int>(type: "int", nullable: false),
                    ResourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookChapterResources", x => new { x.BookChapterId, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_BookChapterResources_BookChapters_BookChapterId",
                        column: x => x.BookChapterId,
                        principalTable: "BookChapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookChapterResources_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookChapterResources_ResourceId",
                table: "BookChapterResources",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_BookResources_ResourceId",
                table: "BookResources",
                column: "ResourceId");

            migrationBuilder.Sql(@"
                    INSERT INTO Books (Id, Code)
                    SELECT DISTINCT Number, Code
                    FROM BibleBooks");

            migrationBuilder.Sql(@"
                    INSERT INTO BookChapters (BookId, Number, VerseCount)
                    SELECT BB.Number, BBC.Number, COUNT(BBCV.Id)
                    FROM BibleBookChapterVerses BBCV
                    INNER JOIN BibleBookChapters BBC ON BBC.Id = BBCV.BibleBookChapterId
                    INNER JOIN BibleBooks BB ON BB.Id = BBC.BibleBookId
                    WHERE BB.BibleId = 1
                    GROUP BY BB.Number, BBC.Number
                    ORDER BY BB.Number, BBC.Number");

            migrationBuilder.Sql(@"
                    INSERT INTO BibleTexts (BibleId, BookId, ChapterNumber, VerseNumber, Text)
                    SELECT BibleId, BB.Number, BBC.Number, BBCV.Number, Text
                    FROM BibleBookChapterVerses BBCV
                    INNER JOIN BibleBookChapters BBC ON BBC.Id = BBCV.BibleBookChapterId
                    INNER JOIN BibleBooks BB ON BB.Id = BBC.BibleBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BibleBookContents_Books_BookId",
                table: "BibleBookContents",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BibleBookContents_Books_BookId",
                table: "BibleBookContents");

            migrationBuilder.DropTable(
                name: "BibleTexts");

            migrationBuilder.DropTable(
                name: "BookChapterResources");

            migrationBuilder.DropTable(
                name: "BookResources");

            migrationBuilder.DropTable(
                name: "BookChapters");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
