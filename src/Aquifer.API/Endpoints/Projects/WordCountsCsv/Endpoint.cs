using System.Text;
using Aquifer.API.Common;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Projects.WordCountsCsv;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request>
{
    private const string Query = """
                                 SELECT
                                     R.EnglishLabel, Snapshots.WordCount
                                 FROM ResourceContents RC
                                     INNER JOIN Resources R ON R.Id = RC.ResourceId
                                     INNER JOIN ResourceContentVersions RCV ON RCV.ResourceContentId = RC.Id
                                     CROSS APPLY (
                                         SELECT TOP 1 WordCount
                                         FROM ResourceContentVersionSnapshots
                                         WHERE ResourceContentVersionId = RCV.Id
                                         ORDER BY Created ASC
                                     ) Snapshots
                                 WHERE RC.Id IN (SELECT ResourceContentId FROM ProjectResourceContents WHERE ProjectId = {0})
                                 ORDER BY R.EnglishLabel
                                 """;

    public override void Configure()
    {
        Get("/projects/{ProjectId}/word-counts.csv");
        Permissions(PermissionName.ReadProject, PermissionName.ReadProjectsInCompany);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var project = await dbContext.Projects.SingleOrDefaultAsync(p => p.Id == request.ProjectId, ct);
        if (project == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var rows = await dbContext.Database.SqlQueryRaw<EnglishLabelAndCount>(Query, request.ProjectId).ToListAsync(ct);
        var csvText = "Title,Word Count\n" +
                      string.Join('\n', rows.Select(tc => $"\"{tc.EnglishLabel.Replace("\"", "\"\"")}\",{tc.WordCount}"));

        await SendStreamAsync(new MemoryStream(Encoding.UTF8.GetBytes(csvText)),
            $"{project.Name} - {DateTime.Now:yyyy-MM-dd} - WordCounts.csv", null, "text/csv", null, false, ct);
    }
}

public class EnglishLabelAndCount
{
    public required string EnglishLabel { get; set; }
    public required int WordCount { get; set; }
}