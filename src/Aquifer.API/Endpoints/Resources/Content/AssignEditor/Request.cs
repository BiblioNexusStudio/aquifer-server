namespace Aquifer.API.Endpoints.Resources.Content.AssignEditor;

public record Request
{
    public int? ContentId { get; set; }
    public int[]? ContentIds { get; set; }
    public int AssignedUserId { get; set; }
    public int? AssignedReviewerUserId { get; set; }
}