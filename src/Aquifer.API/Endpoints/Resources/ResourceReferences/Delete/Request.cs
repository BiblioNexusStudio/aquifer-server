namespace Aquifer.API.Endpoints.Resources.ResourceReferences.Delete;

public record Request
{
    public int ResourceContentId { get; set; }
    public int ReferenceResourceId { get; set; }
}