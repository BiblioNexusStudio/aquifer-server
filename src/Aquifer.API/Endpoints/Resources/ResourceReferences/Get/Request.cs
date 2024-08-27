namespace Aquifer.API.Endpoints.Resources.ResourceReferences.Get;

public record Request
{
    public int ParentResourceId { get; set; }
    public string? ResourceIdOrExternalId { get; set; }
}