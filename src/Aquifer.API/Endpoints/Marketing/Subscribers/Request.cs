namespace Aquifer.API.Endpoints.Marketing.Subscribers;

public class Request
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Organization { get; set; }
    public bool GetNewsletter { get; set; }
    public List<int> SelectedLanguageIds { get; set; } = [];
    public List<int> SelectedParentResourceIds { get; set; } = [];
}