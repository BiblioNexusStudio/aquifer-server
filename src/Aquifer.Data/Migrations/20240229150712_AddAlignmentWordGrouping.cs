using System;
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
                name: "NewTestamentAlignmentBibleVersionWordGroupId",
                table: "GreekNewTestamentWords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NewTestamentAlignmentGreekNewTestamentWordGroupId",
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

            migrationBuilder.AddColumn<int>(
                name: "NewTestamentAlignmentBibleVersionWordGroupId",
                table: "BibleVersionWords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NewTestamentAlignmentGreekNewTestamentWordGroupId",
                table: "BibleVersionWords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BibleVersionWordGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    BibleVersionWordId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibleVersionWordGroups", x => new { x.Id, x.BibleVersionWordId });
                });

            migrationBuilder.CreateTable(
                name: "GreekNewTestamentWordGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GreekNewTestamentWordId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreekNewTestamentWordGroups", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_BibleVersionWords_NewTestamentAlignments_NewTestamentAlignmentBibleVersionWordGroupId_NewTestamentAlignmentGreekNewTestament~",
                table: "BibleVersionWords",
                columns: new[] { "NewTestamentAlignmentBibleVersionWordGroupId", "NewTestamentAlignmentGreekNewTestamentWordGroupId" },
                principalTable: "NewTestamentAlignments",
                principalColumns: new[] { "BibleVersionWordGroupId", "GreekNewTestamentWordGroupId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GreekNewTestamentWords_NewTestamentAlignments_NewTestamentAlignmentBibleVersionWordGroupId_NewTestamentAlignmentGreekNewTest~",
                table: "GreekNewTestamentWords",
                columns: new[] { "NewTestamentAlignmentBibleVersionWordGroupId", "NewTestamentAlignmentGreekNewTestamentWordGroupId" },
                principalTable: "NewTestamentAlignments",
                principalColumns: new[] { "BibleVersionWordGroupId", "GreekNewTestamentWordGroupId" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BibleVersionWords_NewTestamentAlignments_NewTestamentAlignmentBibleVersionWordGroupId_NewTestamentAlignmentGreekNewTestament~",
                table: "BibleVersionWords");

            migrationBuilder.DropForeignKey(
                name: "FK_GreekNewTestamentWords_NewTestamentAlignments_NewTestamentAlignmentBibleVersionWordGroupId_NewTestamentAlignmentGreekNewTest~",
                table: "GreekNewTestamentWords");

            migrationBuilder.DropTable(
                name: "BibleVersionWordGroups");

            migrationBuilder.DropTable(
                name: "GreekNewTestamentWordGroups");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "GreekNewTestamentWords");

            migrationBuilder.DropColumn(
                name: "NewTestamentAlignmentBibleVersionWordGroupId",
                table: "GreekNewTestamentWords");

            migrationBuilder.DropColumn(
                name: "NewTestamentAlignmentGreekNewTestamentWordGroupId",
                table: "GreekNewTestamentWords");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "BibleVersionWords");

            migrationBuilder.DropColumn(
                name: "NewTestamentAlignmentBibleVersionWordGroupId",
                table: "BibleVersionWords");

            migrationBuilder.DropColumn(
                name: "NewTestamentAlignmentGreekNewTestamentWordGroupId",
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
