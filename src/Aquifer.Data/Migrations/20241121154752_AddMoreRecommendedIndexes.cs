using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreRecommendedIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersionAssignedUserHistory_Created",
                table: "ResourceContentVersionAssignedUserHistory",
                column: "Created")
                .Annotation("SqlServer:Include", new[] { "AssignedUserId", "ResourceContentVersionId" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ThreadId",
                table: "Comments",
                column: "ThreadId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ResourceContentVersionAssignedUserHistory_Created",
                table: "ResourceContentVersionAssignedUserHistory");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ThreadId",
                table: "Comments");
        }
    }
}
