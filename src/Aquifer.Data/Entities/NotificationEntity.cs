using Aquifer.Data.EventHandlers;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[Index(nameof(UserId), nameof(Kind), nameof(NotificationKindId), IsUnique = true)]
public sealed class NotificationEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public NotificationKind Kind { get; set; }
    public int NotificationKindId { get; set; }
    public bool IsRead { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}

public enum NotificationKind
{
    None,
    Comment,
    HelpDocument,
}