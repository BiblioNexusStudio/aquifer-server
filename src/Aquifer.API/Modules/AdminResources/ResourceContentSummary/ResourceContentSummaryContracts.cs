namespace Aquifer.API.Modules.AdminResources.ResourceContentSummary;

public class ResourceContentSummaryItemUpdate
{
    public List<object>? Content { get; init; }
    public string DisplayName { get; init; } = null!;
    public int? WordCount { get; init; }
}