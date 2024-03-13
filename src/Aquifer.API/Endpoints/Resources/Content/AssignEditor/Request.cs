namespace Aquifer.API.Endpoints.Resources.Content.AssignEditor;

public record Request
{
    public int ContentId { get; set; }
    public int AssignedUserId { get; set; }
}