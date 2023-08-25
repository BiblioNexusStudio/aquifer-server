using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.API.Data.Migrations;

/// <inheritdoc />
public partial class MakeSupportingResourceForeignKeysWorkWithEntities : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_SupportingResources_Resources_SupportingResourceId",
            table: "SupportingResources");

        migrationBuilder.DropIndex(
            name: "IX_SupportingResources_SupportingResourceId",
            table: "SupportingResources");

        migrationBuilder.CreateIndex(
            name: "IX_SupportingResources_SupportingResourceId",
            table: "SupportingResources",
            column: "SupportingResourceId");

        migrationBuilder.AddForeignKey(
            name: "FK_SupportingResources_Resources_SupportingResourceId",
            table: "SupportingResources",
            column: "SupportingResourceId",
            principalTable: "Resources",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_SupportingResources_Resources_SupportingResourceId",
            table: "SupportingResources");

        migrationBuilder.DropIndex(
            name: "IX_SupportingResources_SupportingResourceId",
            table: "SupportingResources");

        migrationBuilder.CreateIndex(
            name: "IX_SupportingResources_SupportingResourceId",
            table: "SupportingResources",
            column: "SupportingResourceId",
            unique: true);

        migrationBuilder.AddForeignKey(
            name: "FK_SupportingResources_Resources_SupportingResourceId",
            table: "SupportingResources",
            column: "SupportingResourceId",
            principalTable: "Resources",
            principalColumn: "Id");
    }
}