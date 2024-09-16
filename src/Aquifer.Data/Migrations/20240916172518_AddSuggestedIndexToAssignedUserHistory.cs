using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSuggestedIndexToAssignedUserHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersionAssignedUserHistory_ChangedByUserId_Created",
                table: "ResourceContentVersionAssignedUserHistory",
                columns: new[] { "ChangedByUserId", "Created" })
                .Annotation("SqlServer:Include", new[] { "ResourceContentVersionId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ResourceContentVersionAssignedUserHistory_ChangedByUserId_Created",
                table: "ResourceContentVersionAssignedUserHistory");
        }
    }
}
