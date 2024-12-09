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
            migrationBuilder.AddForeignKey(
                name: "FK_VersificationExclusions_Bibles_BibleId",
                table: "VersificationExclusions",
                column: "BibleId",
                principalTable: "Bibles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VersificationExclusions_Verses_VerseId",
                table: "VersificationExclusions",
                column: "VerseId",
                principalTable: "Verses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VersificationMappings_Bibles_BibleId",
                table: "VersificationMappings",
                column: "BibleId",
                principalTable: "Bibles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VersificationMappings_Verses_BaseVerseId",
                table: "VersificationMappings",
                column: "BaseVerseId",
                principalTable: "Verses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VersificationMappings_Verses_BibleVerseId",
                table: "VersificationMappings",
                column: "BibleVerseId",
                principalTable: "Verses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VersificationExclusions_Bibles_BibleId",
                table: "VersificationExclusions");

            migrationBuilder.DropForeignKey(
                name: "FK_VersificationExclusions_Verses_VerseId",
                table: "VersificationExclusions");

            migrationBuilder.DropForeignKey(
                name: "FK_VersificationMappings_Bibles_BibleId",
                table: "VersificationMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_VersificationMappings_Verses_BaseVerseId",
                table: "VersificationMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_VersificationMappings_Verses_BibleVerseId",
                table: "VersificationMappings");
        }
    }
}
