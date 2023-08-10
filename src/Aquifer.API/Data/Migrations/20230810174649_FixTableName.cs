using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VerseContentEntity_Bibles_BibleId",
                table: "VerseContentEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_VerseContentEntity_Verses_VerseId",
                table: "VerseContentEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VerseContentEntity",
                table: "VerseContentEntity");

            migrationBuilder.RenameTable(
                name: "VerseContentEntity",
                newName: "VerseContents");

            migrationBuilder.RenameIndex(
                name: "IX_VerseContentEntity_BibleId",
                table: "VerseContents",
                newName: "IX_VerseContents_BibleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VerseContents",
                table: "VerseContents",
                columns: new[] { "VerseId", "BibleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_VerseContents_Bibles_BibleId",
                table: "VerseContents",
                column: "BibleId",
                principalTable: "Bibles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VerseContents_Verses_VerseId",
                table: "VerseContents",
                column: "VerseId",
                principalTable: "Verses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VerseContents_Bibles_BibleId",
                table: "VerseContents");

            migrationBuilder.DropForeignKey(
                name: "FK_VerseContents_Verses_VerseId",
                table: "VerseContents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VerseContents",
                table: "VerseContents");

            migrationBuilder.RenameTable(
                name: "VerseContents",
                newName: "VerseContentEntity");

            migrationBuilder.RenameIndex(
                name: "IX_VerseContents_BibleId",
                table: "VerseContentEntity",
                newName: "IX_VerseContentEntity_BibleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VerseContentEntity",
                table: "VerseContentEntity",
                columns: new[] { "VerseId", "BibleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_VerseContentEntity_Bibles_BibleId",
                table: "VerseContentEntity",
                column: "BibleId",
                principalTable: "Bibles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VerseContentEntity_Verses_VerseId",
                table: "VerseContentEntity",
                column: "VerseId",
                principalTable: "Verses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
