using System.Text.Json.Serialization;

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

    public string StartBookCode { get; set; } = null!;
    public int StartChapter { get; set; }
    public int StartVerse { get; set; }

    public string EndBookCode { get; set; } = null!;
    public int EndChapter { get; set; }
    public int EndVerse { get; set; }
}

public class ResourceAssociation
{
    public required int ContentId { get; init; }
    public required string DisplayName { get; init; }
    public required int ReferenceId { get; init; }
}