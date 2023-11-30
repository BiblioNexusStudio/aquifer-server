using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameAssignmentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserResourceContentAssignments_ResourceContents_ResourceContentId",
                table: "UserResourceContentAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_UserResourceContentAssignments_Users_AssignedByUserId",
                table: "UserResourceContentAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_UserResourceContentAssignments_Users_AssignedUserId",
                table: "UserResourceContentAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserResourceContentAssignments",
                table: "UserResourceContentAssignments");

            migrationBuilder.RenameTable(
                name: "UserResourceContentAssignments",
                newName: "ResourceContentUserAssignments");

            migrationBuilder.RenameIndex(
                name: "IX_UserResourceContentAssignments_ResourceContentId",
                table: "ResourceContentUserAssignments",
                newName: "IX_ResourceContentUserAssignments_ResourceContentId");

            migrationBuilder.RenameIndex(
                name: "IX_UserResourceContentAssignments_AssignedUserId",
                table: "ResourceContentUserAssignments",
                newName: "IX_ResourceContentUserAssignments_AssignedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserResourceContentAssignments_AssignedByUserId",
                table: "ResourceContentUserAssignments",
                newName: "IX_ResourceContentUserAssignments_AssignedByUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResourceContentUserAssignments",
                table: "ResourceContentUserAssignments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceContentUserAssignments_ResourceContents_ResourceContentId",
                table: "ResourceContentUserAssignments",
                column: "ResourceContentId",
                principalTable: "ResourceContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceContentUserAssignments_Users_AssignedByUserId",
                table: "ResourceContentUserAssignments",
                column: "AssignedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceContentUserAssignments_Users_AssignedUserId",
                table: "ResourceContentUserAssignments",
                column: "AssignedUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceContentUserAssignments_ResourceContents_ResourceContentId",
                table: "ResourceContentUserAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceContentUserAssignments_Users_AssignedByUserId",
                table: "ResourceContentUserAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceContentUserAssignments_Users_AssignedUserId",
                table: "ResourceContentUserAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResourceContentUserAssignments",
                table: "ResourceContentUserAssignments");

            migrationBuilder.RenameTable(
                name: "ResourceContentUserAssignments",
                newName: "UserResourceContentAssignments");

            migrationBuilder.RenameIndex(
                name: "IX_ResourceContentUserAssignments_ResourceContentId",
                table: "UserResourceContentAssignments",
                newName: "IX_UserResourceContentAssignments_ResourceContentId");

            migrationBuilder.RenameIndex(
                name: "IX_ResourceContentUserAssignments_AssignedUserId",
                table: "UserResourceContentAssignments",
                newName: "IX_UserResourceContentAssignments_AssignedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ResourceContentUserAssignments_AssignedByUserId",
                table: "UserResourceContentAssignments",
                newName: "IX_UserResourceContentAssignments_AssignedByUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserResourceContentAssignments",
                table: "UserResourceContentAssignments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserResourceContentAssignments_ResourceContents_ResourceContentId",
                table: "UserResourceContentAssignments",
                column: "ResourceContentId",
                principalTable: "ResourceContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserResourceContentAssignments_Users_AssignedByUserId",
                table: "UserResourceContentAssignments",
                column: "AssignedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserResourceContentAssignments_Users_AssignedUserId",
                table: "UserResourceContentAssignments",
                column: "AssignedUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
