using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EnglishLabel",
                table: "Resources",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_Type_MediaType_EnglishLabel",
                table: "Resources",
                columns: new[] { "Type", "MediaType", "EnglishLabel" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContents_ResourceId_LanguageId",
                table: "ResourceContents",
                columns: new[] { "ResourceId", "LanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Passages_EndVerseId",
                table: "Passages",
                column: "EndVerseId");

            migrationBuilder.CreateIndex(
                name: "IX_Passages_StartVerseId_EndVerseId",
                table: "Passages",
                columns: new[] { "StartVerseId", "EndVerseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Languages_ISO6393Code",
                table: "Languages",
                column: "ISO6393Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Passages_Verses_EndVerseId",
                table: "Passages",
                column: "EndVerseId",
                principalTable: "Verses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Passages_Verses_StartVerseId",
                table: "Passages",
                column: "StartVerseId",
                principalTable: "Verses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passages_Verses_EndVerseId",
                table: "Passages");

            migrationBuilder.DropForeignKey(
                name: "FK_Passages_Verses_StartVerseId",
                table: "Passages");

            migrationBuilder.DropIndex(
                name: "IX_Resources_Type_MediaType_EnglishLabel",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_ResourceContents_ResourceId_LanguageId",
                table: "ResourceContents");

            migrationBuilder.DropIndex(
                name: "IX_Passages_EndVerseId",
                table: "Passages");

            migrationBuilder.DropIndex(
                name: "IX_Passages_StartVerseId_EndVerseId",
                table: "Passages");

            migrationBuilder.DropIndex(
                name: "IX_Languages_ISO6393Code",
                table: "Languages");

            migrationBuilder.AlterColumn<string>(
                name: "EnglishLabel",
                table: "Resources",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
