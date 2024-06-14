using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLanguageDefaultToBibles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GreekSenses_StrongNumbers_StrongNumberId",
                table: "GreekSenses");

            migrationBuilder.AlterColumn<int>(
                name: "StrongNumberId",
                table: "GreekSenses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "LanguageDefault",
                table: "Bibles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Bibles_LanguageId_LanguageDefault",
                table: "Bibles",
                column: "LanguageId",
                unique: true,
                filter: "LanguageDefault = 1");

            migrationBuilder.AddForeignKey(
                name: "FK_GreekSenses_StrongNumbers_StrongNumberId",
                table: "GreekSenses",
                column: "StrongNumberId",
                principalTable: "StrongNumbers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GreekSenses_StrongNumbers_StrongNumberId",
                table: "GreekSenses");

            migrationBuilder.DropIndex(
                name: "IX_Bibles_LanguageId_LanguageDefault",
                table: "Bibles");

            migrationBuilder.DropColumn(
                name: "LanguageDefault",
                table: "Bibles");

            migrationBuilder.AlterColumn<int>(
                name: "StrongNumberId",
                table: "GreekSenses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GreekSenses_StrongNumbers_StrongNumberId",
                table: "GreekSenses",
                column: "StrongNumberId",
                principalTable: "StrongNumbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
