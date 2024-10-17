namespace Aquifer.API.Endpoints.Resources.Content.Versions.History;

public record Request
{
    public int VersionId { get; set; }
}