using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIpDataForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_ResourceContentRequests_IpAddressData_IpAddress",
                table: "ResourceContentRequests",
                column: "IpAddress",
                principalTable: "IpAddressData",
                principalColumn: "IpAddress",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceContentRequests_IpAddressData_IpAddress",
                table: "ResourceContentRequests");
        }
    }
}
