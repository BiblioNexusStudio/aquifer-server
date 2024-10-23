using Aquifer.API.Common;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Untranslated.List;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, List<Response>>
{
    public override void Configure()
    {
        Get("/resources/untranslated");
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
                         WHERE rc.LanguageId = {Constants.EnglishLanguageId}
                         AND r.ParentResourceId = @ParentResourceId
                         AND rcv.IsPublished = 1
                         AND rc.MediaType = {(int)ResourceContentMediaType.Text}
                         AND NOT EXISTS (
                             SELECT 1
                             FROM ResourceContents rc2
                             WHERE rc2.ResourceId = rc.ResourceId
                             AND rc2.MediaType = {(int)ResourceContentMediaType.Text}
                             AND rc2.LanguageId = @LanguageId
                             AND (rc2.Status != {(int)ResourceContentStatus.TranslationAwaitingAiDraft}
                                 OR EXISTS (SELECT 1 FROM ProjectResourceContents prc WHERE prc.ResourceContentId = rc2.Id))
                         )
                         AND (@SearchQuery = '' OR r.EnglishLabel LIKE '%' + @SearchQuery + '%')
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
                         r.EnglishLabel AS Title,
                         r.SortOrder,
                         CAST(CASE WHEN rc.Status IN
                                 ({(int)ResourceContentStatus.AquiferizeEditorReview},
                                  {(int)ResourceContentStatus.AquiferizePublisherReview},
                                  {(int)ResourceContentStatus.AquiferizeReviewPending}) THEN 1 ELSE 0 END AS BIT) AS IsBeingAquiferized
                     FROM
                         ResourceContentVersions rcv
                     INNER JOIN ResourceContents rc ON rcv.ResourceContentId = rc.Id
                     INNER JOIN FilteredResources fr ON fr.ResourceContentId = rc.Id
                     INNER JOIN Resources r ON rc.ResourceId = r.Id
                     WHERE rcv.IsPublished = 1
                     ORDER BY r.SortOrder ASC, r.EnglishLabel ASC;
                     """;

        var response = await dbContext.Database
            .SqlQueryRaw<Response>(query,
                new SqlParameter("ParentResourceId", request.ParentResourceId),
                new SqlParameter("LanguageId", request.LanguageId),
                new SqlParameter("SearchQuery", request.SearchQuery ?? ""))
            .ToListAsync(ct);

        await SendOkAsync(response, ct);
    }
}