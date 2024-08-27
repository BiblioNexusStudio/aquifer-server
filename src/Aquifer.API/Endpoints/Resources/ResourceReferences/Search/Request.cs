namespace Aquifer.API.Endpoints.Resources.ResourceReferences.Search;

public record Request
{
    public int ParentResourceId { get; set; }
    public string? Query { get; set; }
}