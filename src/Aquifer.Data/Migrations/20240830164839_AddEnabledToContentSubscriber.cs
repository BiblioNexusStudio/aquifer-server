using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEnabledToContentSubscriber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "ContentSubscribers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "ContentSubscribers");
        }
    }
}
