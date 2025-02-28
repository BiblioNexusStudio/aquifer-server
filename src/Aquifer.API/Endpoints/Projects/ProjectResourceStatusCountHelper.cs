using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Projects;

public static class ProjectResourceStatusCountHelper
{
    private static async Task<List<ProjectResourceSourceWordCount>> GetCountsPerProjectAsync(
        IEnumerable<int> projectIds,
        AquiferDbContext dbContext,
        CancellationToken ct)
    {
        var query = $"""
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
                     WHERE PRC.ProjectId IN ({string.Join(", ", projectIds)})
                     GROUP BY ProjectId, Status
                     """;

        return await dbContext.Database
            .SqlQueryRaw<ProjectResourceSourceWordCount>(query)
            .ToListAsync(ct);
    }

    public static async Task<Dictionary<int, ProjectResourceStatusCounts>> GetResourceStatusCountsPerProjectAsync(
        List<int> projectIds,
        AquiferDbContext dbContext,
        CancellationToken ct)
    {
        var counts = await GetCountsPerProjectAsync(projectIds, dbContext, ct);
        var countsPerProject = new Dictionary<int, ProjectResourceStatusCounts>();
        foreach (var id in projectIds)
        {
            countsPerProject[id] = new ProjectResourceStatusCounts(counts.Select(pri => (pri.Status, pri.WordCount)));
        }

        return countsPerProject;
    }

    private record ProjectResourceSourceWordCount
    {
        public int? WordCount { get; set; }
        public ResourceContentStatus Status { get; set; }
        public int ProjectId { get; set; }
    }
}