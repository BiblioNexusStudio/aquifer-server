using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSuggestedIndexesForAssociatedResourcesAndResources : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Resources_ExternalId",
                table: "Resources",
                column: "ExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_AssociatedResources_ResourceId",
                table: "AssociatedResources",
                column: "ResourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Resources_ExternalId",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_AssociatedResources_ResourceId",
                table: "AssociatedResources");
        }
    }
}
