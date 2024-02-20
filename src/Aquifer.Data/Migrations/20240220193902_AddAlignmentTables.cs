using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAlignmentTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VerseResources_ResourceId",
                table: "VerseResources");

            migrationBuilder.DropIndex(
                name: "IX_Users_CompanyId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_ResourceContentVersionStatusHistory_ChangedByUserId",
                table: "ResourceContentVersionStatusHistory");

            migrationBuilder.DropIndex(
                name: "IX_ResourceContentVersionStatusHistory_ResourceContentVersionId",
                table: "ResourceContentVersionStatusHistory");

            migrationBuilder.DropIndex(
                name: "IX_ResourceContentVersions_AssignedUserId",
                table: "ResourceContentVersions");

            migrationBuilder.DropIndex(
                name: "IX_ResourceContentVersionAssignedUserHistory_AssignedUserId",
                table: "ResourceContentVersionAssignedUserHistory");

            migrationBuilder.DropIndex(
                name: "IX_ResourceContentVersionAssignedUserHistory_ChangedByUserId",
                table: "ResourceContentVersionAssignedUserHistory");

            migrationBuilder.DropIndex(
                name: "IX_ResourceContentVersionAssignedUserHistory_ResourceContentVersionId",
                table: "ResourceContentVersionAssignedUserHistory");

            migrationBuilder.DropIndex(
                name: "IX_ResourceContents_LanguageId",
                table: "ResourceContents");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CompanyId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CompanyLeadUserId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_LanguageId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectManagerUserId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectPlatformId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Passages_EndVerseId",
                table: "Passages");

            migrationBuilder.DropIndex(
                name: "IX_PassageResources_ResourceId",
                table: "PassageResources");

            migrationBuilder.DropIndex(
                name: "IX_AssociatedResources_ResourceId",
                table: "AssociatedResources");

            migrationBuilder.CreateTable(
                name: "BibleVersionWords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BibleId = table.Column<int>(type: "int", nullable: false),
                    WordIdentifier = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibleVersionWords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BibleVersionWords_Bibles_BibleId",
                        column: x => x.BibleId,
                        principalTable: "Bibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GreekNewTestaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreekNewTestaments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GreekWords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StrongNumber = table.Column<int>(type: "int", nullable: true),
                    Lemma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrammarType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sense = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsageCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreekWords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GreekNewTestamentWords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GreekNewTestamentId = table.Column<int>(type: "int", nullable: false),
                    GreekWordId = table.Column<int>(type: "int", nullable: false),
                    WordIdentifier = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GreekNewTestamentWords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GreekNewTestamentWords_GreekNewTestaments_GreekNewTestamentId",
                        column: x => x.GreekNewTestamentId,
                        principalTable: "GreekNewTestaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GreekNewTestamentWords_GreekWords_GreekWordId",
                        column: x => x.GreekWordId,
                        principalTable: "GreekWords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewTestamentAlignments",
                columns: table => new
                {
                    BibleVersionWordId = table.Column<int>(type: "int", nullable: false),
                    GreekNewTestamentWordId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewTestamentAlignments", x => new { x.BibleVersionWordId, x.GreekNewTestamentWordId });
                    table.ForeignKey(
                        name: "FK_NewTestamentAlignments_BibleVersionWords_BibleVersionWordId",
                        column: x => x.BibleVersionWordId,
                        principalTable: "BibleVersionWords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewTestamentAlignments_GreekNewTestamentWords_GreekNewTestamentWordId",
                        column: x => x.GreekNewTestamentWordId,
                        principalTable: "GreekNewTestamentWords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BibleVersionWords_BibleId_WordIdentifier",
                table: "BibleVersionWords",
                columns: new[] { "BibleId", "WordIdentifier" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GreekNewTestamentWords_GreekNewTestamentId_WordIdentifier",
                table: "GreekNewTestamentWords",
                columns: new[] { "GreekNewTestamentId", "WordIdentifier" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewTestamentAlignments");

            migrationBuilder.DropTable(
                name: "BibleVersionWords");

            migrationBuilder.DropTable(
                name: "GreekNewTestamentWords");

            migrationBuilder.DropTable(
                name: "GreekNewTestaments");

            migrationBuilder.DropTable(
                name: "GreekWords");

            migrationBuilder.CreateIndex(
                name: "IX_VerseResources_ResourceId",
                table: "VerseResources",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyId",
                table: "Users",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersionStatusHistory_ChangedByUserId",
                table: "ResourceContentVersionStatusHistory",
                column: "ChangedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersionStatusHistory_ResourceContentVersionId",
                table: "ResourceContentVersionStatusHistory",
                column: "ResourceContentVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersions_AssignedUserId",
                table: "ResourceContentVersions",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersionAssignedUserHistory_AssignedUserId",
                table: "ResourceContentVersionAssignedUserHistory",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersionAssignedUserHistory_ChangedByUserId",
                table: "ResourceContentVersionAssignedUserHistory",
                column: "ChangedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContentVersionAssignedUserHistory_ResourceContentVersionId",
                table: "ResourceContentVersionAssignedUserHistory",
                column: "ResourceContentVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContents_LanguageId",
                table: "ResourceContents",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CompanyId",
                table: "Projects",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CompanyLeadUserId",
                table: "Projects",
                column: "CompanyLeadUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_LanguageId",
                table: "Projects",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectManagerUserId",
                table: "Projects",
                column: "ProjectManagerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectPlatformId",
                table: "Projects",
                column: "ProjectPlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_Passages_EndVerseId",
                table: "Passages",
                column: "EndVerseId");

            migrationBuilder.CreateIndex(
                name: "IX_PassageResources_ResourceId",
                table: "PassageResources",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_AssociatedResources_ResourceId",
                table: "AssociatedResources",
                column: "ResourceId");
        }
    }
}
