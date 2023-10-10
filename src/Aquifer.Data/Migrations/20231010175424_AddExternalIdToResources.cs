using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddExternalIdToResources : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Resources_TypeId_EnglishLabel",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Resources");

            migrationBuilder.AlterColumn<string>(
                name: "EnglishLabel",
                table: "Resources",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "Resources",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_TypeId_ExternalId",
                table: "Resources",
                columns: new[] { "TypeId", "ExternalId" },
                unique: true,
                filter: "[ExternalId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Resources_TypeId_ExternalId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Resources");

            migrationBuilder.AlterColumn<string>(
                name: "EnglishLabel",
                table: "Resources",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Resources",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_TypeId_EnglishLabel",
                table: "Resources",
                columns: new[] { "TypeId", "EnglishLabel" },
                unique: true);
        }
    }
}
