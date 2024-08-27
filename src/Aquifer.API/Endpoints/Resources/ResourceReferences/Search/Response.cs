using System.Text.Json.Serialization;

namespace Aquifer.API.Endpoints.Resources.ResourceReferences.Search;

public record Response
{
    public required int ResourceId { get; set; }
    public string Label => EnglishDisplayName ?? EnglishLabel;

    [JsonIgnore]
    public string? EnglishDisplayName { get; set; }

    [JsonIgnore]
    public string EnglishLabel { get; set; } = null!;
}