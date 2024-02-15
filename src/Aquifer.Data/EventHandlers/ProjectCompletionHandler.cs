using Aquifer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Aquifer.Data.EventHandlers;

public static class ProjectCompletionHandler
{
    public static async Task HandleAsync(DbContextOptions<AquiferDbContext> dbContextOptions, IEnumerable<EntityEntry> entityEntries)
    {
        List<int> resourceContentIds = [];
        entityEntries.Where(entry => entry is { State: EntityState.Unchanged, Entity: ResourceContentEntity })
            .Select(x => x.Entity as ResourceContentEntity).ToList().ForEach(x =>
            {
                if (x?.Status == ResourceContentStatus.Complete)
                {
                    resourceContentIds.Add(x.Id);
                }
            });

        if (resourceContentIds.Count > 0)
        {
            await using var dbContext = new AquiferDbContext(dbContextOptions);
            await dbContext.Projects.Where(x =>
                    x.ResourceContents.Any(rc => resourceContentIds.Contains(rc.Id)) &&
                    x.ResourceContents.Where(rc => !resourceContentIds.Contains(rc.Id))
                        .All(rc => rc.Status == ResourceContentStatus.Complete))
                .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.ActualPublishDate, DateOnly.FromDateTime(DateTime.UtcNow))
                    .SetProperty(p => p.Updated, DateTime.UtcNow));
        }
    }
}