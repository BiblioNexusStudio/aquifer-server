using Aquifer.Common.Extensions;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.ParentResources.Statuses.List;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, List<Response>>
{
    private const string Query = """
                                 select R.ParentResourceId, RC.LanguageId, count(*) AS Count from Resources R
                                                          inner join ResourceContents RC on R.Id = RC.ResourceId AND RC.LanguageId IN (@EnglishLanguageId, @LanguageId)
                                                          inner join ResourceContentVersions RCV ON RC.Id = RCV.ResourceContentId AND RCV.IsPublished = 1
                                 GROUP BY R.ParentResourceId, RC.LanguageId;
                                 """;

    public override void Configure()
    {
        Get("/resources/parent-resources/statuses");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var languageExists = dbContext.ResourceContents.Any(x => x.Language.Id == request.LanguageId);
        if (!languageExists)
        {
            await SendOkAsync([], ct);
        }

        var rows = await dbContext.Database
            .SqlQueryRaw<ParentAndLanguageCountRow>(Query,
                new SqlParameter("LanguageId", request.LanguageId),
                new SqlParameter("EnglishLanguageId", 1))
            .ToListAsync(ct);

        var response = await dbContext.ParentResources.Select(x => new Response
        {
            ResourceType = x.ResourceType.GetDisplayName(),
            ResourceId = x.Id,
            Title = x.DisplayName,
            LicenseInfoValue = x.LicenseInfo,
            Status = GetStatus(x.Id, request.LanguageId, rows)
        }).ToListAsync(ct);

        await SendOkAsync(response, ct);
    }

    private static ParentResourceStatus GetStatus(int parentResourceId, int languageId, List<ParentAndLanguageCountRow> rows)
    {
        var otherLangRow = rows.Find(x => x.ParentResourceId == parentResourceId && x.LanguageId == languageId);
        var englishRow = rows.Find(x => x.ParentResourceId == parentResourceId && x.LanguageId == 1);
        if (otherLangRow == null)
        {
            return ParentResourceStatus.ComingSoon;
        }

        if (otherLangRow.Count == englishRow!.Count)
        {
            return ParentResourceStatus.Complete;
        }

        return otherLangRow.Count == 0 ? ParentResourceStatus.ComingSoon : ParentResourceStatus.Partial;
    }
}

public record ParentAndLanguageCountRow
{
    public required int ParentResourceId { get; set; }
    public required int LanguageId { get; set; }
    public required int Count { get; set; }
}