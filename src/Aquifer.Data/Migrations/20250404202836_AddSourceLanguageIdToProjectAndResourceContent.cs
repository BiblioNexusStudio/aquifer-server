using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSourceLanguageIdToProjectAndResourceContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceContents_Languages_LanguageId",
                table: "ResourceContents");

            migrationBuilder.DropIndex(
                name: "IX_ResourceContents_LanguageId_MediaType_Status",
                table: "ResourceContents");

            migrationBuilder.DropIndex(
                name: "IX_ResourceContents_Status",
                table: "ResourceContents");

            migrationBuilder.AddColumn<int>(
                name: "SourceLanguageId",
                table: "ResourceContents",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "SourceLanguageId",
                table: "Projects",
                type: "int",
                nullable: true,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContents_LanguageId_MediaType_Status",
                table: "ResourceContents",
                columns: new[] { "LanguageId", "MediaType", "Status" })
                .Annotation("SqlServer:Include", new[] { "SourceLanguageId", "Created", "ResourceId", "Updated", "ContentUpdated" });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContents_Status",
                table: "ResourceContents",
                column: "Status")
                .Annotation("SqlServer:Include", new[] { "ContentUpdated", "SourceLanguageId", "LanguageId", "ResourceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Languages_SourceLanguageId",
                table: "Projects",
                column: "SourceLanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceContents_Languages_LanguageId",
                table: "ResourceContents",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceContents_Languages_SourceLanguageId",
                table: "ResourceContents",
                column: "SourceLanguageId",
                principalTable: "Languages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Languages_SourceLanguageId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceContents_Languages_LanguageId",
                table: "ResourceContents");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceContents_Languages_SourceLanguageId",
                table: "ResourceContents");

            migrationBuilder.DropIndex(
                name: "IX_ResourceContents_LanguageId_MediaType_Status",
                table: "ResourceContents");

            migrationBuilder.DropIndex(
                name: "IX_ResourceContents_Status",
                table: "ResourceContents");

            migrationBuilder.DropColumn(
                name: "SourceLanguageId",
                table: "ResourceContents");

            migrationBuilder.DropColumn(
                name: "SourceLanguageId",
                table: "Projects");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContents_LanguageId_MediaType_Status",
                table: "ResourceContents",
                columns: new[] { "LanguageId", "MediaType", "Status" })
                .Annotation("SqlServer:Include", new[] { "Created", "ResourceId", "Updated", "ContentUpdated" });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContents_Status",
                table: "ResourceContents",
                column: "Status")
                .Annotation("SqlServer:Include", new[] { "ContentUpdated", "LanguageId", "ResourceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceContents_Languages_LanguageId",
                table: "ResourceContents",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
