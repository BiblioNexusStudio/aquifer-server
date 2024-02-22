using System.Text.Json.Serialization;
using Aquifer.Common.Utilities;
using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Resources.Content.Get;

public class Response
{
    public required string ParentResourceName { get; set; }
    public required string EnglishLabel { get; set; }
    public IEnumerable<VerseReferenceResponse> VerseReferences { get; set; } = null!;
    public IEnumerable<PassageReferenceResponse> PassageReferences { get; set; } = null!;
    public required IEnumerable<AssociatedContentResponse> AssociatedResources { get; set; }
    public required int ResourceContentId { get; set; }
    public required bool HasAudio { get; set; }
    public required ResourceContentMediaType MediaType { get; set; }
    public required ResourceContentStatus Status { get; set; }
    public required LanguageResponse Language { get; set; }
    public IEnumerable<VersionResponse> ContentVersions { get; set; } = null!;
    public required IEnumerable<TranslationResponse> ContentTranslations { get; set; }

    public ProjectResponse? Project =>
        ProjectEntity == null ? null : new ProjectResponse { Id = ProjectEntity.Id, Name = ProjectEntity.Name };

    [JsonIgnore]
    public int ResourceId { get; set; }

    [JsonIgnore]
    public ProjectEntity? ProjectEntity { get; set; }
}

public class LanguageResponse
{
    public required string EnglishDisplay { get; set; }

    [JsonPropertyName("iso6393Code")]
    public required string ISO6393Code { get; set; }
}

public class VersionResponse
{
    public required string DisplayName { get; set; }
    public required UserResponse? AssignedUser { get; set; }

    [JsonIgnore]
    public string ContentValue { get; set; } = null!;

    [JsonIgnore]
    public int Version { get; set; }

    public object Content => JsonUtilities.DefaultDeserialize(ContentValue);
    public required int ContentSize { get; set; }
    public required int? WordCount { get; set; }
    public required bool IsDraft { get; set; }
    public required bool IsPublished { get; set; }
}

public class AssociatedContentResponse
{
    [JsonIgnore]
    public ResourceContentEntity? ResourceContent { get; set; }

    public int? ContentId => ResourceContent?.Id;
    public required string ParentResourceName { get; set; }
    public required string EnglishLabel { get; set; }
    public IEnumerable<ResourceContentMediaType> MediaTypes { get; set; } = null!;
}

public class ProjectResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
}

public class VerseReferenceResponse
{
    public required int VerseId { get; init; }

    private (Data.Enums.BookId BookId, int Chapter, int Verse) TranslatedVerse =>
        BibleUtilities.TranslateVerseId(VerseId);

    public string Book => BibleBookCodeUtilities.FullNameFromId(TranslatedVerse.BookId);
    public int Chapter => TranslatedVerse.Chapter;
    public int Verse => TranslatedVerse.Verse;
}

public class PassageReferenceResponse
{
    public required int StartVerseId { get; init; }
    public required int EndVerseId { get; init; }

    private (Data.Enums.BookId BookId, int Chapter, int Verse) StartTranslatedVerse =>
        BibleUtilities.TranslateVerseId(StartVerseId);

    public string StartBook => BibleBookCodeUtilities.FullNameFromId(StartTranslatedVerse.BookId);
    public int StartChapter => StartTranslatedVerse.Chapter;
    public int StartVerse => StartTranslatedVerse.Verse;

    private (Data.Enums.BookId BookId, int Chapter, int Verse) EndTranslatedVerse =>
        BibleUtilities.TranslateVerseId(EndVerseId);

    public string EndBook => BibleBookCodeUtilities.FullNameFromId(EndTranslatedVerse.BookId);
    public int EndChapter => EndTranslatedVerse.Chapter;
    public int EndVerse => EndTranslatedVerse.Verse;
}

public class UserResponse
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required int CompanyId { get; init; }
}

public class TranslationResponse
{
    public required int ContentId { get; init; }
    public required int LanguageId { get; init; }
    public required string Status { get; set; }
    public required bool HasDraft { get; set; }
    public required bool HasPublished { get; set; }
}