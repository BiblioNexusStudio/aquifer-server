namespace Aquifer.API.Modules.Reports.RequestedResources;

public class MostRequestedResourcesResponse
{
    public string Resource { get; set; } = null!;
    public int Count { get; set; } = 0;
    public string Language { get; set; } = null!;
    public string Label { get; set; } = null!;
}