using Aquifer.API.Common.Dtos;
using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Notifications.List;

public sealed class Response
{
    public required NotificationKind Kind { get; init; }
    public required CommentNotification? Comment { get; init; }
    public required HelpDocumentNotification? HelpDocument { get; init; }
    public required bool IsRead { get; init; }
}

public sealed class CommentNotification
{
    public required int Id { get; init; }
    public required string Text { get; init; }
    public required DateTime Created { get; init; }
    public required UserDto User { get; init; }
    public required int ResourceContentId { get; init; }
    public required string ResourceEnglishLabel { get; init; }
    public required string ParentResourceDisplayName { get; init; }
}

public sealed class HelpDocumentNotification
{
    public required int Id { get; init; }
    public required string Title { get; init; }
    public required DateTime Created { get; init; }
    public required HelpDocumentType Type { get; init; }
    public required string Url { get; init; }
    public string? ThumbnailUrl { get; init; }
}