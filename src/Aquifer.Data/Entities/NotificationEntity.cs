using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[Index(nameof(UserId), nameof(Kind), nameof(NotificationEntityId), IsUnique = true)]
public sealed class NotificationEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public NotificationKind Kind { get; set; }
    public int NotificationEntityId { get; set; }
    public DateTime Created { get; set; }
    public bool IsRead { get; set; }
}

public enum NotificationKind
{
    None,
    Comment,
    HelpDocument,
}