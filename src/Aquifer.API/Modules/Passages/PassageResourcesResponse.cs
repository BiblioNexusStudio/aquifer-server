using System.Text.Json.Serialization;

namespace Aquifer.API.Modules.Passages;

public class PassageResourcesResponse
{
    public int BookId => PassageStartDetails.BookId;
    public string BookName => PassageStartDetails.BookName;
    public int StartChapter => PassageStartDetails.Chapter;
    public int EndChapter => PassageEndDetails.Chapter;
    public int StartVerse => PassageStartDetails.Verse;
    public int EndVerse => PassageEndDetails.Verse;

    public IEnumerable<PassageResourcesResponseResource> Resources { get; set; } =
        new List<PassageResourcesResponseResource>();

    [JsonIgnore]
    public (string BookName, int BookId, int Chapter, int Verse) PassageStartDetails { get; set; }

    [JsonIgnore]
    public (string BookName, int BookId, int Chapter, int Verse) PassageEndDetails { get; set; }
}

public class PassageResourcesResponseResource
{
    public int LanguageId { get; set; }
    public string DisplayName { get; set; } = null!;
    public string? Summary { get; set; }
    public object? Content { get; set; }
    public int ContentSize { get; set; }
    public int Type { get; set; }
    public int MediaType { get; set; }
    public string EnglishLabel { get; set; } = null!;
    public string? Tag { get; set; }

    public IEnumerable<PassageResourcesResponseResourceContent> Contents { get; set; } =
        new List<PassageResourcesResponseResourceContent>();
}

public class PassageResourcesResponseResourceContent
{
    public int LanguageId { get; set; }
    public string DisplayName { get; set; } = null!;
    public string? Summary { get; set; }
    public object? Content { get; set; }
    public int ContentSize { get; set; }
}