namespace Aquifer.API.Modules.Reports.RequestedResources;

/// <summary>
///     Represents the response for the most requested resources.
/// </summary>
public class MostRequestedResourcesResponse
{
    public string DisplayName { get; set; } = null!;
    public int Count { get; set; } = 0;
    public string EnglishDisplay { get; set; } = null!;
    public string EnglishLabel { get; set; } = null!;
}