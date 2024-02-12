using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Aquifer.Data.EventHandlers;

public static class UpdatedTimestampHandler
{
    public static void SetUpdatedTimestamp(object sender, EntityEntryEventArgs e)
    {
        if (e.Entry is { Entity: IHasUpdatedTimestamp entity, State: EntityState.Modified })
        {
            entity.Updated = DateTime.UtcNow;
        }
    }
}

public interface IHasUpdatedTimestamp
{
    public DateTime Updated { get; set; }
}