using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameTypeIdToParentResourceId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_ParentResources_TypeId",
                table: "Resources");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Resources",
                newName: "ParentResourceId");

            migrationBuilder.RenameIndex(
                name: "IX_Resources_TypeId_ExternalId",
                table: "Resources",
                newName: "IX_Resources_ParentResourceId_ExternalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_ParentResources_ParentResourceId",
                table: "Resources",
                column: "ParentResourceId",
                principalTable: "ParentResources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_ParentResources_ParentResourceId",
                table: "Resources");

            migrationBuilder.RenameColumn(
                name: "ParentResourceId",
                table: "Resources",
                newName: "TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Resources_ParentResourceId_ExternalId",
                table: "Resources",
                newName: "IX_Resources_TypeId_ExternalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_ParentResources_TypeId",
                table: "Resources",
                column: "TypeId",
                principalTable: "ParentResources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
