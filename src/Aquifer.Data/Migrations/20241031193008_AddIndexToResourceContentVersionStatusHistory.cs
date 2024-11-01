using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexToResourceContentVersionStatusHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersionStatusHistory_Status_ResourceContentVersionId_Created",
                table: "ResourceContentVersionStatusHistory",
                columns: new[] { "Status", "ResourceContentVersionId", "Created" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ResourceContentVersionStatusHistory_Status_ResourceContentVersionId_Created",
                table: "ResourceContentVersionStatusHistory");
        }
    }
}
