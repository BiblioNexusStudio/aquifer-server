using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Projects;

public static class ProjectResourceStatusCountHelper
{
    private static async Task<List<ProjectResourceSourceWordCount>> GetCountsPerProjectAsync(
        IReadOnlyList<int>? projectIds,
        AquiferDbContext dbContext,
        CancellationToken ct)
    {
        var selectQuery = $"""
                           SELECT
                               SUM(RCVD.SourceWordCount) AS WordCount,
                               RC.Status,
                               PRC.ProjectId
                           FROM ResourceContents RC
                           JOIN ProjectResourceContents PRC ON RC.Id = PRC.ResourceContentId
                           LEFT JOIN
                           (
                               SELECT *
                               FROM
                               (
                                   SELECT
                                       *,
                                       ROW_NUMBER() OVER (PARTITION BY ResourceContentId ORDER BY [Version] DESC) AS LatestVersionRank
                                   FROM ResourceContentVersions
                               ) x
                               WHERE x.LatestVersionRank = 1
                           ) RCVD ON RCVD.ResourceContentId = RC.Id
                           {(projectIds is { Count: > 0 } ? $"WHERE PRC.ProjectId IN ({string.Join(", ", projectIds)})" : "")}
                           GROUP BY ProjectId, Status
                           """;
        return await dbContext.Database
            .SqlQueryRaw<ProjectResourceSourceWordCount>(selectQuery)
            .ToListAsync(ct);
    }

    public static async Task<Dictionary<int, ProjectResourceStatusCounts>> GetResourceStatusCountsPerProjectAsync(
        IReadOnlyList<int>? projectIds,
        AquiferDbContext dbContext,
        CancellationToken ct)
    {
        return (await GetCountsPerProjectAsync(projectIds, dbContext, ct))
            .GroupBy(c => c.ProjectId)
            .ToDictionary(
                grp => grp.Key,
                grp => new ProjectResourceStatusCounts(grp.Select(c => (c.Status, c.WordCount)).ToList()));
    }

    private record ProjectResourceSourceWordCount
    {
        public int? WordCount { get; set; }
        public ResourceContentStatus Status { get; set; }
        public int ProjectId { get; set; }
    }
}