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
        int? limit,
        bool shouldSortByName,
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
    public int? AssignedUserCompanyId { get; set; }
    public bool? IsPublished { get; set; }
    public bool? IsDraft { get; set; }
    public int? StartVerseId { get; set; }
    public int? EndVerseId { get; set; }
    public bool? HasAudio { get; set; }
    public bool? HasUnresolvedCommentThreads { get; set; }
    public bool? IsInProject { get; set; }
}

public sealed class DbResourceContentSummary
{
    public required int Id { get; init; }
    public required int ResourceId { get; init; }
    public required string ResourceEnglishLabel { get; init; }
    public required int ResourceSortOrder { get; init; }
    public required int ParentResourceId { get; init; }
    public required string ParentResourceEnglishDisplayName { get; init; }
    public required int LanguageId { get; init; }
    public required ResourceContentStatus Status { get; init; }
    public required DateTime? ContentUpdated { get; init; }

    /// <summary>
    /// Will not be <c>null</c> if <see cref="ResourceContentSearchFilter.IsInProject"/> is <c>true</c>.
    /// </summary>
    public required int? ProjectId { get; init; }

    /// <summary>
    /// If <see cref="IsDraft"/> is <c>true</c> then this will be the ID of the ResourceContentVersion in the Draft status,
    /// and it will always be the most recent ResourceContentVersion's ID for the ResourceContent.
    /// </summary>
    public required int LatestResourceContentVersionId { get; init; }

    public required bool IsPublished { get; init; }
    public required bool IsDraft { get; init; }
    public required bool HasAudio { get; init; }
    public required bool HasUnresolvedCommentThreads { get; init; }

    /// <summary>
    /// Will not be <c>null</c> if either <see cref="ResourceContentSearchFilter.AssignedUserId"/> or
    /// <see cref="ResourceContentSearchFilter.AssignedUserCompanyId"/> is not <c>null</c>.
    /// </summary>
    public required int? AssignedUserId { get; init; }
}

public sealed class ResourceContentSearchService(AquiferDbContext dbContext) : IResourceContentSearchService
{
    public async Task<(int Total, IReadOnlyList<DbResourceContentSummary> ResourceContentSummaries)> SearchAsync(
        ResourceContentSearchFilter filter,
        int offset,
        int? limit,
        bool shouldSortByName,
        CancellationToken ct)
    {
        if (offset < 0)
        {
            throw new ArgumentException($"\"{nameof(offset)}\" must be greater than or equal to 0.", nameof(offset));
        }

        if (limit is <= 0)
        {
            throw new ArgumentException($"\"{nameof(limit)}\" must be greater than 0.", nameof(limit));
        }

        if (limit is null && offset > 0)
        {
            throw new ArgumentException($"\"{nameof(limit)}\" must be passed if \"{nameof(offset)}\" is greater than 0.", nameof(limit));
        }

        if (filter is { IsDraft: not null, IsPublished: not null })
        {
            throw new ArgumentException(
                $"\"{nameof(filter.IsDraft)}\" and \"{nameof(filter.IsPublished)}\" cannot both be passed.",
                nameof(filter));
        }

        if (filter is { AssignedUserId: not null, IsDraft: not true })
        {
            throw new ArgumentException(
                $"Filtering on \"{nameof(filter.AssignedUserId)}\" requires also passing \"{nameof(filter.IsDraft)}\" as true.",
                nameof(filter));
        }

        if (filter is { AssignedUserCompanyId: not null, IsDraft: not true })
        {
            throw new ArgumentException(
                $"Filtering on \"{nameof(filter.AssignedUserCompanyId)}\" requires also passing \"{nameof(filter.IsDraft)}\" as true.",
                nameof(filter));
        }

        if (filter is { HasUnresolvedCommentThreads: not null, IsDraft: not true })
        {
            throw new ArgumentException(
                $"Filtering on \"{nameof(filter.HasUnresolvedCommentThreads)}\" requires also passing \"{nameof(filter.IsDraft)}\" as true.",
                nameof(filter));
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

        const string selectCountSql = "SELECT COUNT(rc.Id) AS Count";

        // Facts that help improve search performance:
        // 1. If a ResourceContentVersion is in the Draft status then it must be the most recent (by Id) for a Resource Content.
        // 2. If a ResourceContentVersion has an AssignedUserId then it must also be in the Draft status.
        // 3. For unresolved comment threads, only comment threads on a ResourceContentVersion in the Draft status are considered.
        //    Older ResourceContentVersions that have unresolved comment threads are ignored.
        // 4. Only one ResourceContentVersion can be in the Published status for a ResourceContent.
        // 5. Only one ResourceContentVersion can be in the Draft status for a ResourceContent.
        //
        // Thus, if the IsPublished filter is true then we can ignore the AssignedUserId/AssignedUserCompanyId filters and don't need
        // to calculate the HasUnresolvedCommentThreads property.
        // Therefore, we can fetch one row of summary data about all ResourceContentVersions for a ResourceContent (labeled rcvd below)
        // which allows avoiding grouping in the main query.
        var selectPropertiesSql = $"""
            SELECT
                rc.Id AS {nameof(DbResourceContentSummary.Id)},
                r.EnglishLabel AS {nameof(DbResourceContentSummary.ResourceEnglishLabel)},
                r.SortOrder AS {nameof(DbResourceContentSummary.ResourceSortOrder)},
                pr.DisplayName AS {nameof(DbResourceContentSummary.ParentResourceEnglishDisplayName)},
                rc.LanguageId AS {nameof(DbResourceContentSummary.LanguageId)},
                rc.Status AS {nameof(DbResourceContentSummary.Status)},
                rc.ContentUpdated AS {nameof(DbResourceContentSummary.ContentUpdated)},
                prc.ProjectId AS {nameof(DbResourceContentSummary.ProjectId)},
                rcvd.IsPublished AS {nameof(DbResourceContentSummary.IsPublished)},
                rcvd.IsDraft AS {nameof(DbResourceContentSummary.IsDraft)},
                rcvd.MaxRcvId AS {nameof(DbResourceContentSummary.LatestResourceContentVersionId)},
                IIF(rcvd.IsDraft = 1, rcvd.AssignedUserId, NULL) AS {nameof(DbResourceContentSummary.AssignedUserId)},
                IIF(ISNULL(a.AudioCount, 0) > 0, 1, 0) AS {nameof(DbResourceContentSummary.HasAudio)},
                {(filter.IsPublished.HasValue && filter.IsPublished.Value
                    ? $"""
                           NULL AS {nameof(DbResourceContentSummary.AssignedUserId)},
                           0 AS {nameof(DbResourceContentSummary.HasUnresolvedCommentThreads)}
                       """
                    : $"""
                           rcvd.AssignedUserId AS {nameof(DbResourceContentSummary.AssignedUserId)},
                           ISNULL(c.HasUnresolvedCommentThreads, 0) AS {nameof(DbResourceContentSummary.HasUnresolvedCommentThreads)}
                       """
                )}
            """;

        // Using CROSS APPLY and OUTER APPLY with inner groupings significantly improves performance over using JOINs with a main grouping.
        var fromSql = $"""
            FROM ResourceContents rc
                JOIN Resources r ON r.Id = rc.ResourceId
                JOIN ParentResources pr ON pr.id = r.ParentResourceId
                CROSS APPLY
                (
                    SELECT
                        MAX(rcv.Id) AS MaxRcvId,
                        MAX(rcv.AssignedUserId) AS AssignedUserId,
                        IIF(MAX(CAST(rcv.IsPublished AS INT)) = 1, 1, 0) AS IsPublished,
                        IIF(MAX(CAST(rcv.IsDraft AS INT)) = 1, 1, 0) AS IsDraft
                    FROM ResourceContentVersions rcv
                    WHERE rcv.ResourceContentId = rc.Id
                    GROUP BY rcv.ResourceContentId
                ) rcvd
                LEFT JOIN ProjectResourceContents prc ON prc.ResourceContentId = rc.Id
                {(filter.AssignedUserCompanyId.HasValue ? "    LEFT JOIN Users u ON rcvd.AssignedUserId = u.Id" : "")}
                OUTER APPLY
                (
                    SELECT COUNT(rcAudio.Id) AS AudioCount
                    FROM ResourceContents rcAudio
                    WHERE
                        rcAudio.ResourceId = r.Id AND
                        rcAudio.LanguageId = rc.LanguageId AND
                        rcAudio.Id <> rc.Id AND
                        rcAudio.MediaType = {(int)ResourceContentMediaType.Audio}
                    GROUP BY rcAudio.ResourceId
                ) a
                {(filter.IsPublished.HasValue && filter.IsPublished.Value
                    ? ""
                    : """
                        OUTER APPLY
                        (
                            SELECT IIF(MIN(CAST(ct.Resolved AS INT)) = 0, 1, 0) AS HasUnresolvedCommentThreads
                            FROM ResourceContentVersionCommentThreads rcvct
                                JOIN CommentThreads ct ON rcvct.CommentThreadId = ct.Id
                            WHERE rcvd.IsDraft = 1 AND rcvct.ResourceContentVersionId = rcvd.MaxRcvId
                            GROUP BY rcvct.ResourceContentVersionId
                        ) c
                    """)}
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
                whereClausesSql.Add($"r.EnglishLabel LIKE N'%' + @{queryParamName} + N'%'");
            }
        }

        if (filter.LanguageId.HasValue)
        {
            const string languageIdParamName = "languageId";

            coreParameters.Add(languageIdParamName, filter.LanguageId.Value);
            whereClausesSql.Add($"rc.LanguageId = @{languageIdParamName}");
        }

        if (filter.IsInProject.HasValue)
        {
            whereClausesSql.Add($"prc.ProjectId IS {(filter.IsInProject.Value ? "NOT " : "")}NULL");
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

        if (filter.HasUnresolvedCommentThreads.HasValue)
        {
            whereClausesSql.Add($"ISNULL(c.HasUnresolvedCommentThreads, 0) = {(filter.HasUnresolvedCommentThreads.Value ? "1" : "0")}");
        }

        if (filter.HasAudio.HasValue)
        {
            whereClausesSql.Add($"ISNULL(a.AudioCount, 0) {(filter.HasAudio.Value ? ">" : "=")} 0");
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
                        WHERE
                            r.Id = psr.ResourceId AND
                            (
                                p.StartVerseId BETWEEN @{startVerseIdParamName} AND @{endVerseIdParamName} OR
                                p.EndVerseId BETWEEN @{startVerseIdParamName} AND @{endVerseIdParamName} OR
                                (p.StartVerseId <= @{startVerseIdParamName} AND p.EndVerseId >= @{endVerseIdParamName})
                            )
                    )
                )
            """);
        }

        if (filter.AssignedUserId.HasValue)
        {
            const string assignedUserIdParamName = "assignedUserId";

            coreParameters.Add(assignedUserIdParamName, filter.AssignedUserId.Value);
            whereClausesSql.Add($"rcvd.AssignedUserId = @{assignedUserIdParamName}");
        }

        if (filter.AssignedUserCompanyId.HasValue)
        {
            const string assignedUserCompanyIdParamName = "assignedUserCompanyId";

            coreParameters.Add(assignedUserCompanyIdParamName, filter.AssignedUserCompanyId.Value);
            whereClausesSql.Add($"u.CompanyId = @{assignedUserCompanyIdParamName}");
        }

        if (filter.IsPublished.HasValue)
        {
            if (filter.IsPublished.Value)
            {
                whereClausesSql.Add("rcvd.IsPublished = 1");
            }
            else
            {
                // supporting this search will add complexity and should only be added if needed
                throw new NotImplementedException($"A \"{nameof(filter.IsPublished)}\" value of false is not yet supported.");
            }
        }

        if (filter.IsDraft.HasValue)
        {
            if (filter.IsDraft.Value)
            {
                whereClausesSql.Add("rcvd.IsDraft = 1");
            }
            else
            {
                // supporting this search will add complexity and should only be added if needed
                throw new NotImplementedException($"A \"{nameof(filter.IsDraft)}\" value of false is not yet supported.");
            }
        }

        var whereSql = whereClausesSql.Count > 0
            ? $"WHERE{Environment.NewLine}    {string.Join($" AND{Environment.NewLine}    ", whereClausesSql)}"
            : "";

        var hasPaging = offset != 0 || limit != null;

        const string offsetLiteralName = "offset";
        const string limitLiteralName = "limit";
        const string pagingSql = $$"""
           OFFSET {={{offsetLiteralName}}} ROWS FETCH NEXT {={{limitLiteralName}}} ROWS ONLY
           """;

        var pagingParameters = new DynamicParameters(coreParameters);
        if (hasPaging)
        {
            pagingParameters.Add(offsetLiteralName, offset);
            pagingParameters.Add(limitLiteralName, limit);
        }

        var sortSql = shouldSortByName
            ? """
            ORDER BY
                pr.DisplayName,
                r.SortOrder,
                r.EnglishLabel
            """
            : "ORDER BY rc.Id";

        var dataSql = $"""
            {selectPropertiesSql}
            {fromSql}
            {whereSql}
            {sortSql}
            {(hasPaging ? pagingSql : "")}
            """;

        var resourceContentSummaries = (await dbContext.Database.GetDbConnection()
                .QueryWithRetriesAsync<DbResourceContentSummary>(dataSql, pagingParameters, cancellationToken: ct))
            .ToList()
            .AsReadOnly();

        // if there is only one page (a common case) then we can determine the total without another query
        int total;
        if (offset == 0 && limit > resourceContentSummaries.Count)
        {
            total = resourceContentSummaries.Count;
        }
        else
        {
            var totalSql = $"""
                {selectCountSql}
                {fromSql}
                {whereSql}
                """;

            total = await dbContext.Database.GetDbConnection()
                .ExecuteScalarWithRetriesAsync<int>(totalSql, coreParameters, cancellationToken: ct);
        }

        return (total, resourceContentSummaries);
    }
}