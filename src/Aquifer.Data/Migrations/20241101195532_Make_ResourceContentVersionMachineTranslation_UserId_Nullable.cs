using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class Make_ResourceContentVersionMachineTranslation_UserId_Nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceContentVersionMachineTranslations_Users_UserId",
                table: "ResourceContentVersionMachineTranslations");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ResourceContentVersionMachineTranslations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceContentVersionMachineTranslations_Users_UserId",
                table: "ResourceContentVersionMachineTranslations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceContentVersionMachineTranslations_Users_UserId",
                table: "ResourceContentVersionMachineTranslations");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ResourceContentVersionMachineTranslations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceContentVersionMachineTranslations_Users_UserId",
                table: "ResourceContentVersionMachineTranslations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
