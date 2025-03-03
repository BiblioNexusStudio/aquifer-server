using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLanguageIdIndexToIncludeMoreProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ResourceContents_LanguageId_MediaType",
                table: "ResourceContents");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContents_LanguageId_MediaType_Status",
                table: "ResourceContents",
                columns: new[] { "LanguageId", "MediaType", "Status" })
                .Annotation("SqlServer:Include", new[] { "Created", "ResourceId", "Updated", "ContentUpdated" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ResourceContents_LanguageId_MediaType_Status",
                table: "ResourceContents");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContents_LanguageId_MediaType",
                table: "ResourceContents",
                columns: new[] { "LanguageId", "MediaType" })
                .Annotation("SqlServer:Include", new[] { "Created", "ResourceId", "Status" });
        }
    }
}
