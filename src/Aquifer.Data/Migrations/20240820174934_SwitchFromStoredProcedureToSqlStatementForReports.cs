using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.Data.Migrations
{
    /// <inheritdoc />
    public partial class SwitchFromStoredProcedureToSqlStatementForReports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_StoredProcedureExists",
                table: "Reports");

            migrationBuilder.DropCheckConstraint(
                name: "CK_StoredProcedureNamingConvention",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "StoredProcedureName",
                table: "Reports",
                newName: "SqlStatement");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS dbo.StoredProcedureExists");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS dbo.StoredProcedureMatchesNamingConvention");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SqlStatement",
                table: "Reports",
                newName: "StoredProcedureName");

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

            migrationBuilder.AddCheckConstraint(
                name: "CK_StoredProcedureExists",
                table: "Reports",
                sql: "dbo.StoredProcedureExists(StoredProcedureName) = 1");

            migrationBuilder.AddCheckConstraint(
                name: "CK_StoredProcedureNamingConvention",
                table: "Reports",
                sql: "dbo.StoredProcedureMatchesNamingConvention(StoredProcedureName) = 1");
        }
    }
}