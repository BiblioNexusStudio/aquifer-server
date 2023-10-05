namespace Aquifer.API.Modules.Resources.ResourcesSummary;

public record ResourcesSummaryByTypeDto
{
    public string ResourceType { get; set; } = null!;
    public DateTime Date { get; set; }
    public int Status { get; set; }
    public int ResourceCount { get; set; }
}

public record ResourcesSummaryByLanguageDto(
    string LanguageName,
    string ResourceType,
    DateTime Date,
    int ResourceCount
);

public record ResourcesSummaryResponse(
    List<ResourcesSummaryByTypeDto> ResourcesByType,
    List<ResourcesSummaryByLanguageDto> ResourcesByLanguage,
    int allResourcesCount,
    int MultiLanguageResourcesCount
);