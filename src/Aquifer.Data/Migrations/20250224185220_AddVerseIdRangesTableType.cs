using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddVerseIdRangesTableType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                CREATE TYPE [dbo].[VerseIdRanges] AS TABLE
                (
                    StartVerseId INT,
                    EndVerseId INT
                );
                GRANT EXECUTE ON TYPE::[dbo].[VerseIdRanges] TO public;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                DROP TYPE [dbo].[VerseIdRanges]
                """);
        }
    }
}
