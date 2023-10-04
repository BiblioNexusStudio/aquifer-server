namespace Aquifer.API.Modules.Resources.ResourcesSummary;

public record ResourcesSummaryByTypeDto(
    string ResourceType,
    int Year,
    int Month,
    int ResourceCount
);

public record ResourcesSummaryByLanguageDto(
    string LanguageName,
    string ResourceType,
    int Year,
    int Month,
    int ResourceCount
);

public record ResourcesSummaryResponse(
    List<ResourcesSummaryByTypeDto> ResourcesByType,
    List<ResourcesSummaryByLanguageDto> ResourcesByLanguage,
    int MultiLanguageResourcesCount
);