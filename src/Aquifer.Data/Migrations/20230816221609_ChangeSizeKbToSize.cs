using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSizeKbToSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContentSizeKb",
                table: "ResourceContents",
                newName: "ContentSize");

            migrationBuilder.RenameColumn(
                name: "TextSizeKb",
                table: "BibleBookContents",
                newName: "TextSize");

            migrationBuilder.RenameColumn(
                name: "AudioSizeKb",
                table: "BibleBookContents",
                newName: "AudioSize");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContentSize",
                table: "ResourceContents",
                newName: "ContentSizeKb");

            migrationBuilder.RenameColumn(
                name: "TextSize",
                table: "BibleBookContents",
                newName: "TextSizeKb");

            migrationBuilder.RenameColumn(
                name: "AudioSize",
                table: "BibleBookContents",
                newName: "AudioSizeKb");
        }
    }
}
