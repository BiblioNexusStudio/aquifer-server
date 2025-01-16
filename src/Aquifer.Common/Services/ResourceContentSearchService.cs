using Aquifer.Data;
using Aquifer.Data.Entities;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Common.Services;

public interface IResourceContentSearchService
{
    Task<(int Total, IReadOnlyList<DbResourceContentSummary> ResourceContentSummaries)> SearchAsync(
        ResourceContentSearchFilter filter,
        int offset,
        int limit,
        CancellationToken ct);
}

public sealed class ResourceContentSearchFilter
{
    public int? ParentResourceId { get; set; }
    public int? ResourceId { get; set; }
    public string? ResourceEnglishLabelQuery { get; set; }
    public int? LanguageId { get; set; }
    public IReadOnlyList<ResourceContentMediaType>? ExcludeContentMediaTypes { get; set; }
    public IReadOnlyList<ResourceContentMediaType>? IncludeContentMediaTypes { get; set; }
    public IReadOnlyList<ResourceContentStatus>? ExcludeContentStatuses { get; set; }
    public IReadOnlyList<ResourceContentStatus>? IncludeContentStatuses { get; set; }
    public int? AssignedUserId { get; set; }
    public bool? IsPublished { get; set; }
    public int? StartVerseId { get; set; }
    public int? EndVerseId { get; set; }
    public bool? HasAudio { get; set; }
    public bool? HasUnresolvedCommentThreads { get; set; }
}

public sealed class DbResourceContentSummary
{
    public required int Id { get; init; }
    public required int ResourceId { get; init; }
    public required string ResourceEnglishLabel { get; init; }
    public required int ParentResourceId { get; init; }
    public required string ParentResourceEnglishDisplayName { get; init; }
    public required int LanguageId { get; init; }
    public required ResourceContentStatus Status { get; init; }
    public required bool IsPublished { get; init; }
    public required bool HasAudio { get; init; }
    public required bool HasUnresolvedCommentThreads { get; init; }
}

public sealed class ResourceContentSearchService(AquiferDbContext dbContext) : IResourceContentSearchService
{
    public async Task<(int Total, IReadOnlyList<DbResourceContentSummary> ResourceContentSummaries)> SearchAsync(
        ResourceContentSearchFilter filter,
        int offset,
        int limit,
        CancellationToken ct)
    {
        if (offset < 0)
        {
            throw new ArgumentException($"\"{nameof(offset)}\" must be greater than or equal to 0.", nameof(offset));
        }

        if (limit <= 0)
        {
            throw new ArgumentException($"\"{nameof(limit)}\" must be greater than 0.", nameof(limit));
        }

        if (filter is { StartVerseId: not null, EndVerseId: null } or { StartVerseId: null, EndVerseId: not null })
        {
            throw new ArgumentException(
                $"\"{nameof(filter.StartVerseId)}\" and \"{nameof(filter.EndVerseId)}\" must both be passed if one is passed.",
                nameof(filter));
        }

        if (filter is { StartVerseId: not null, EndVerseId: not null } && filter.StartVerseId > filter.EndVerseId)
        {
            throw new ArgumentException(
                $"\"{nameof(filter.StartVerseId)}\" must be less than or equal to \"{nameof(filter.EndVerseId)}\".",
                nameof(filter));
        }

        if (filter is { ExcludeContentMediaTypes: not null, IncludeContentMediaTypes: not null })
        {
            throw new ArgumentException(
                $"\"{nameof(filter.ExcludeContentMediaTypes)}\" and \"{nameof(filter.IncludeContentMediaTypes)}\" cannot both be passed.",
                nameof(filter));
        }

        if (filter is { ExcludeContentStatuses: not null, IncludeContentStatuses: not null })
        {
            throw new ArgumentException(
                $"\"{nameof(filter.ExcludeContentStatuses)}\" and \"{nameof(filter.IncludeContentStatuses)}\" cannot both be passed.",
                nameof(filter));
        }

        const string selectCountSql = "SELECT COUNT(DISTINCT(rc.Id)) AS Count";

        var selectPropertiesSql = $"""
            SELECT
                rc.Id AS {nameof(DbResourceContentSummary.Id)},
                r.EnglishLabel AS {nameof(DbResourceContentSummary.ResourceEnglishLabel)},
                pr.DisplayName AS {nameof(DbResourceContentSummary.ParentResourceEnglishDisplayName)},
                rc.LanguageId AS {nameof(DbResourceContentSummary.LanguageId)},
                rc.Status AS {nameof(DbResourceContentSummary.Status)},
                IIF(MAX(CAST(rcv.IsPublished AS INT)) = 1, 1, 0) AS {nameof(DbResourceContentSummary.IsPublished)},
                IIF(MIN(CAST(ct.Resolved AS INT)) = 0, 1, 0) AS {nameof(DbResourceContentSummary.HasUnresolvedCommentThreads)},
                IIF(rc.MediaType = {(int)ResourceContentMediaType.Audio} OR COUNT(rcAudio.Id) > 0, 1, 0) AS {nameof(DbResourceContentSummary.HasAudio)}
            """;

        var fromSql = $"""
            FROM ResourceContents rc
                JOIN Resources r ON r.Id = rc.ResourceId
                JOIN ParentResources pr ON pr.id = r.ParentResourceId
                JOIN ResourceContentVersions rcv ON rcv.ResourceContentId = rc.Id
                JOIN ResourceContentVersionCommentThreads rcvct ON rcvct.ResourceContentVersionId = rcv.Id
                JOIN CommentThreads ct ON rcvct.CommentThreadId = ct.Id
                LEFT JOIN ResourceContents rcAudio ON
                    rcAudio.ResourceId = r.Id AND
                    rcAudio.LanguageId = rc.LanguageId AND
                    rcAudio.Id <> rc.Id AND
                    rcAudio.MediaType = {(int)ResourceContentMediaType.Audio}
            """;

        var coreParameters = new DynamicParameters();
        var whereClausesSql = new List<string>();

        if (filter.ParentResourceId.HasValue)
        {
            const string parentResourceIdParamName = "parentResourceId";

            coreParameters.Add(parentResourceIdParamName, filter.ParentResourceId.Value);
            whereClausesSql.Add($"r.ParentResourceId = @{parentResourceIdParamName}");
        }

        if (filter.ResourceId.HasValue)
        {
            const string resourceIdParamName = "resourceId";

            coreParameters.Add(resourceIdParamName, filter.ResourceId.Value);
            whereClausesSql.Add($"rc.ResourceId = @{resourceIdParamName}");
        }

        if (!string.IsNullOrWhiteSpace(filter.ResourceEnglishLabelQuery))
        {
            const string queryParamName = "query";

            var queryTrimmed = filter.ResourceEnglishLabelQuery.Trim();
            var queryWithoutQuotations = queryTrimmed.Trim('"');
            var isExactSearch = queryWithoutQuotations.Length + 2 == queryTrimmed.Length;

            if (isExactSearch)
            {
                coreParameters.Add(queryParamName, queryWithoutQuotations);
                whereClausesSql.Add($"r.EnglishLabel = @{queryParamName}");
            }
            else
            {
                coreParameters.Add(queryParamName, queryTrimmed);
                whereClausesSql.Add($"r.EnglishLabel LIKE N'%@{queryParamName}%'");
            }
        }

        if (filter.LanguageId.HasValue)
        {
            const string languageIdParamName = "languageId";

            coreParameters.Add(languageIdParamName, filter.LanguageId.Value);
            whereClausesSql.Add($"rc.LanguageId = @{languageIdParamName}");
        }

        if (filter.ExcludeContentMediaTypes is { Count: > 0 })
        {
            const string excludeContentMediaTypesParamName = "contentMediaTypeIds";

            coreParameters.Add(excludeContentMediaTypesParamName, filter.ExcludeContentMediaTypes
                .Select(x => (int)x)
                .ToArray());
            whereClausesSql.Add($"rc.MediaType NOT IN @{excludeContentMediaTypesParamName}");
        }

        if (filter.IncludeContentMediaTypes is { Count: > 0 })
        {
            const string includeContentMediaTypesParamName = "contentMediaTypeIds";

            coreParameters.Add(includeContentMediaTypesParamName, filter.IncludeContentMediaTypes
                .Select(x => (int)x)
                .ToArray());
            whereClausesSql.Add($"rc.MediaType IN @{includeContentMediaTypesParamName}");
        }

        if (filter.ExcludeContentStatuses is { Count: > 0 })
        {
            const string excludeContentStatusesParamName = "contentStatusIds";

            coreParameters.Add(excludeContentStatusesParamName, filter.ExcludeContentStatuses
                .Select(x => (int)x)
                .ToArray());
            whereClausesSql.Add($"rc.Status NOT IN @{excludeContentStatusesParamName}");
        }

        if (filter.IncludeContentStatuses is { Count: > 0 })
        {
            const string includeContentStatusesParamName = "contentStatusIds";

            coreParameters.Add(includeContentStatusesParamName, filter.IncludeContentStatuses
                .Select(x => (int)x)
                .ToArray());
            whereClausesSql.Add($"rc.Status IN @{includeContentStatusesParamName}");
        }

        if (filter.AssignedUserId.HasValue)
        {
            const string assignedUserIdParamName = "assignedUserId";

            coreParameters.Add(assignedUserIdParamName, filter.AssignedUserId.Value);
            whereClausesSql.Add($"rcv.AssignedUserId = @{assignedUserIdParamName}");
        }

        // Ensure we only join with a single ResourceContentVersion in order to avoid the need to do a grouping
        // (only one ResourceContentVersion may be in IsDraft or in IsPublished for a given ResourceContent).
        if (filter.IsPublished.HasValue)
        {
            // the opposite of IsPublished is *not* IsDraft but current filtering expect this
            whereClausesSql.Add(filter.IsPublished.Value ? "rcv.IsPublished = 1" : "rcv.IsDraft = 1");
        }

        if (filter is { StartVerseId: not null, EndVerseId: not null })
        {
            const string startVerseIdParamName = "startVerseId";
            const string endVerseIdParamName = "endVerseId";

            coreParameters.Add(startVerseIdParamName, filter.StartVerseId.Value);
            coreParameters.Add(endVerseIdParamName, filter.EndVerseId.Value);
            whereClausesSql.Add($"""
                (
                    EXISTS
                    (
                        SELECT NULL
                        FROM VerseResources vr
                        WHERE r.Id = vr.ResourceId AND
                            vr.VerseId BETWEEN @{startVerseIdParamName} AND @{endVerseIdParamName}
                    )
                    OR
                    EXISTS
                    (
                        SELECT NULL
                        FROM PassageResources AS psr
                            JOIN Passages p ON psr.PassageId = p.Id
                        WHERE r.Id = psr.ResourceId AND
                            (
                                p.StartVerseId BETWEEN @{startVerseIdParamName} AND @{endVerseIdParamName} OR
                                p.EndVerseId BETWEEN @{startVerseIdParamName} AND @{endVerseIdParamName} OR
                                (p.StartVerseId <= @{startVerseIdParamName} AND p.EndVerseId >= @{endVerseIdParamName})
                            )
                    )
                )
                """);
        }

        if (filter.HasUnresolvedCommentThreads.HasValue && filter.HasUnresolvedCommentThreads.Value)
        {
            whereClausesSql.Add("ct.Resolved = 0");
        }

        if (filter.HasAudio.HasValue)
        {
            whereClausesSql.Add($"rcAudio.Id IS {(filter.HasAudio.Value ? "NOT " : "")}NULL");
        }

        // The only actual grouping property is the ResourceContent.Id but the rest are required for the select.
        const string groupBySql =
            "GROUP BY rc.Id, rc.ResourceId, r.EnglishLabel, r.ParentResourceId, pr.DisplayName, l.LanguageId, rc.Status, r.SortOrder";

        var coreSql = $"""
            {fromSql}
            {(whereClausesSql.Count > 0 ? $"WHERE {string.Join($" AND{Environment.NewLine}\t", whereClausesSql)}" : "")}
            {groupBySql}
            """;

        const string offsetLiteralName = "offset";
        const string limitLiteralName = "limit";

        const string pagingSql = $$"""
            ORDER BY pr.DisplayName, r.SortOrder, r.EnglishLabel
            OFFSET {={{offsetLiteralName}}} ROWS FETCH NEXT {={{limitLiteralName}}} ROWS ONLY
            """;

        var dataSql = $"""
            {selectPropertiesSql}
            {coreSql}
            {pagingSql}
            """;

        var pagingParameters = new DynamicParameters(coreParameters);
        pagingParameters.Add(offsetLiteralName, offset);
        pagingParameters.Add(limitLiteralName, limit);

        await using var connection = dbContext.Database.GetDbConnection();

        var resourceContentSummaries =
            (await connection.QueryWithRetriesAsync<DbResourceContentSummary>(dataSql, pagingParameters, cancellationToken: ct))
            .ToList()
            .AsReadOnly();

        // if there is only one page (a common case) we can determine the total without another query
        int total;
        if (offset == 0 && limit > resourceContentSummaries.Count)
        {
            total = resourceContentSummaries.Count;
        }
        else
        {
            var totalSql = $"""
                {selectCountSql}
                {coreSql}
                """;

            total = await connection.ExecuteScalarWithRetriesAsync<int>(totalSql, coreParameters, cancellationToken: ct);
        }

        return (total, resourceContentSummaries);
    }
}