using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingIndicesForGreekSensesQuery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_GreekSenseGlosses_GreekSenseId",
                table: "GreekSenseGlosses",
                column: "GreekSenseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GreekSenseGlosses_GreekSenseId",
                table: "GreekSenseGlosses");
        }
    }
}
