﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class GreekSenseStrongNumberNullable : Migration
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
