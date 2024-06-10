namespace Aquifer.API.Endpoints.Users.Disable;

public class Response
{
    public bool HasError { get; set; }
    public string? Error { get; set; }
    public IEnumerable<DisableUserAssignedResourceResponse>? AssignedResources { get; set; }
}

public class DisableUserAssignedResourceResponse
{
    public int ResourceContentId { get; set; }
    public string DisplayName { get; set; } = null!;
}