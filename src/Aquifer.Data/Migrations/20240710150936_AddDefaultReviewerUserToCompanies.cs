using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultReviewerUserToCompanies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefaultReviewerUserId",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Users_DefaultReviewerUserId",
                table: "Companies",
                column: "DefaultReviewerUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Users_DefaultReviewerUserId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "DefaultReviewerUserId",
                table: "Companies");
        }
    }
}
