using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIpAddressDataTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Users_DefaultReviewerUserId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "DefaultReviewerUserId",
                table: "Companies");

            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                table: "ResourceContentRequests",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "IpAddressData",
                columns: table => new
                {
                    IpAddress = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    City = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IpAddressData", x => x.IpAddress);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IpAddressData");

            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                table: "ResourceContentRequests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AddColumn<int>(
                name: "DefaultReviewerUserId",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Users_DefaultReviewerUserId",
                table: "Companies",
                column: "DefaultReviewerUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
