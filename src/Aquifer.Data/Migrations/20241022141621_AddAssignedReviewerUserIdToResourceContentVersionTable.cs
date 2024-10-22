using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignedReviewerUserIdToResourceContentVersionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedReviewerUserId",
                table: "ResourceContentVersions",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceContentVersions_Users_AssignedReviewerUserId",
                table: "ResourceContentVersions",
                column: "AssignedReviewerUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceContentVersions_Users_AssignedReviewerUserId",
                table: "ResourceContentVersions");

            migrationBuilder.DropColumn(
                name: "AssignedReviewerUserId",
                table: "ResourceContentVersions");
        }
    }
}
