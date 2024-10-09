using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexesToHelpWithUserActivityReporting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersionStatusHistory_ChangedByUserId_Created",
                table: "ResourceContentVersionStatusHistory",
                columns: new[] { "ChangedByUserId", "Created" });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersionEditTimes_UserId_Created",
                table: "ResourceContentVersionEditTimes",
                columns: new[] { "UserId", "Created" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ResourceContentVersionStatusHistory_ChangedByUserId_Created",
                table: "ResourceContentVersionStatusHistory");

            migrationBuilder.DropIndex(
                name: "IX_ResourceContentVersionEditTimes_UserId_Created",
                table: "ResourceContentVersionEditTimes");
        }
    }
}
