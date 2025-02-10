using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddResourceContentVersionIdIndexOnResourceContentVersionCommentThreadEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersionCommentThreads_ResourceContentVersionId",
                table: "ResourceContentVersionCommentThreads",
                column: "ResourceContentVersionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ResourceContentVersionCommentThreads_ResourceContentVersionId",
                table: "ResourceContentVersionCommentThreads");
        }
    }
}
