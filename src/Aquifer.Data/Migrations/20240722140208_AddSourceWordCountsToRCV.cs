using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSourceWordCountsToRCV : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HadMachineTranslation",
                table: "ResourceContentVersions");

            migrationBuilder.AddColumn<int>(
                name: "SourceWordCount",
                table: "ResourceContentVersions",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceWordCount",
                table: "ResourceContentVersions");

            migrationBuilder.AddColumn<bool>(
                name: "HadMachineTranslation",
                table: "ResourceContentVersions",
                type: "bit",
                nullable: true);
        }
    }
}
