using Aquifer.API.Common;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Reports.Dynamic.Get;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/reports/dynamic/{Id}");
        Permissions(PermissionName.ReadReports);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var report = await dbContext.Reports.FindAsync([request.Id], ct);

        if (report == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await using var connection = dbContext.Database.GetDbConnection();
        await connection.OpenAsync(ct);

        await using var command = connection.CreateCommand();
        command.CommandText = report.StoredProcedureName;

        if (report.AcceptsDateRange)
        {
            command.Parameters.Add(new SqlParameter("@StartDate", request.StartDate));
            command.Parameters.Add(new SqlParameter("@EndDate", request.EndDate));
            command.CommandText += " @StartDate, @EndDate";
        }

        if (report.AcceptsLanguage)
        {
            command.Parameters.Add(new SqlParameter("@LanguageId", request.LanguageId));
            command.CommandText += " @LanguageId";
        }

        if (report.AcceptsParentResource)
        {
            command.Parameters.Add(new SqlParameter("@ParentResourceId", request.ParentResourceId));
            command.CommandText += " @ParentResourceId";
        }

        var items = new List<List<object>>();
        var columnNames = new List<string>();

        await using var reader = await command.ExecuteReaderAsync(ct);

        for (var i = 0; i < reader.FieldCount; i++)
        {
            columnNames.Add(reader.GetName(i));
        }

        while (await reader.ReadAsync(ct))
        {
            var item = new List<object>();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                item.Add(reader[i]);
            }

            items.Add(item);
        }

        var response = new Response
        {
            Name = report.Name,
            Description = report.Description,
            Type = report.Type,
            AcceptsDateRange = report.AcceptsDateRange,
            AcceptsLanguage = report.AcceptsLanguage,
            AcceptsParentResource = report.AcceptsParentResource,
            Columns = columnNames,
            Results = items
        };

        await SendOkAsync(response, ct);
    }
}