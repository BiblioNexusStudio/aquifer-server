using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aquifer.API.Data.Migrations;

/// <inheritdoc />
public partial class Update4 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "Updated",
            table: "Passages",
            type: "datetime2",
            nullable: false,
            defaultValueSql: "getutcdate()",
            oldClrType: typeof(DateTime),
            oldType: "datetime2");

        migrationBuilder.AlterColumn<DateTime>(
            name: "Created",
            table: "Passages",
            type: "datetime2",
            nullable: false,
            defaultValueSql: "getutcdate()",
            oldClrType: typeof(DateTime),
            oldType: "datetime2");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "Updated",
            table: "Passages",
            type: "datetime2",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValueSql: "getutcdate()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "Created",
            table: "Passages",
            type: "datetime2",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValueSql: "getutcdate()");
    }
}
