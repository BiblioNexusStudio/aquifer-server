using Aquifer.API.Common;
using Aquifer.API.Utilities;
using Aquifer.Data.Entities;
using Aquifer.Data.Enums;
using System.Text.Json.Serialization;

namespace Aquifer.API.Modules.Resources.ResourceContentSummary;

public record ResourceContentSummaryById : ResourceContentSummaryDetailsById
{
    public IEnumerable<ResourceContentSummaryVerseById> VerseReferences { get; set; } = null!;
    public IEnumerable<ResourceContentSummaryPassageById> PassageReferences { get; set; } = null!;
    public IEnumerable<ResourceContentSummaryAssociatedContentById> AssociatedResources { get; set; } = null!;
    public int ResourceContentId { get; set; }
    public string DisplayName { get; init; } = null!;
    public bool HasAudio { get; init; }
    public ResourceContentMediaType MediaType { get; init; }
    public ResourceContentStatus Status { get; init; }
    public int ContentSize { get; init; }
    public string Language { get; init; } = null!;
    public bool IsPublished { get; init; }
    public ResourceContentSummaryAssignedUser? AssignedUser { get; init; }
    public object Content { get; init; } = null!;
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

    private (BookId BookId, int Chapter, int Verse) TranslatedVerse =>
        BibleUtilities.TranslateVerseId(VerseId);

    public string Book => TranslatedVerse.BookId.ToCode();
    public int Chapter => TranslatedVerse.Chapter;
    public int Verse => TranslatedVerse.Verse;
}

public record ResourceContentSummaryPassageById
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
    public string Label => 
                    StartTranslatedVerse.BookId == EndTranslatedVerse.BookId 
                        ? StartChapter == EndChapter 
                            ? string.Format("{0} {1}:{2}-{3}", StartTranslatedVerse.BookId.GetDisplayName(), StartChapter, StartVerse, EndVerse)
                            : string.Format("{0} {1}:{2}-{3}:{4}", StartTranslatedVerse.BookId.GetDisplayName(), StartChapter, StartVerse, EndChapter, EndVerse)
                        : string.Format("{0} {1}:{2} - ${3} {4}:{5}", StartTranslatedVerse.BookId.GetDisplayName(), StartChapter, StartVerse, EndTranslatedVerse.BookId.GetDisplayName(), EndChapter, EndVerse);
                        
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