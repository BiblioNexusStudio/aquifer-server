using Aquifer.API.Helpers;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.ParentResources.Statuses.BibleBooks.Get;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, List<Response>>
{
    private const int EnglishLanguageId = 1;

    public override void Configure()
    {
        Get("/resources/parent-resources/statuses/bible-books");
        Options(EndpointHelpers.ServerCacheInSeconds(EndpointHelpers.OneHourInSeconds));
        ResponseCache(EndpointHelpers.OneDayInSeconds, varyByQueryKeys: ["languageId", "resourceId"]);
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var englishVerses = await GetVerseResourceCountAsync(EnglishLanguageId, request.ParentResourceId, ct);
        var englishPassages = await GetPassageResourceCountAsync(EnglishLanguageId, request.ParentResourceId, ct);
        var languageVerses = await GetVerseResourceCountAsync(request.LanguageId, request.ParentResourceId, ct);
        var languagePassages = await GetPassageResourceCountAsync(request.LanguageId, request.ParentResourceId, ct);

        var rowsEnglishTotals = englishVerses.Concat(englishPassages).GroupBy(x => x.BibleBookId)
            .Select(x => new BookRow
            {
                BibleBookId = x.Key,
                BookName = x.Select(y => y.BookName).First(),
                LastPublished = x.Select(y => y.LastPublished).Max(),
                TotalResources = x.Sum(y => y.TotalResources)
            }).ToList();

        var rowsLanguageTotals = languageVerses.Concat(languagePassages).GroupBy(x => x.BibleBookId)
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
                Status = ParentResourceStatusHelpers.GetStatus(englishRow.TotalResources, languageRow?.TotalResources ?? 0,
                    languageRow?.LastPublished)
            });
        }

        await SendOkAsync(responseRows, ct);
    }

    private async Task<List<BookRow>> GetPassageResourceCountAsync(int languageId, int resourceId, CancellationToken ct)
    {
        return await dbContext.Database.SqlQuery<BookRow>($"""
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
                                                           WHERE RC.LanguageId = {languageId}
                                                               AND PR.Id = {resourceId}
                                                           GROUP BY PR.Id, PR.DisplayName, SUBSTRING(CAST(StartVerseId AS VARCHAR(10)), 2, 3), BB.LocalizedName
                                                           ORDER BY SUBSTRING(CAST(StartVerseId AS VARCHAR(10)), 2, 3)
                                                           """).ToListAsync(ct);
    }

    private async Task<List<BookRow>> GetVerseResourceCountAsync(int languageId, int resourceId, CancellationToken ct)
    {
        return await dbContext.Database.SqlQuery<BookRow>($"""
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
                                                           WHERE RC.LanguageId = {languageId}
                                                           AND PR.Id = {resourceId}
                                                           GROUP BY PR.Id, PR.DisplayName, SUBSTRING(CAST(VerseId AS VARCHAR(10)), 2, 3), BB.LocalizedName
                                                               ORDER BY SUBSTRING(CAST(VerseId AS VARCHAR(10)), 2, 3)
                                                           """)
            .ToListAsync(ct);
    }
}

internal class BookRow
{
    public string BookName { get; set; } = null!;
    public string BibleBookId { get; set; } = null!;
    public int TotalResources { get; set; }
    public DateTime? LastPublished { get; set; }
}