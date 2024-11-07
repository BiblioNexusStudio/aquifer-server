namespace Aquifer.API.Endpoints.Resources.Content.PullFromPublisherReview;

public record Request
{
    public int? ContentId { get; set; }
    public int[]? ContentIds { get; set; }
    public int AssignedUserId { get; set; }
}