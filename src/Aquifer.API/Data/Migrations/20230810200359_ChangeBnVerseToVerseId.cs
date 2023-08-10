using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBnVerseToVerseId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartBnVerse",
                table: "Passages",
                newName: "StartVerseId");

            migrationBuilder.RenameColumn(
                name: "EndBnVerse",
                table: "Passages",
                newName: "EndVerseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartVerseId",
                table: "Passages",
                newName: "StartBnVerse");

            migrationBuilder.RenameColumn(
                name: "EndVerseId",
                table: "Passages",
                newName: "EndBnVerse");
        }
    }
}
