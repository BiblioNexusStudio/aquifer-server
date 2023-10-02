using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUniqueIndexOnResourceContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ResourceContents_ResourceId_LanguageId",
                table: "ResourceContents");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContents_ResourceId_LanguageId_MediaType",
                table: "ResourceContents",
                columns: new[] { "ResourceId", "LanguageId", "MediaType" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ResourceContents_ResourceId_LanguageId_MediaType",
                table: "ResourceContents");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContents_ResourceId_LanguageId",
                table: "ResourceContents",
                columns: new[] { "ResourceId", "LanguageId" },
                unique: true);
        }
    }
}
