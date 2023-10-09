using System.Text.Json.Serialization;

namespace Aquifer.API.Modules.Resources.ResourcesSummary;

public record ResourcesSummaryByTypeDto : ResourcesSummaryDtoCommon
{
    public int Status { get; set; }
}

public record ResourcesSummaryByLanguageDto : ResourcesSummaryDtoCommon
{
    public string LanguageName { get; set; } = null!;
}

public record ResourcesSummaryDtoCommon
{
    public string ResourceType { get; set; } = null!;
    public DateTime Date { get; set; }
    public int ResourceCount { get; set; }
}

public record ResourcesSummaryResponse(
    List<ResourcesSummaryByType> ResourcesByType,
    List<ResourcesSummaryByLanguage> ResourcesByLanguage,
    List<ResourcesSummaryTypeTotalsByMonth> TotalsByMonth,
    int AllResourcesCount,
    int MultiLanguageResourcesCount,
    List<string> Languages,
    List<string> ResourceTypes
);

public record ResourcesSummaryByType(int ResourceCount,
    string ResourceType,
    [property: JsonIgnore]
    DateTime FullDateTime)
{
    public DateOnly Date { get; init; } = DateOnly.FromDateTime(FullDateTime);
    public string MonthAbbreviation { get; init; } = FullDateTime.ToString("MMM");
}

public record ResourcesSummaryByLanguage(string Language,
        int ResourceCount,
        string ResourceType,
        [property: JsonIgnore]
        DateTime FullDateTime)
    : ResourcesSummaryByType(ResourceCount, ResourceType, FullDateTime);

public record ResourcesSummaryTypeTotalsByMonth(DateOnly Date, string MonthAbbreviation, int ResourceCount);