namespace Aquifer.API.Endpoints.Resources.Content.NotApplicable.List;

public class Response
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string ParentResourceName { get; set; }
    public required string Language { get; set; }
    public string? ProjectName { get; set; }
}