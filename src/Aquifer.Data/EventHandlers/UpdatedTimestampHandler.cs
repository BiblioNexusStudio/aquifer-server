using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Aquifer.Data.EventHandlers;

public static class UpdatedTimestampHandler
{
    public static void Handle(EntityEntry entityEntry)
    {
        if (entityEntry is { Entity: IHasUpdatedTimestamp entity, State: EntityState.Modified })
        {
            entity.Updated = DateTime.UtcNow;
        }
    }
}

public interface IHasUpdatedTimestamp
{
    DateTime Updated { get; set; }
}