using Aquifer.API.Helpers;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.ParentResources.Statuses.Get;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, List<Response>>
{
    private const int EnglishLanguageId = 1;

    private const string VerseResourceCountQuery = """
                                                   SELECT COUNT(DISTINCT RC.id) AS TotalResources,
                                                       SUBSTRING(CAST(VerseId AS VARCHAR(10)), 2, 3) AS BibleBookId,
                                                       MAX(RCV.Updated) AS LastPublished,
                                                       BB.LocalizedName AS BookName
                                                   FROM ResourceContents RC
                                                       INNER JOIN Resources R ON R.Id = RC.ResourceId
                                                       INNER JOIN ResourceContentVersions RCV ON RCV.ResourceContentId = RC.Id AND RCV.IsPublished = 1
                                                       INNER JOIN ParentResources PR ON PR.Id = R.ParentResourceId
                                                       INNER JOIN VerseResources VR ON VR.ResourceId = R.Id
                                                       INNER JOIN BibleBooks BB ON BB.BibleId = 1 AND BB.Number = CAST(SUBSTRING(CAST(VerseId AS VARCHAR(10)), 2, 3) AS int)
                                                   WHERE RC.LanguageId = @LanguageId
                                                       AND PR.Id = @ResourceId
                                                   GROUP BY PR.Id, PR.DisplayName, SUBSTRING(CAST(VerseId AS VARCHAR(10)), 2, 3), BB.LocalizedName
                                                   ORDER BY SUBSTRING(CAST(VerseId AS VARCHAR(10)), 2, 3)
                                                   """;

    private const string PassageResourceCountQuery = """
                                                     SELECT COUNT(DISTINCT RC.id) AS TotalResources,
                                                         SUBSTRING(CAST(StartVerseId AS VARCHAR(10)), 2, 3) AS BibleBookId,
                                                         MAX(RCV.Updated) AS LastPublished,
                                                         BB.LocalizedName AS BookName
                                                     FROM ResourceContents RC
                                                         INNER JOIN Resources R ON R.Id = RC.ResourceId
                                                         INNER JOIN ResourceContentVersions RCV ON RCV.ResourceContentId = RC.Id AND RCV.IsPublished = 1
                                                         INNER JOIN ParentResources PR ON PR.Id = R.ParentResourceId
                                                         INNER JOIN PassageResources PAR ON PAR.ResourceId = R.Id
                                                         INNER JOIN Passages PAS ON PAS.Id = PAR.PassageId
                                                         INNER JOIN BibleBooks BB ON BB.BibleId = 1 AND BB.Number = CAST(SUBSTRING(CAST(StartVerseId AS VARCHAR(10)), 2, 3) AS int)
                                                     WHERE RC.LanguageId = @LanguageId
                                                         AND PR.Id = @ResourceId
                                                     GROUP BY PR.Id, PR.DisplayName, SUBSTRING(CAST(StartVerseId AS VARCHAR(10)), 2, 3), BB.LocalizedName
                                                     ORDER BY SUBSTRING(CAST(StartVerseId AS VARCHAR(10)), 2, 3)
                                                     """;

    public override void Configure()
    {
        Get("/resources/parent-resources/statuses/{resourceId}");
        Options(EndpointHelpers.SetCacheOption(60));
        ResponseCache(EndpointHelpers.OneDayInSeconds, varyByQueryKeys: ["languageId", "resourceId"]);
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var resourceId = request.ResourceId;

        var verseRowsEnglish = await dbContext.Database.SqlQueryRaw<BookRow>(VerseResourceCountQuery,
                new SqlParameter("LanguageId", EnglishLanguageId),
                new SqlParameter("ResourceId", resourceId))
            .ToListAsync(ct);

        var passageRowsEnglish = await dbContext.Database.SqlQueryRaw<BookRow>(PassageResourceCountQuery,
                new SqlParameter("LanguageId", EnglishLanguageId),
                new SqlParameter("ResourceId", resourceId))
            .ToListAsync(ct);

        var rowsEnglishTotals = verseRowsEnglish.Concat(passageRowsEnglish).GroupBy(x => x.BibleBookId)
            .Select(x => new BookRow
            {
                BibleBookId = x.Key,
                BookName = x.Select(y => y.BookName).First(),
                LastPublished = x.Select(y => y.LastPublished).Max(),
                TotalResources = x.Sum(y => y.TotalResources)
            }).ToList();

        var verseRowsLanguage = await dbContext.Database.SqlQueryRaw<BookRow>(VerseResourceCountQuery,
                new SqlParameter("LanguageId", request.LanguageId),
                new SqlParameter("ResourceId", request.ResourceId))
            .ToListAsync(ct);

        var passageRowsLanguage = await dbContext.Database.SqlQueryRaw<BookRow>(PassageResourceCountQuery,
                new SqlParameter("LanguageId", request.LanguageId),
                new SqlParameter("ResourceId", resourceId))
            .ToListAsync(ct);

        var rowsLanguageTotals = verseRowsLanguage.Concat(passageRowsLanguage).GroupBy(x => x.BibleBookId)
            .Select(x => new BookRow
            {
                BibleBookId = x.Key,
                BookName = x.Select(y => y.BookName).First(),
                LastPublished = x.Select(y => y.LastPublished).Max(),
                TotalResources = x.Sum(y => y.TotalResources)
            }).ToList();

        List<Response> responseRows = [];
        foreach (var englishRow in rowsEnglishTotals)
        {
            var languageRow = rowsLanguageTotals.Find(r => r.BibleBookId == englishRow.BibleBookId);
            responseRows.Add(new Response
            {
                Book = englishRow.BookName,
                Status = ResourceStatusUtilities.GetStatus(englishRow.TotalResources, languageRow?.TotalResources ?? 0,
                    languageRow?.LastPublished)
            });
        }

        await SendOkAsync(responseRows, ct);
    }
}

internal class BookRow
{
    public string BookName { get; set; } = null!;
    public string BibleBookId { get; set; } = null!;
    public int TotalResources { get; set; }
    public DateTime? LastPublished { get; set; }
}