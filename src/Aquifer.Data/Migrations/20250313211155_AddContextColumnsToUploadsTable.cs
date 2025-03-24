using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddContextColumnsToUploadsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlobName",
                table: "Uploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Uploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "FileSize",
                table: "Uploads",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "ResourceContentId",
                table: "Uploads",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartedByUserId",
                table: "Uploads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StepNumber",
                table: "Uploads",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Uploads_ResourceContents_ResourceContentId",
                table: "Uploads",
                column: "ResourceContentId",
                principalTable: "ResourceContents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Uploads_Users_StartedByUserId",
                table: "Uploads",
                column: "StartedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uploads_ResourceContents_ResourceContentId",
                table: "Uploads");

            migrationBuilder.DropForeignKey(
                name: "FK_Uploads_Users_StartedByUserId",
                table: "Uploads");

            migrationBuilder.DropColumn(
                name: "BlobName",
                table: "Uploads");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Uploads");

            migrationBuilder.DropColumn(
                name: "FileSize",
                table: "Uploads");

            migrationBuilder.DropColumn(
                name: "ResourceContentId",
                table: "Uploads");

            migrationBuilder.DropColumn(
                name: "StartedByUserId",
                table: "Uploads");

            migrationBuilder.DropColumn(
                name: "StepNumber",
                table: "Uploads");
        }
    }
}
