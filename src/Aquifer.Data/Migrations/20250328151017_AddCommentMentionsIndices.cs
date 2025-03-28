using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentMentionsIndices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CommentMentions_CommentId_UserId",
                table: "CommentMentions",
                columns: new[] { "CommentId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_CommentMentions_UserId_CommentId",
                table: "CommentMentions",
                columns: new[] { "UserId", "CommentId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CommentMentions_CommentId_UserId",
                table: "CommentMentions");

            migrationBuilder.DropIndex(
                name: "IX_CommentMentions_UserId_CommentId",
                table: "CommentMentions");
        }
    }
}
