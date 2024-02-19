namespace Aquifer.API.Endpoints.Resources.Content.AssignReview;

public record Request
{
    public int ContentId { get; set; }

    public required int AssignedUserId { get; set; }
}