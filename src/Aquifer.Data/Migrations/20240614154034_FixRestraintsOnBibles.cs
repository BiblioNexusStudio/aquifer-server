using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixRestraintsOnBibles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bibles_LanguageId_LanguageDefault",
                table: "Bibles");

            migrationBuilder.CreateIndex(
                name: "IX_Bibles_LanguageId_LanguageDefault",
                table: "Bibles",
                column: "LanguageId",
                unique: true,
                filter: "LanguageDefault = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bibles_LanguageId_LanguageDefault",
                table: "Bibles");

            migrationBuilder.CreateIndex(
                name: "IX_Bibles_LanguageId_LanguageDefault",
                table: "Bibles",
                columns: new[] { "LanguageId", "LanguageDefault" },
                unique: true);
        }
    }
}
