using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Unaquiferized.List;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, List<Response>>
{
    public override void Configure()
    {
        Get("/resources/unaquiferized");
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var chapterAndVerseRanges = BibleUtilities.VerseRangesForBookAndChapters(request.BookCode, request.Chapters);

        var rangesSql = chapterAndVerseRanges.Count != 0
            ? string.Join(", ", chapterAndVerseRanges.Select(range => $"({range.Item1}, {range.Item2})"))
            : "(0, 0)"; // stubbed values, will be ignored
        var rangeIsEmpty = rangesSql == "(0, 0)" ? "1 = 1" : "1 = 0";

        var query = $"""
                     WITH ChapterVerseRanges AS (
                         SELECT * FROM (VALUES {rangesSql}) AS T(StartVerseId, EndVerseId)
                     ),
                     ResourceContentCandidates AS (
                         SELECT rc.Id AS ResourceContentId, rc.ResourceId
                         FROM ResourceContents rc
                         INNER JOIN Resources r ON r.Id = rc.ResourceId
                         INNER JOIN ResourceContentVersions rcv ON rc.Id = rcv.ResourceContentId
                         INNER JOIN Languages l ON l.Id = rc.LanguageId
                         WHERE l.ISO6393Code = 'eng'
                         AND r.ParentResourceId = @ParentResourceId
                         AND rc.MediaType = {(int)ResourceContentMediaType.Text}
                         AND rc.Status = {(int)ResourceContentStatus.New}
                         AND (@SearchQuery = '' OR r.EnglishLabel LIKE '%' + @SearchQuery + '%')
                         AND rc.Id NOT IN (SELECT ResourceContentId FROM ProjectResourceContents)
                     ),
                     FilteredResources AS (
                         SELECT DISTINCT rcc.ResourceContentId
                         FROM ResourceContentCandidates rcc
                         LEFT JOIN PassageResources pr ON rcc.ResourceId = pr.ResourceId
                         LEFT JOIN Passages p ON p.Id = pr.PassageId
                         LEFT JOIN VerseResources vr ON rcc.ResourceId = vr.ResourceId
                         WHERE {rangeIsEmpty} OR EXISTS (
                             SELECT 1
                             FROM ChapterVerseRanges cvr
                             WHERE (p.StartVerseId BETWEEN cvr.StartVerseId AND cvr.EndVerseId)
                             OR (p.EndVerseId BETWEEN cvr.StartVerseId AND cvr.EndVerseId)
                             OR (p.StartVerseId <= cvr.StartVerseId AND p.EndVerseId >= cvr.EndVerseId)
                             OR (vr.VerseId >= cvr.StartVerseId AND vr.VerseId <= cvr.EndVerseId)
                         )
                     )
                     SELECT
                         COALESCE(rcv.WordCount, 0) AS WordCount,
                         rc.ResourceId AS ResourceId,
                         r.EnglishLabel AS Title
                     FROM
                         ResourceContentVersions rcv
                     INNER JOIN ResourceContents rc ON rcv.ResourceContentId = rc.Id
                     INNER JOIN FilteredResources fr ON fr.ResourceContentId = rc.Id
                     INNER JOIN Resources r ON rc.ResourceId = r.Id
                     WHERE rcv.Id = (
                         SELECT TOP 1 rcv2.id
                         FROM ResourceContentVersions rcv2
                         WHERE rcv.ResourceContentId = rcv2.ResourceContentId
                         ORDER BY Created DESC
                     )
                     ORDER BY r.EnglishLabel ASC;
                     """;

        var response = await dbContext.Database
            .SqlQueryRaw<Response>(query,
                new SqlParameter("ParentResourceId", request.ParentResourceId),
                new SqlParameter("SearchQuery", request.SearchQuery ?? ""))
            .ToListAsync(ct);

        await SendOkAsync(response, ct);
    }
}