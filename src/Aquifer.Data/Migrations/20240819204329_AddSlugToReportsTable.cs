using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSlugToReportsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Reports",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_Slug",
                table: "Reports",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reports_Slug",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Reports");
        }
    }
}
