using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixBibleRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Verses_Bibles_BibleEntityId",
                table: "Verses");

            migrationBuilder.DropIndex(
                name: "IX_Verses_BibleEntityId",
                table: "Verses");

            migrationBuilder.DropColumn(
                name: "BibleEntityId",
                table: "Verses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BibleEntityId",
                table: "Verses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Verses_BibleEntityId",
                table: "Verses",
                column: "BibleEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Verses_Bibles_BibleEntityId",
                table: "Verses",
                column: "BibleEntityId",
                principalTable: "Bibles",
                principalColumn: "Id");
        }
    }
}
