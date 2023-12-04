using Aquifer.Data.Entities;
using System.Text.Json.Serialization;

namespace Aquifer.API.Modules.Resources.ResourceContents;

public class ResourceContentsStatusResponse
{
    [JsonConverter(typeof(JsonNumberEnumConverter<ResourceContentStatus>))]
    public ResourceContentStatus Status { get; init; }
    public string DisplayName { get; init; } = null!;
}