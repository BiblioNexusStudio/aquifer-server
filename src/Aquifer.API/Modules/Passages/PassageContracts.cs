using System.Text.Json.Serialization;
using Aquifer.API.Common;
using Aquifer.API.Modules.Resources;
using Aquifer.Data.Enums;

namespace Aquifer.API.Modules.Passages;

public class PassagesByBookResponse
{
    public required string BookCode { get; set; }
    public required IEnumerable<PassageResponse> Passages { get; set; }
}

public class PassageResponse
{
    public int Id { get; set; }
    public string BookCode => BookCodes.CodeFromId(PassageStartDetails.BookId);
    public int StartChapter => PassageStartDetails.Chapter;
    public int EndChapter => PassageEndDetails.Chapter;
    public int StartVerse => PassageStartDetails.Verse;
    public int EndVerse => PassageEndDetails.Verse;

    [JsonIgnore]
    public (BookId BookId, int Chapter, int Verse) PassageStartDetails { get; set; }

    [JsonIgnore]
    public (BookId BookId, int Chapter, int Verse) PassageEndDetails { get; set; }
}

public class PassageWithResourceItemsResponse : PassageResponse
{
    public required IEnumerable<ResourceItemResponse> Contents { get; set; }
}