using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Projects;

public interface IProjectWithCount
{
    public ProjectResourceStatusCounts Counts { get; set; }
    public int Id { get; set; }
}

public static class ProjectResourceStatusCountHelper
{
    private static async Task<List<ProjectResourceSourceWordCount>> GetCountsPerProjectAsync(
        AquiferDbContext dbContext,
        CancellationToken ct)
    {
        const string query = """
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
                             GROUP BY [ProjectId], [Status]
                             """;

        return await dbContext.Database.SqlQueryRaw<ProjectResourceSourceWordCount>(query).ToListAsync(ct);
    }

    public static async Task PopulateCountsPerProjectAsync<T>(List<T> projects, AquiferDbContext dbContext, CancellationToken ct)
        where T : class, IProjectWithCount
    {
        var countsEachProject = await GetCountsPerProjectAsync(dbContext, ct);
        foreach (var p in projects)
        {
            var counts = countsEachProject.Where(x => x.ProjectId == p?.Id).ToArray();
            p.Counts = new ProjectResourceStatusCounts
            {
                NotStarted =
                    counts.Where(x => ProjectResourceStatusCounts.NotStartedStatuses.Contains(x.Status)).Sum(x => x.WordCount ?? 0),
                EditorReview =
                    counts.Where(x => ProjectResourceStatusCounts.EditorReviewStatuses.Contains(x.Status)).Sum(x => x.WordCount ?? 0),
                InCompanyReview =
                    counts.Where(x => ProjectResourceStatusCounts.InCompanyReviewStatuses.Contains(x.Status)).Sum(x => x.WordCount ?? 0),
                InPublisherReview =
                    counts.Where(x => ProjectResourceStatusCounts.InPublisherReviewStatuses.Contains(x.Status)).Sum(x => x.WordCount ?? 0),
                Completed = counts.Where(x => ProjectResourceStatusCounts.CompletedStatuses.Contains(x.Status)).Sum(x => x.WordCount ?? 0)
            };
        }
    }

    private record ProjectResourceSourceWordCount
    {
        public int? WordCount { get; set; }
        public ResourceContentStatus Status { get; set; }
        public int ProjectId { get; set; }
    }
}