using Aquifer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Aquifer.Data.EventHandlers;

public static class ProjectCompletionHandler
{
    public static void SetProjectCompletionStatus(DbSet<ProjectEntity> projects,
        ChangeTracker changeTracker,
        IEnumerable<EntityEntry> entityEntries)
    {
        List<int> resourceContentIds = [];
        entityEntries.Where(entry => entry is { State: EntityState.Modified, Entity: IProjectCompletion })
            .Select(x => x.Entity as IProjectCompletion).ToList().ForEach(x =>
            {
                if (x?.Status == ResourceContentStatus.Complete)
                {
                    resourceContentIds.Add(x.Id);
                }
            });

        if (resourceContentIds.Count > 0)
        {
            projects.Where(x =>
                    x.ResourceContents.Any(rc => resourceContentIds.Contains(rc.Id)) &&
                    x.ResourceContents.Where(rc => !resourceContentIds.Contains(rc.Id))
                        .All(rc => rc.Status == ResourceContentStatus.Complete))
                .ExecuteUpdate(x => x.SetProperty(p => p.ActualPublishDate, p => DateOnly.FromDateTime(DateTime.UtcNow)));
        }
    }
}

public interface IProjectCompletion
{
    public int Id { get; set; }
    public ResourceContentStatus Status { get; set; }
}