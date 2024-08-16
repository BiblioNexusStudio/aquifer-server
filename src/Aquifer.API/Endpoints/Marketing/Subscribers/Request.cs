namespace Aquifer.API.Endpoints.Marketing.Subscribers;

public class Request
{
    public string Name { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string? Organization { get; init; }
    public bool GetNewsletter { get; init; }
    public IEnumerable<int> SelectedLanguageIds { get; init; } = [];
    public IEnumerable<int> SelectedParentResourceIds { get; init; } = [];
}