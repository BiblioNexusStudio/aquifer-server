using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreSuggestedIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ResourceContentVersions_AssignedUserId",
                table: "ResourceContentVersions");

            migrationBuilder.DropIndex(
                name: "IX_ResourceContents_LanguageId_MediaType",
                table: "ResourceContents");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersions_AssignedUserId",
                table: "ResourceContentVersions",
                column: "AssignedUserId")
                .Annotation("SqlServer:Include", new[] { "ResourceContentId", "SourceWordCount" });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersionAssignedUserHistory_ResourceContentVersionId_AssignedUserId",
                table: "ResourceContentVersionAssignedUserHistory",
                columns: new[] { "ResourceContentVersionId", "AssignedUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContents_LanguageId_MediaType",
                table: "ResourceContents",
                columns: new[] { "LanguageId", "MediaType" })
                .Annotation("SqlServer:Include", new[] { "Created", "ResourceId", "Status" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ResourceContentVersions_AssignedUserId",
                table: "ResourceContentVersions");

            migrationBuilder.DropIndex(
                name: "IX_ResourceContentVersionAssignedUserHistory_ResourceContentVersionId_AssignedUserId",
                table: "ResourceContentVersionAssignedUserHistory");

            migrationBuilder.DropIndex(
                name: "IX_ResourceContents_LanguageId_MediaType",
                table: "ResourceContents");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersions_AssignedUserId",
                table: "ResourceContentVersions",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContents_LanguageId_MediaType",
                table: "ResourceContents",
                columns: new[] { "LanguageId", "MediaType" });
        }
    }
}
