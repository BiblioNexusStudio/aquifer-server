using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Aquifer.Data.EventHandlers;

public static class UpdatedTimestampHandler
{
    public static void SetUpdatedTimestamp(EntityEntry entityEntry)
    {
        if (entityEntry is { Entity: IHasUpdatedTimestamp entity, State: EntityState.Modified })
        {
            entity.Updated = DateTime.UtcNow;
        }
    }
}

public interface IHasUpdatedTimestamp
{
    public DateTime Updated { get; set; }
}