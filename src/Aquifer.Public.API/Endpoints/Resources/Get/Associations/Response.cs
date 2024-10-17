using System.Text.Json.Serialization;
using Aquifer.Common.Utilities;

namespace Aquifer.Public.API.Endpoints.Resources.Get.Associations;

public class Response
{
    public required IReadOnlyList<PassageAssociation> PassageAssociations { get; init; }
    public required IReadOnlyList<ResourceAssociation> ResourceAssociations { get; init; }
}

public class PassageAssociation
{
    [JsonIgnore]
    public int StartVerseId { get; init; }

    [JsonIgnore]
    public int EndVerseId { get; init; }

    [JsonIgnore]
    private (Data.Enums.BookId BookId, int Chapter, int Verse) StartTranslatedVerse =>
        BibleUtilities.TranslateVerseId(StartVerseId);

    public string StartBookCode => BibleBookCodeUtilities.CodeFromId(StartTranslatedVerse.BookId);
    public int StartChapter => StartTranslatedVerse.Chapter;
    public int StartVerse => StartTranslatedVerse.Verse;

    [JsonIgnore]
    private (Data.Enums.BookId BookId, int Chapter, int Verse) EndTranslatedVerse =>
        BibleUtilities.TranslateVerseId(EndVerseId);

    public string EndBookCode => BibleBookCodeUtilities.CodeFromId(EndTranslatedVerse.BookId);
    public int EndChapter => EndTranslatedVerse.Chapter;
    public int EndVerse => EndTranslatedVerse.Verse;
}

public class ResourceAssociation
{
    public required int ContentId { get; init; }
    public required string DisplayName { get; init; }
}