using System.Text.Json.Serialization;

namespace Aquifer.API.Modules.Resources.ResourcesSummary;

public record ResourcesSummaryDto(
    List<ResourcesSummaryByParentResourceDto> ResourcesByParentResource,
    List<ResourcesSummaryByLanguageDto> ResourcesByLanguage,
    List<ResourcesSummaryParentResourceTotalsByMonthDto> TotalsByMonth,
    int AllResourcesCount,
    int MultiLanguageResourcesCount,
    List<string> Languages,
    List<string> ParentResourceNames
);

public record ResourcesSummaryByParentResourceDto(int ResourceCount,
    string ParentResourceName,
    [property: JsonIgnore]
    DateTime FullDateTime)
{
    public DateOnly Date { get; init; } = DateOnly.FromDateTime(FullDateTime);
    public string MonthAbbreviation { get; init; } = FullDateTime.ToString("MMM");
}

public record ResourcesSummaryByLanguageDto(string Language,
        int ResourceCount,
        string ParentResourceName,
        DateTime FullDateTime)
    : ResourcesSummaryByParentResourceDto(ResourceCount, ParentResourceName, FullDateTime);

public record ResourcesSummaryParentResourceTotalsByMonthDto(DateOnly Date, string MonthAbbreviation, int ResourceCount);
