using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAlignmentsVersionTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Lemma",
                table: "GreekWords");

            migrationBuilder.DropColumn(
                name: "Sense",
                table: "GreekWords");

            migrationBuilder.DropColumn(
                name: "StrongNumber",
                table: "GreekWords");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "GreekNewTestamentWords");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "BibleVersionWords");

            migrationBuilder.RenameColumn(
                name: "Word",
                table: "GreekWords",
                newName: "Text");

            migrationBuilder.AddColumn<int>(
                name: "GreekLemmaId",
                table: "GreekWords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPunctuation",
                table: "BibleVersionWords",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "BibleVersionWordGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibleVersionWordGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GreekNewTestamentWordGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreekNewTestamentWordGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StrongNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrongNumbers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BibleVersionWordGroupWords",
                columns: table => new
                {
                    BibleVersionWordGroupId = table.Column<int>(type: "int", nullable: false),
                    BibleVersionWordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibleVersionWordGroupWords", x => new { x.BibleVersionWordGroupId, x.BibleVersionWordId });
                    table.ForeignKey(
                        name: "FK_BibleVersionWordGroupWords_BibleVersionWordGroups_BibleVersionWordGroupId",
                        column: x => x.BibleVersionWordGroupId,
                        principalTable: "BibleVersionWordGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BibleVersionWordGroupWords_BibleVersionWords_BibleVersionWordId",
                        column: x => x.BibleVersionWordId,
                        principalTable: "BibleVersionWords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GreekNewTestamentWordGroupWords",
                columns: table => new
                {
                    GreekNewTestamentWordGroupId = table.Column<int>(type: "int", nullable: false),
                    GreekNewTestamentWordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreekNewTestamentWordGroupWords", x => new { x.GreekNewTestamentWordGroupId, x.GreekNewTestamentWordId });
                    table.ForeignKey(
                        name: "FK_GreekNewTestamentWordGroupWords_GreekNewTestamentWordGroups_GreekNewTestamentWordGroupId",
                        column: x => x.GreekNewTestamentWordGroupId,
                        principalTable: "GreekNewTestamentWordGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GreekNewTestamentWordGroupWords_GreekNewTestamentWords_GreekNewTestamentWordId",
                        column: x => x.GreekNewTestamentWordId,
                        principalTable: "GreekNewTestamentWords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GreekLemmas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StrongNumberId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreekLemmas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GreekLemmas_StrongNumbers_StrongNumberId",
                        column: x => x.StrongNumberId,
                        principalTable: "StrongNumbers",
                        principalColumn: "Id");
                });

            migrationBuilder.AddForeignKey(
                name: "FK_GreekWords_GreekLemmas_GreekLemmaId",
                table: "GreekWords",
                column: "GreekLemmaId",
                principalTable: "GreekLemmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewTestamentAlignments_BibleVersionWordGroups_BibleVersionWordGroupId",
                table: "NewTestamentAlignments",
                column: "BibleVersionWordGroupId",
                principalTable: "BibleVersionWordGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewTestamentAlignments_GreekNewTestamentWordGroups_GreekNewTestamentWordGroupId",
                table: "NewTestamentAlignments",
                column: "GreekNewTestamentWordGroupId",
                principalTable: "GreekNewTestamentWordGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GreekWords_GreekLemmas_GreekLemmaId",
                table: "GreekWords");

            migrationBuilder.DropForeignKey(
                name: "FK_NewTestamentAlignments_BibleVersionWordGroups_BibleVersionWordGroupId",
                table: "NewTestamentAlignments");

            migrationBuilder.DropForeignKey(
                name: "FK_NewTestamentAlignments_GreekNewTestamentWordGroups_GreekNewTestamentWordGroupId",
                table: "NewTestamentAlignments");

            migrationBuilder.DropTable(
                name: "BibleVersionWordGroupWords");

            migrationBuilder.DropTable(
                name: "GreekLemmas");

            migrationBuilder.DropTable(
                name: "GreekNewTestamentWordGroupWords");

            migrationBuilder.DropTable(
                name: "BibleVersionWordGroups");

            migrationBuilder.DropTable(
                name: "StrongNumbers");

            migrationBuilder.DropTable(
                name: "GreekNewTestamentWordGroups");

            migrationBuilder.DropColumn(
                name: "GreekLemmaId",
                table: "GreekWords");

            migrationBuilder.DropColumn(
                name: "IsPunctuation",
                table: "BibleVersionWords");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "GreekWords",
                newName: "Word");

            migrationBuilder.AddColumn<string>(
                name: "Lemma",
                table: "GreekWords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sense",
                table: "GreekWords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StrongNumber",
                table: "GreekWords",
                type: "nvarchar(max)",
                nullable: true);

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
    }
}
