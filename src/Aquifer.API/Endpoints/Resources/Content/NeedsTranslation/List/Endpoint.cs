using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Common;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.NeedsTranslation.List;

/// <summary>
/// Despite the general endpoint name, this is only for Community Reviewers and their dashboard. Don't use it for anything else.
/// </summary>
public class Endpoint(AquiferDbContext dbContext, IUserService userService) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/resources/content/needs-translation");
        Permissions(PermissionName.ReadResources);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);
        var (hasSearchQuery, isExactSearch) = GetSearchParams(req.SearchQuery);
        var likeKey = isExactSearch ? "" : "%";
        var searchParameter = new SqlParameter(
            "SearchQuery",
            hasSearchQuery ? $"{likeKey}{req.SearchQuery!.Trim('"')}{likeKey}" : "");

        var total = (await dbContext.Database
            .SqlQueryRaw<int>(BuildQuery(req, user, true, hasSearchQuery, isExactSearch), searchParameter)
            .ToListAsync(ct)).Single();
        var resourceContent = total == 0
            ? []
            : await dbContext.Database
                .SqlQueryRaw<ResourceContentResponse>(
                    BuildQuery(req, user, false, hasSearchQuery, isExactSearch),
                    searchParameter)
                .ToListAsync(ct);

        var response = new Response
        {
            ResourceContents = resourceContent,
            Total = total,
        };

        await SendOkAsync(response, ct);
    }

    private static string BuildQuery(Request req, UserEntity user, bool getTotalCount, bool hasSearchQuery, bool isExactSearch)
    {
        const string selectCount = "SELECT COUNT(DISTINCT(RC.Id)) AS Count";
        const string selectProperties = """
            SELECT RC.Id AS Id, R.EnglishLabel, PR.DisplayName AS ParentResourceName,
            COALESCE(RCV.SourceWordCount, RCV.WordCount) AS WordCount
            """;

        var groupingWithOffset = $"""
            GROUP BY RC.Id, R.EnglishLabel, PR.DisplayName, L.EnglishDisplay, COALESCE(RCV.SourceWordCount, RCV.WordCount), R.SortOrder
            ORDER BY PR.DisplayName, R.SortOrder, R.EnglishLabel
            OFFSET {req.Offset} ROWS FETCH NEXT {req.Limit} ROWS ONLY
            """;

        return $"""
            {(getTotalCount ? selectCount : selectProperties)}
            FROM ResourceContents RC
                INNER JOIN Resources R ON R.Id = RC.ResourceId
                INNER JOIN ParentResources PR ON PR.id = R.ParentResourceId
                INNER JOIN Languages L ON L.Id = RC.LanguageId
                INNER JOIN ResourceContentVersions RCV ON RCV.ResourceContentId = RC.Id
            WHERE RC.MediaType = {(int)ResourceContentMediaType.Text}
            AND PR.AllowCommunityReview = 1
            {ApplyLanguageIdFilter(user.LanguageId ?? 1)}
            {ApplyParentResourceIdFilter(req.ParentResourceId)}
            {ApplySearchQueryFilter(hasSearchQuery, isExactSearch)}
            {ApplyBookAndChapterFilter(req.BookCode, req.StartChapter, req.EndChapter)}
            {(getTotalCount ? "" : groupingWithOffset)}
            """;
    }

    private static string ApplyLanguageIdFilter(int languageId)
    {
        return $"""
            AND RC.LanguageId = {Constants.EnglishLanguageId} AND RCV.IsPublished = 1 AND NOT EXISTS (
                SELECT 1 FROM ResourceContents RCL
                WHERE RCL.ResourceId = R.Id AND RCL.LanguageId = {languageId}
            )
            """;
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
                      AND ([p0].[StartVerseId] BETWEEN {startVerseId} AND {endVerseId}
                           OR [p0].[EndVerseId] BETWEEN {startVerseId} AND {endVerseId}
                           OR ([p0].[StartVerseId] <= {startVerseId} AND [p0].[EndVerseId] >= {endVerseId})))
            )
            """;
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