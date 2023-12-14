using Aquifer.API.Common;
using Aquifer.API.Utilities;
using Aquifer.Data.Entities;

namespace Aquifer.API.Modules.Resources.ResourceContentSummary;

public record ResourceContentSummaryById : ResourceContentSummaryDetailsById
{
    public IEnumerable<ResourceContentSummaryVerseById> VerseReferences { get; set; } = null!;
    public IEnumerable<ResourceContentSummaryPassageById> PassageReferences { get; set; } = null!;
    public IEnumerable<ResourceContentSummaryAssociatedContentById> AssociatedResources { get; set; } = null!;
    public int ResourceContentId { get; set; }
    public string DisplayName { get; set; } = null!;
    public bool HasAudio { get; set; }
    public ResourceContentMediaType MediaType { get; set; }
    public ResourceContentStatus Status { get; set; }
    public int ContentSize { get; set; }
    public string Language { get; set; } = null!;
    public bool IsPublished { get; set; }
    public ResourceContentSummaryAssignedUser? AssignedUser { get; set; }
    public object Content { get; set; } = null!;
    public IEnumerable<ResourceContentSummaryContentIdWithLanguageId> OtherLanguageContentIds { get; set; } = null!;
}

public record ResourceContentSummaryDetailsById
{
    public string ParentResourceName { get; set; } = null!;
    public string Label { get; set; } = null!;
}

public record ResourceContentSummaryAssociatedContentById : ResourceContentSummaryDetailsById
{
    public IEnumerable<ResourceContentMediaType> MediaTypes { get; set; } = null!;
}

public record ResourceContentSummaryVerseById
{
    public int VerseId { get; init; }

    private (Data.Enums.BookId BookId, int Chapter, int Verse) TranslatedVerse =>
        BibleUtilities.TranslateVerseId(VerseId);

    public string Book => BookCodes.CodeFromEnum(TranslatedVerse.BookId);
    public int Chapter => TranslatedVerse.Chapter;
    public int Verse => TranslatedVerse.Verse;
}

public record ResourceContentSummaryPassageById
{
    public int StartVerseId { get; init; }

    private (Data.Enums.BookId BookId, int Chapter, int Verse) StartTranslatedVerse =>
        BibleUtilities.TranslateVerseId(StartVerseId);

    public string StartBook => BookCodes.FullNameFromEnum(StartTranslatedVerse.BookId);
    public int StartChapter => StartTranslatedVerse.Chapter;
    public int StartVerse => StartTranslatedVerse.Verse;
    public int EndVerseId { get; init; }

    private (Data.Enums.BookId BookId, int Chapter, int Verse) EndTranslatedVerse =>
        BibleUtilities.TranslateVerseId(EndVerseId);

    public string EndBook => BookCodes.FullNameFromEnum(EndTranslatedVerse.BookId);
    public int EndChapter => EndTranslatedVerse.Chapter;
    public int EndVerse => EndTranslatedVerse.Verse;
                        
}

public record ResourceContentSummaryAssignedUser
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
}

public record ResourceContentSummaryContentIdWithLanguageId
{
    public int ContentId { get; init; }
    public int LanguageId { get; init; }
}

public record ResourceContentSummaryItemUpdate
{
    public List<object> Content { get; init; } = new();
    public ResourceContentStatus Status { get; init; }
    public string DisplayName { get; init; } = null!;
}