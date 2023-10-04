using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class SwitchFromResourceTypeEnumToEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupportingResources");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Resources",
                newName: "TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Resources_Type_EnglishLabel",
                table: "Resources",
                newName: "IX_Resources_TypeId_EnglishLabel");

            migrationBuilder.AddColumn<int>(
                name: "ComplexityLevel",
                table: "ResourceTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_ResourceTypes_TypeId",
                table: "Resources",
                column: "TypeId",
                principalTable: "ResourceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_ResourceTypes_TypeId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "ComplexityLevel",
                table: "ResourceTypes");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Resources",
                newName: "Type");

            migrationBuilder.RenameIndex(
                name: "IX_Resources_TypeId_EnglishLabel",
                table: "Resources",
                newName: "IX_Resources_Type_EnglishLabel");

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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupportingResources_Resources_SupportingResourceId",
                        column: x => x.SupportingResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupportingResources_SupportingResourceId",
                table: "SupportingResources",
                column: "SupportingResourceId");
        }
    }
}
