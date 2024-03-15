using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddResolvedByUserToThreads : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResolvedByUserId",
                table: "CommentThreads",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentThreads_Users_ResolvedByUserId",
                table: "CommentThreads",
                column: "ResolvedByUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentThreads_Users_ResolvedByUserId",
                table: "CommentThreads");

            migrationBuilder.DropColumn(
                name: "ResolvedByUserId",
                table: "CommentThreads");
        }
    }
}
