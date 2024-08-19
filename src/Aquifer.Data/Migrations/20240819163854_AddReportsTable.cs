using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddReportsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE FUNCTION dbo.StoredProcedureExists(@procedureName NVARCHAR(MAX))
                RETURNS BIT
                AS
                BEGIN
                    RETURN CASE WHEN EXISTS (
                        SELECT 1
                        FROM sys.procedures
                        WHERE name = @procedureName
                    ) THEN 1 ELSE 0 END
                END
            ");

            migrationBuilder.Sql(@"
                CREATE FUNCTION dbo.StoredProcedureMatchesNamingConvention(@procedureName NVARCHAR(MAX))
                RETURNS BIT
                AS
                BEGIN
                    RETURN CASE WHEN @procedureName LIKE 'DynamicReport%' THEN 1 ELSE 0 END
                END
            ");

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoredProcedureName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    AcceptsDateRange = table.Column<bool>(type: "bit", nullable: false),
                    AcceptsLanguage = table.Column<bool>(type: "bit", nullable: false),
                    AcceptsParentResource = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.CheckConstraint("CK_StoredProcedureExists", "dbo.StoredProcedureExists(StoredProcedureName) = 1");
                    table.CheckConstraint("CK_StoredProcedureNamingConvention", "dbo.StoredProcedureMatchesNamingConvention(StoredProcedureName) = 1");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS dbo.StoredProcedureExists");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS dbo.StoredProcedureMatchesNamingConvention");
        }
    }
}