using System.Text.Json.Serialization;

namespace Aquifer.API.Modules.Resources.ResourcesSummary;

public record ResourcesSummaryResponse(
    List<ResourcesSummaryByParentResourceResponse> ResourcesByParentResource,
    List<ResourcesSummaryByLanguageResponse> ResourcesByLanguage,
    List<ResourcesSummaryParentResourceTotalsByMonthResponse> TotalsByMonth,
    int AllResourcesCount,
    int MultiLanguageResourcesCount,
    List<string> Languages,
    List<string> ParentResourceNames
);

public record ResourcesSummaryByParentResourceResponse(int ResourceCount,
    string ParentResourceName,
    [property: JsonIgnore]
    DateTime FullDateTime)
{
    public DateOnly Date { get; init; } = DateOnly.FromDateTime(FullDateTime);
    public string MonthAbbreviation { get; init; } = FullDateTime.ToString("MMM");
}

public record ResourcesSummaryByLanguageResponse(string Language,
        int ResourceCount,
        string ParentResourceName,
        DateTime FullDateTime)
    : ResourcesSummaryByParentResourceResponse(ResourceCount, ParentResourceName, FullDateTime);

public record ResourcesSummaryParentResourceTotalsByMonthResponse(DateOnly Date, string MonthAbbreviation, int ResourceCount);
