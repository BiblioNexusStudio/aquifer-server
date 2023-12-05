using Aquifer.API.Utilities;
using Aquifer.Data.Entities;
using Aquifer.Data.Enums;
using System.Text.Json.Serialization;

namespace Aquifer.API.Modules.Resources.ResourcesSummary;

public class ResourcesSummaryByParentResourceDto : ResourcesSummaryDtoCommon
{
}

public class ResourcesSummaryByLanguageDto : ResourcesSummaryDtoCommon
{
    public string LanguageName { get; set; } = null!;
}

public class ResourcesSummaryDtoCommon
{
    public string ParentResourceName { get; set; } = null!;
    public DateTime Date { get; set; }
    public int ResourceCount { get; set; }
}

public record ResourcesSummaryResponse(
    List<ResourcesSummaryByParentResource> ResourcesByParentResource,
    List<ResourcesSummaryByLanguage> ResourcesByLanguage,
    List<ResourcesSummaryParentResourceTotalsByMonth> TotalsByMonth,
    int AllResourcesCount,
    int MultiLanguageResourcesCount,
    List<string> Languages,
    List<string> ParentResourceNames
);

public record ResourcesSummaryByParentResource(int ResourceCount,
    string ParentResourceName,
    [property: JsonIgnore]
    DateTime FullDateTime)
{
    public DateOnly Date { get; init; } = DateOnly.FromDateTime(FullDateTime);
    public string MonthAbbreviation { get; init; } = FullDateTime.ToString("MMM");
}

public record ResourcesSummaryByLanguage(string Language,
        int ResourceCount,
        string ParentResourceName,
        DateTime FullDateTime)
    : ResourcesSummaryByParentResource(ResourceCount, ParentResourceName, FullDateTime);

public record ResourcesSummaryParentResourceTotalsByMonth(DateOnly Date, string MonthAbbreviation, int ResourceCount);