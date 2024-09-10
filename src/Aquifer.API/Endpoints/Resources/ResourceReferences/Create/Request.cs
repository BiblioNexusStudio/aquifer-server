namespace Aquifer.API.Endpoints.Resources.ResourceReferences.Create;

public record Request
{
    public int ResourceContentId { get; set; }
    public int ReferenceResourceId { get; set; }
}