namespace Aquifer.API.Endpoints.Resources.Search;

public record Response
{
    public required int Id { get; set; }
    public required string DisplayName { get; set; }
    public required string MediaType { get; set; }
    public required int ParentResourceId { get; set; }
    public required string ResourceType { get; set; }
}