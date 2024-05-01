namespace Aquifer.API.Endpoints.Resources.Content.Update;

public record Request
{
    public int ContentId { get; set; }
    public List<object>? Content { get; init; }
    public string? DisplayName { get; init; } = null!;
    public int? WordCount { get; init; }
}