using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexForVerseLookupSpeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BibleTexts_BibleId_BookId_ChapterNumber_VerseNumber",
                table: "BibleTexts",
                columns: new[] { "BibleId", "BookId", "ChapterNumber", "VerseNumber" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BibleTexts_BibleId_BookId_ChapterNumber_VerseNumber",
                table: "BibleTexts");
        }
    }
}
