using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupportingResources",
                columns: table => new
                {
                    ParentResourceId = table.Column<int>(type: "int", nullable: false),
                    SupportingResourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportingResources", x => new { x.ParentResourceId, x.SupportingResourceId });
                    table.ForeignKey(
                        name: "FK_SupportingResources_Resources_ParentResourceId",
                        column: x => x.ParentResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SupportingResources_Resources_SupportingResourceId",
                        column: x => x.SupportingResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupportingResources_SupportingResourceId",
                table: "SupportingResources",
                column: "SupportingResourceId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupportingResources");
        }
    }
}
