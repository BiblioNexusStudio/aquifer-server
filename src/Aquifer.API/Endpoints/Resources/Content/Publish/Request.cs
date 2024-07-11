namespace Aquifer.API.Endpoints.Resources.Content.Publish;

public record Request
{
    public int? ContentId { get; set; }
    public int[]? ContentIds { get; set; }
    public int? AssignedUserId { get; set; }
    public bool CreateDraft { get; set; } = false;
}