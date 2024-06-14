using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLanguageDefaultToBibles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "LanguageDefault",
                table: "Bibles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Bibles_LanguageId_LanguageDefault",
                table: "Bibles",
                columns: new[] { "LanguageId", "LanguageDefault" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bibles_LanguageId_LanguageDefault",
                table: "Bibles");

            migrationBuilder.DropColumn(
                name: "LanguageDefault",
                table: "Bibles");
        }
    }
}
