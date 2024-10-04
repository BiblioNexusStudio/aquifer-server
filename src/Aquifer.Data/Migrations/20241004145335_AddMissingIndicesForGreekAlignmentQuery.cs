using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingIndicesForGreekAlignmentQuery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BibleVersionWordGroupWords_BibleVersionWordId",
                table: "BibleVersionWordGroupWords",
                column: "BibleVersionWordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BibleVersionWordGroupWords_BibleVersionWordId",
                table: "BibleVersionWordGroupWords");
        }
    }
}
