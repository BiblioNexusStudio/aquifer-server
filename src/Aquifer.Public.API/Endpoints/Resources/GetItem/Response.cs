using System.Text.Json.Serialization;
using Aquifer.Common.Utilities;
using Aquifer.Data.Entities;

namespace Aquifer.Public.API.Endpoints.Resources.GetItem;

public class Response
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string LocalizedName { get; set; } = null!;

    [JsonIgnore]
    public string ContentValue { get; set; } = null!;

    public object Content => JsonUtilities.DefaultDeserialize(ContentValue);

    public ResourceTypeMetadata Grouping { get; set; } = null!;
    public ResourceContentLanguage Language { get; set; } = null!;
}

public class ResourceTypeMetadata
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ResourceType Type { get; init; }

    public string Name { get; set; } = null!;

    [JsonIgnore]
    public string? LicenseInfoValue { get; set; } = null!;

    public object? LicenseInfo => LicenseInfoValue is null ? null : JsonUtilities.DefaultDeserialize(LicenseInfoValue);
}

public class ResourceContentLanguage
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public ScriptDirection ScriptDirection { get; set; }
}