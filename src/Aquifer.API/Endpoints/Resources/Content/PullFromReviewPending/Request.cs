namespace Aquifer.API.Endpoints.Resources.Content.PullFromReviewPending;

public record Request
{
    public int? ContentId { get; set; }
    public int[]? ContentIds { get; set; }
    public int AssignedUserId { get; set; }
}