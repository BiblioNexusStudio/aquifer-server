using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAlignmentWordGrouping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewTestamentAlignments_BibleVersionWords_BibleVersionWordId",
                table: "NewTestamentAlignments");

            migrationBuilder.DropForeignKey(
                name: "FK_NewTestamentAlignments_GreekNewTestamentWords_GreekNewTestamentWordId",
                table: "NewTestamentAlignments");

            migrationBuilder.RenameColumn(
                name: "GreekNewTestamentWordId",
                table: "NewTestamentAlignments",
                newName: "GreekNewTestamentWordGroupId");

            migrationBuilder.RenameColumn(
                name: "BibleVersionWordId",
                table: "NewTestamentAlignments",
                newName: "BibleVersionWordGroupId");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "GreekNewTestamentWords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "BibleVersionWords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_NewTestamentAlignments_BibleVersionWordGroupId",
                table: "NewTestamentAlignments",
                column: "BibleVersionWordGroupId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_NewTestamentAlignments_GreekNewTestamentWordGroupId",
                table: "NewTestamentAlignments",
                column: "GreekNewTestamentWordGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_BibleVersionWords_NewTestamentAlignments_GroupId",
                table: "BibleVersionWords",
                column: "GroupId",
                principalTable: "NewTestamentAlignments",
                principalColumn: "BibleVersionWordGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_GreekNewTestamentWords_NewTestamentAlignments_GroupId",
                table: "GreekNewTestamentWords",
                column: "GroupId",
                principalTable: "NewTestamentAlignments",
                principalColumn: "GreekNewTestamentWordGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BibleVersionWords_NewTestamentAlignments_GroupId",
                table: "BibleVersionWords");

            migrationBuilder.DropForeignKey(
                name: "FK_GreekNewTestamentWords_NewTestamentAlignments_GroupId",
                table: "GreekNewTestamentWords");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_NewTestamentAlignments_BibleVersionWordGroupId",
                table: "NewTestamentAlignments");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_NewTestamentAlignments_GreekNewTestamentWordGroupId",
                table: "NewTestamentAlignments");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "GreekNewTestamentWords");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "BibleVersionWords");

            migrationBuilder.RenameColumn(
                name: "GreekNewTestamentWordGroupId",
                table: "NewTestamentAlignments",
                newName: "GreekNewTestamentWordId");

            migrationBuilder.RenameColumn(
                name: "BibleVersionWordGroupId",
                table: "NewTestamentAlignments",
                newName: "BibleVersionWordId");

            migrationBuilder.AddForeignKey(
                name: "FK_NewTestamentAlignments_BibleVersionWords_BibleVersionWordId",
                table: "NewTestamentAlignments",
                column: "BibleVersionWordId",
                principalTable: "BibleVersionWords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewTestamentAlignments_GreekNewTestamentWords_GreekNewTestamentWordId",
                table: "NewTestamentAlignments",
                column: "GreekNewTestamentWordId",
                principalTable: "GreekNewTestamentWords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
