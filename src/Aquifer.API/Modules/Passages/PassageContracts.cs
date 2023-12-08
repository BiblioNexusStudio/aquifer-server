using Aquifer.Data.Entities;
using Aquifer.Data.Enums;
using System.Text.Json.Serialization;
using Aquifer.API.Modules.Resources;

namespace Aquifer.API.Modules.Passages;

public class PassagesByBookDto
{
    public BookCode BookCode { get; set; }
    public required IEnumerable<PassageDto> Passages { get; set; }
}

public class PassageDto
{
    public int Id { get; set; }
    public BookCode BookCode => PassageStartDetails.BookId;
    public int StartChapter => PassageStartDetails.Chapter;
    public int EndChapter => PassageEndDetails.Chapter;
    public int StartVerse => PassageStartDetails.Verse;
    public int EndVerse => PassageEndDetails.Verse;

    [JsonIgnore]
    public (BookCode BookId, int Chapter, int Verse) PassageStartDetails { get; set; }

    [JsonIgnore]
    public (BookCode BookId, int Chapter, int Verse) PassageEndDetails { get; set; }
}

public class PassageWithResourcesMetadataDto : PassageDto
{
    public required IEnumerable<ResourceItemDto> Contents { get; set; }
}