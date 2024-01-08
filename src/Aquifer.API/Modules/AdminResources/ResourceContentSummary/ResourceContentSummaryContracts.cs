using Aquifer.API.Common;
using Aquifer.API.Utilities;
using Aquifer.Data.Entities;
using System.Text.Json.Serialization;

namespace Aquifer.API.Modules.AdminResources.ResourceContentSummary;

public class ResourceContentSummaryById : ResourceContentSummaryDetailsById
{
    public IEnumerable<ResourceContentSummaryVerseById> VerseReferences { get; set; } = null!;
    public IEnumerable<ResourceContentSummaryPassageById> PassageReferences { get; set; } = null!;
    public IEnumerable<ResourceContentSummaryAssociatedContentById> AssociatedResources { get; set; } = null!;
    public int ResourceContentId { get; set; }
    public bool HasAudio { get; set; }
    public ResourceContentMediaType MediaType { get; set; }
    public ResourceContentStatus Status { get; set; }
    public string Language { get; set; } = null!;
    public IEnumerable<ResourceContentSummaryVersion> ContentVersions { get; set; } = null!;
    public IEnumerable<ResourceContentSummaryContentIdWithLanguageId> OtherLanguageContentIds { get; set; } = null!;

    [JsonIgnore]
    public int ResourceId { get; set; }
}

public class ResourceContentSummaryVersion
{
    public string DisplayName { get; set; } = null!;
    public ResourceContentSummaryAssignedUser? AssignedUser { get; set; }

    [JsonIgnore]
    public string ContentValue { get; set; } = null!;

    [JsonIgnore]
    public int Version { get; set; }

    public object Content => JsonUtilities.DefaultDeserialize(ContentValue);
    public int ContentSize { get; set; }
    public bool IsDraft { get; set; }
    public bool IsPublished { get; set; }
}

public class ResourceContentSummaryDetailsById
{
    public string ParentResourceName { get; set; } = null!;
    public string Label { get; set; } = null!;
}

public class ResourceContentSummaryAssociatedContentById : ResourceContentSummaryDetailsById
{
    public IEnumerable<ResourceContentMediaType> MediaTypes { get; set; } = null!;
}

public class ResourceContentSummaryVerseById
{
    public int VerseId { get; init; }

    private (Data.Enums.BookId BookId, int Chapter, int Verse) TranslatedVerse =>
        BibleUtilities.TranslateVerseId(VerseId);

    public string Book => BookCodes.FullNameFromId(TranslatedVerse.BookId);
    public int Chapter => TranslatedVerse.Chapter;
    public int Verse => TranslatedVerse.Verse;
}

public class ResourceContentSummaryPassageById
{
    public int StartVerseId { get; init; }

    private (Data.Enums.BookId BookId, int Chapter, int Verse) StartTranslatedVerse =>
        BibleUtilities.TranslateVerseId(StartVerseId);

    public string StartBook => BookCodes.FullNameFromId(StartTranslatedVerse.BookId);
    public int StartChapter => StartTranslatedVerse.Chapter;
    public int StartVerse => StartTranslatedVerse.Verse;
    public int EndVerseId { get; init; }

    private (Data.Enums.BookId BookId, int Chapter, int Verse) EndTranslatedVerse =>
        BibleUtilities.TranslateVerseId(EndVerseId);

    public string EndBook => BookCodes.FullNameFromId(EndTranslatedVerse.BookId);
    public int EndChapter => EndTranslatedVerse.Chapter;
    public int EndVerse => EndTranslatedVerse.Verse;
}

public class ResourceContentSummaryAssignedUser
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
}

public class ResourceContentSummaryContentIdWithLanguageId
{
    public int ContentId { get; init; }
    public int LanguageId { get; init; }
}

public class ResourceContentSummaryItemUpdate
{
    public List<object> Content { get; init; } = new();
    public string DisplayName { get; init; } = null!;
}