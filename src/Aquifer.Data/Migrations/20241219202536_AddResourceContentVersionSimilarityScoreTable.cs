using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddResourceContentVersionSimilarityScoreTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResourceContentVersionSimilarityScores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaseVersionId = table.Column<int>(type: "int", nullable: false),
                    BaseVersionType = table.Column<int>(type: "int", nullable: false),
                    ComparedVersionId = table.Column<int>(type: "int", nullable: false),
                    ComparedVersionType = table.Column<int>(type: "int", nullable: false),
                    SimilarityScore = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceContentVersionSimilarityScores", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResourceContentVersionSimilarityScores");
        }
    }
}
