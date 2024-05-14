using System.Text.Json.Serialization;
using Aquifer.Common.Extensions;
using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Resources.Content.List;

public record Response
{
    public required List<ResourceContentResponse> ResourceContents { get; set; }
    public required int Total { get; set; }
}

public record ResourceContentResponse
{
    public required int Id { get; set; }
    public required string EnglishLabel { get; set; }
    public required string ParentResourceName { get; set; }
    public required string LanguageEnglishDisplay { get; set; }

    public string Status => StatusValue.GetDisplayName();
    public bool IsPublished => IsPublishedValue == 1;

    [JsonIgnore]
    public ResourceContentStatus StatusValue { get; set; }

    [JsonIgnore]
    public int IsPublishedValue { get; set; }
}