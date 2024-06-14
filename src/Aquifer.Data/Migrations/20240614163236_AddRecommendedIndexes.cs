using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRecommendedIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersionSnapshots_ResourceContentVersionId",
                table: "ResourceContentVersionSnapshots",
                column: "ResourceContentVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersions_AssignedUserId",
                table: "ResourceContentVersions",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersionMachineTranslations_ResourceContentVersionId",
                table: "ResourceContentVersionMachineTranslations",
                column: "ResourceContentVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContents_LanguageId_MediaType",
                table: "ResourceContents",
                columns: new[] { "LanguageId", "MediaType" });

            migrationBuilder.CreateIndex(
                name: "IX_PassageResources_ResourceId",
                table: "PassageResources",
                column: "ResourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ResourceContentVersionSnapshots_ResourceContentVersionId",
                table: "ResourceContentVersionSnapshots");

            migrationBuilder.DropIndex(
                name: "IX_ResourceContentVersions_AssignedUserId",
                table: "ResourceContentVersions");

            migrationBuilder.DropIndex(
                name: "IX_ResourceContentVersionMachineTranslations_ResourceContentVersionId",
                table: "ResourceContentVersionMachineTranslations");

            migrationBuilder.DropIndex(
                name: "IX_ResourceContents_LanguageId_MediaType",
                table: "ResourceContents");

            migrationBuilder.DropIndex(
                name: "IX_PassageResources_ResourceId",
                table: "PassageResources");
        }
    }
}
