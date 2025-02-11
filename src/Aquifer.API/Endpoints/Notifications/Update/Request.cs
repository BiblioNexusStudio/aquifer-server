using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Notifications.Update;

public sealed class Request
{
    public NotificationKind NotificationKind { get; init; }
    public int NotificationKindId { get; init; }
    public bool? IsRead { get; init; }
}