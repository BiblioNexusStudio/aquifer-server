using Aquifer.API.Common;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.List;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/resources/content");
        Permissions(PermissionName.ReadResourceLists);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var (hasSearchQuery, isExactSearch) = GetSearchParams(req.SearchQuery);
        var likeKey = isExactSearch ? "" : "%";
        var searchParameter = new SqlParameter("SearchQuery", hasSearchQuery ? $"{likeKey}{req.SearchQuery!.Trim('"')}{likeKey}" : "");

        var total = (await dbContext.Database.SqlQueryRaw<int>(BuildQuery(req, true, hasSearchQuery, isExactSearch), searchParameter)
            .ToListAsync(ct)).Single();
        var resourceContent = total == 0
            ? []
            : await dbContext.Database
                .SqlQueryRaw<ResourceContentResponse>(BuildQuery(req, false, hasSearchQuery, isExactSearch), searchParameter)
                .ToListAsync(ct);

        var response = new Response
        {
            ResourceContents = resourceContent,
            Total = total
        };

        await SendOkAsync(response, ct);
    }

    private static string BuildQuery(Request req, bool getTotalCount, bool hasSearchQuery, bool isExactSearch)
    {
        const string selectCount = "SELECT COUNT(DISTINCT(RC.Id)) AS Count";
        const string selectProperties = """
                                        SELECT RC.Id AS Id, R.EnglishLabel, PR.DisplayName AS ParentResourceName,
                                        L.EnglishDisplay AS LanguageEnglishDisplay, RC.Status AS StatusValue,
                                        IIF(MAX(CAST(RCV.IsPublished as INT)) = 1, 1, 0) AS IsPublishedValue
                                        """;

        var groupingWithOffset = $"""
                                  GROUP BY RC.Id, R.EnglishLabel, PR.DisplayName, L.EnglishDisplay, RC.Status, R.SortOrder
                                  ORDER BY PR.DisplayName, R.SortOrder, R.EnglishLabel
                                  OFFSET {req.Offset} ROWS FETCH NEXT {req.Limit} ROWS ONLY
                                  """;

        return $"""
                {(getTotalCount ? selectCount : selectProperties)}
                FROM ResourceContents RC
                    INNER JOIN Resources R ON R.Id = RC.ResourceId
                    INNER JOIN ParentResources PR ON PR.id =R.ParentResourceId
                    INNER JOIN Languages L ON L.Id = RC.LanguageId
                    INNER JOIN ResourceContentVersions RCV ON RCV.ResourceContentId = RC.Id
                WHERE RC.MediaType != {(int)ResourceContentMediaType.Audio}
                {ApplyLanguageIdFilter(req.LanguageId)}
                {ApplyParentResourceIdFilter(req.ParentResourceId)}
                {ApplyIsPublishedFilter(req.IsPublished)}
                {ApplySearchQueryFilter(hasSearchQuery, isExactSearch)}
                {ApplyBookAndChapterFilter(req.BookCode, req.StartChapter, req.EndChapter)}
                {(getTotalCount ? "" : groupingWithOffset)}
                """;
    }

    private static string ApplyLanguageIdFilter(int? languageId)
    {
        return languageId.HasValue ? $"AND RC.LanguageId = {languageId.Value}" : "";
    }

    private static string ApplyParentResourceIdFilter(int? parentResourceId)
    {
        return parentResourceId.HasValue ? $"AND R.ParentResourceId = {parentResourceId.Value}" : "";
    }

    private static string ApplyBookAndChapterFilter(string? bookCode, int? startChapter, int? endChapter)
    {
        var verseRange = BibleUtilities.VerseRangeForBookAndChapters(bookCode, startChapter, endChapter);
        if (verseRange is null)
        {
            return "";
        }

        var (startVerseId, endVerseId) = verseRange.Value;

        return $"""
                AND (
                EXISTS (SELECT 1
                    FROM [VerseResources] AS [v]
                    WHERE [R].[Id] = [v].[ResourceId] AND [v].[VerseId] BETWEEN {startVerseId} AND {endVerseId})
                OR
                EXISTS (SELECT 1
                    FROM [PassageResources] AS [p]
                    INNER JOIN [Passages] AS [p0] ON [p].[PassageId] = [p0].[Id]
                    WHERE [R].[Id] = [p].[ResourceId]
                          AND [p0].[StartVerseId] BETWEEN {startVerseId} AND {endVerseId}
                          AND [p0].[EndVerseId] BETWEEN {startVerseId} AND {endVerseId}
                          AND [p0].[StartVerseId] <= {startVerseId} AND [p0].[EndVerseId] >= {endVerseId})
                )
                """;
    }

    private static string ApplyIsPublishedFilter(bool? isPublished)
    {
        if (!isPublished.HasValue)
        {
            return "";
        }

        return isPublished.Value ? "AND RCV.IsPublished = 1" : "AND RCV.IsDraft = 1";
    }

    private static string ApplySearchQueryFilter(bool hasSearchQuery, bool isExactSearch)
    {
        if (!hasSearchQuery)
        {
            return "";
        }

        return isExactSearch ? "AND R.EnglishLabel = @SearchQuery" : "AND R.EnglishLabel LIKE @SearchQuery";
    }

    private static (bool hasSearchQuery, bool isExactQuery) GetSearchParams(string? searchQuery)
    {
        var hasSearchQuery = !string.IsNullOrWhiteSpace(searchQuery);
        return (hasSearchQuery, hasSearchQuery && searchQuery![0].Equals('"') && searchQuery[^1].Equals('"'));
    }
}