using Aquifer.API.Utilities;
using Aquifer.Data.Entities;
using Aquifer.Data.Enums;
using System.Text.Json.Serialization;

namespace Aquifer.API.Modules.Resources.ResourcesSummary;

public class ResourcesSummaryByTypeDto : ResourcesSummaryDtoCommon
{
}

public class ResourcesSummaryByLanguageDto : ResourcesSummaryDtoCommon
{
    public string LanguageName { get; set; } = null!;
}

public class ResourcesSummaryDtoCommon
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
        DateTime FullDateTime)
    : ResourcesSummaryByType(ResourceCount, ResourceType, FullDateTime);

public record ResourcesSummaryTypeTotalsByMonth(DateOnly Date, string MonthAbbreviation, int ResourceCount);

public record ResourcesSummaryById : ResourcesSummaryDetailsById
{
    public IEnumerable<ResourcesSummaryVerseById> VerseReferences { get; set; } = null!;
    public IEnumerable<ResourcesSummaryPassageById> PassageReferences { get; set; } = null!;
    public IEnumerable<ResourcesSummaryContentById> Resources { get; set; } = null!;
    public IEnumerable<ResourcesSummaryAssociatedContentById> AssociatedResources { get; set; } = null!;
}

public record ResourcesSummaryDetailsById
{
    public string Type { get; set; } = null!;
    public string Label { get; set; } = null!;
}

public record ResourcesSummaryAssociatedContentById : ResourcesSummaryDetailsById
{
    public IEnumerable<ResourceContentMediaType> MediaTypes { get; set; } = null!;
}

public record ResourcesSummaryContentById
{
    public int ResourceContentId { get; set; }
    public string DisplayName { get; init; } = null!;
    public ResourceContentMediaType MediaType { get; init; }
    public ResourceContentStatus Status { get; init; }
    public int ContentSize { get; init; }
    public ResourcesSummaryLanguageById Language { get; init; } = null!;
    public object Content { get; init; } = null!;
}

public record ResourcesSummaryVerseById
{
    public int VerseId { get; init; }

    private (BookId BookId, int Chapter, int Verse) TranslatedVerse =>
        BibleUtilities.TranslateVerseId(VerseId);

    public string Book => TranslatedVerse.BookId.ToCode();
    public int Chapter => TranslatedVerse.Chapter;
    public int Verse => TranslatedVerse.Verse;
}

public record ResourcesSummaryPassageById
{
    public int StartVerseId { get; init; }

    private (BookId BookId, int Chapter, int Verse) StartTranslatedVerse =>
        BibleUtilities.TranslateVerseId(StartVerseId);

    public string StartBook => StartTranslatedVerse.BookId.ToCode();
    public int StartChapter => StartTranslatedVerse.Chapter;
    public int StartVerse => StartTranslatedVerse.Verse;
    public int EndVerseId { get; init; }
    private (BookId BookId, int Chapter, int Verse) EndTranslatedVerse => BibleUtilities.TranslateVerseId(EndVerseId);
    public string EndBook => EndTranslatedVerse.BookId.ToCode();
    public int EndChapter => EndTranslatedVerse.Chapter;
    public int EndVerse => EndTranslatedVerse.Verse;
}

public record ResourcesSummaryLanguageById
{
    public int Id { get; init; }
    public string DisplayName { get; init; } = null!;
}

public record ResourcesSummaryItemUpdate
{
    public ResourcesSummaryTiptapContent TiptapContent { get; init; } = null!;
    public ResourceContentStatus Status { get; init; }
    public string Label { get; init; } = null!;
}

public class ResourcesSummaryTiptapContent
{
    public int? StepNumber { get; init; }
    public string Tiptap { get; set; } = null!;
}