using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Notifications.BulkUpdate;

public sealed class Request
{
    public IReadOnlyList<NotificationUpdate> Updates { get; init; } = [];
}

public sealed class NotificationUpdate
{
    public NotificationKind NotificationKind { get; init; }
    public int NotificationKindId { get; init; }
    public bool? IsRead { get; init; }
}