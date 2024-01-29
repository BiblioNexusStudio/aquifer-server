namespace Aquifer.API.Modules.Reports.RequestedResources;

/// <summary>
///     Represents the response for the most requested resources.
/// </summary>
public class MostRequestedResourcesResponse
{
    public int ResourceContentId { get; set; }
    public int Count { get; set; }
    public string EnglishDisplay { get; set; }
    public string EnglishLabel { get; set; }
}