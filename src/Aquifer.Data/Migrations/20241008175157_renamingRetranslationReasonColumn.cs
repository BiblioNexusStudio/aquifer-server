using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class renamingRetranslationReasonColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReasonForReTranslation",
                table: "ResourceContentVersionMachineTranslations",
                newName: "RetranslationReason");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RetranslationReason",
                table: "ResourceContentVersionMachineTranslations",
                newName: "ReasonForReTranslation");
        }
    }
}
