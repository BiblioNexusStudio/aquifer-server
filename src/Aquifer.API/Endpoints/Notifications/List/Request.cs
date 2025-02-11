using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Notifications.List;

public sealed class Request
{
    public bool? IsRead { get; init; }
    public IReadOnlyList<NotificationKind>? Kinds { get; init; }
    public int Limit { get; init; } = 100;
    public int Offset { get; init; } = 0;
}