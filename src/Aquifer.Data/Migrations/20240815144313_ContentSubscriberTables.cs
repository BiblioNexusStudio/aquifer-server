using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class ContentSubscriberTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ForMarketing",
                table: "ParentResources",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContentSubscribers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Organization = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    GetNewsletter = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentSubscribers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContentSubscriberLanguages",
                columns: table => new
                {
                    ContentSubscriberId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentSubscriberLanguages", x => new { x.ContentSubscriberId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_ContentSubscriberLanguages_ContentSubscribers_ContentSubscriberId",
                        column: x => x.ContentSubscriberId,
                        principalTable: "ContentSubscribers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentSubscriberLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentSubscriberParentResources",
                columns: table => new
                {
                    ContentSubscriberId = table.Column<int>(type: "int", nullable: false),
                    ParentResourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentSubscriberParentResources", x => new { x.ContentSubscriberId, x.ParentResourceId });
                    table.ForeignKey(
                        name: "FK_ContentSubscriberParentResources_ContentSubscribers_ContentSubscriberId",
                        column: x => x.ContentSubscriberId,
                        principalTable: "ContentSubscribers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentSubscriberParentResources_ParentResources_ParentResourceId",
                        column: x => x.ParentResourceId,
                        principalTable: "ParentResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentSubscriberLanguages");

            migrationBuilder.DropTable(
                name: "ContentSubscriberParentResources");

            migrationBuilder.DropTable(
                name: "ContentSubscribers");

            migrationBuilder.DropColumn(
                name: "ForMarketing",
                table: "ParentResources");
        }
    }
}
