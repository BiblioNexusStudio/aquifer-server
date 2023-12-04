using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class CopyContentToVersionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET COMMAND_TIMEOUT = 300"); // 5 minutes

            migrationBuilder.Sql(@"
                INSERT INTO [dbo].[ResourceContentVersions] (
                    [ResourceContentId],
                    [Version],
                    [IsDraft],
                    [IsPublished],
                    [Content],
                    [ContentSize],
                    [DisplayName],
                    [Created],
                    [Updated]
                )
                SELECT
                    [Id],
                    1 As Version,
                    0 As IsDraft,
                    [Published],
                    [Content],
                    [ContentSize],
                    [DisplayName],
                    [Created],
                    [Updated]
                FROM [dbo].[ResourceContents];
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
