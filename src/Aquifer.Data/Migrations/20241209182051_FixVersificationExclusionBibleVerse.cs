using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixVersificationExclusionBibleVerse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VersificationExclusions_Verses_VerseId",
                table: "VersificationExclusions");

            migrationBuilder.DropColumn(
                name: "VerseId",
                table: "VersificationExclusions");

            migrationBuilder.AddForeignKey(
                name: "FK_VersificationExclusions_Verses_BibleVerseId",
                table: "VersificationExclusions",
                column: "BibleVerseId",
                principalTable: "Verses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VersificationExclusions_Verses_BibleVerseId",
                table: "VersificationExclusions");

            migrationBuilder.AddColumn<int>(
                name: "VerseId",
                table: "VersificationExclusions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_VersificationExclusions_Verses_VerseId",
                table: "VersificationExclusions",
                column: "VerseId",
                principalTable: "Verses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
