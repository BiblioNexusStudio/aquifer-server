using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGreekSenseTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GreekSenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefinitionShort = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entrycode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Domain = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubDomain = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GreekLemmaId = table.Column<int>(type: "int", nullable: false),
                    StrongNumberId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreekSenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GreekSenses_GreekLemmas_GreekLemmaId",
                        column: x => x.GreekLemmaId,
                        principalTable: "GreekLemmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GreekSenses_StrongNumbers_StrongNumberId",
                        column: x => x.StrongNumberId,
                        principalTable: "StrongNumbers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GreekNewTestamentWordSenses",
                columns: table => new
                {
                    GreekNewTestamentWordId = table.Column<int>(type: "int", nullable: false),
                    GreekSenseId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreekNewTestamentWordSenses", x => new { x.GreekNewTestamentWordId, x.GreekSenseId });
                    table.ForeignKey(
                        name: "FK_GreekNewTestamentWordSenses_GreekNewTestamentWords_GreekNewTestamentWordId",
                        column: x => x.GreekNewTestamentWordId,
                        principalTable: "GreekNewTestamentWords",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GreekNewTestamentWordSenses_GreekSenses_GreekSenseId",
                        column: x => x.GreekSenseId,
                        principalTable: "GreekSenses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GreekSenseGlosses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GreekSenseId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreekSenseGlosses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GreekSenseGlosses_GreekSenses_GreekSenseId",
                        column: x => x.GreekSenseId,
                        principalTable: "GreekSenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GreekNewTestamentWordSenses");

            migrationBuilder.DropTable(
                name: "GreekSenseGlosses");

            migrationBuilder.DropTable(
                name: "GreekSenses");
        }
    }
}
