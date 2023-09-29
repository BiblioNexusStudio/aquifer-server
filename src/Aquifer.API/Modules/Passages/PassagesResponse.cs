using Aquifer.Data.Entities;
using System.Text.Json.Serialization;

namespace Aquifer.API.Modules.Passages;

public class PassagesBookResponse
{
    public int BookId { get; set; }
    public required IEnumerable<PassagesResponsePassage> Passages { get; set; }
}

public class PassagesResponsePassage
{
    public int Id { get; set; }
    public int BookId => PassageStartDetails.BookId;
    public int StartChapter => PassageStartDetails.Chapter;
    public int EndChapter => PassageEndDetails.Chapter;
    public int StartVerse => PassageStartDetails.Verse;
    public int EndVerse => PassageEndDetails.Verse;

    [JsonIgnore]
    public (int BookId, int Chapter, int Verse) PassageStartDetails { get; set; }

    [JsonIgnore]
    public (int BookId, int Chapter, int Verse) PassageEndDetails { get; set; }
}

public class PassageDetailsResponse : PassagesResponsePassage
{
    public required IEnumerable<PassageDetailsResponseContent> Contents { get; set; }
}

public class PassageDetailsResponseContent
{
    public int ContentId { get; set; }
    public ResourceEntityType TypeName { get; set; }
    public ResourceContentMediaType MediaTypeName { get; set; }
    public int ContentSize { get; set; }
}