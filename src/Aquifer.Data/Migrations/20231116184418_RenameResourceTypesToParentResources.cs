using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameResourceTypesToParentResources : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_ResourceTypes_TypeId",
                table: "Resources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResourceTypes",
                table: "ResourceTypes");

            migrationBuilder.RenameTable(
                name: "ResourceTypes",
                newName: "ParentResources");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParentResources",
                table: "ParentResources",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_ParentResources_TypeId",
                table: "Resources",
                column: "TypeId",
                principalTable: "ParentResources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_ParentResources_TypeId",
                table: "Resources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParentResources",
                table: "ParentResources");

            migrationBuilder.RenameTable(
                name: "ParentResources",
                newName: "ResourceTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResourceTypes",
                table: "ResourceTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_ResourceTypes_TypeId",
                table: "Resources",
                column: "TypeId",
                principalTable: "ResourceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
