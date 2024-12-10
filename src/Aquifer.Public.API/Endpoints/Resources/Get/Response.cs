using System.Text.Json.Serialization;
using Aquifer.Common.Utilities;
using Aquifer.Data.Entities;

namespace Aquifer.Public.API.Endpoints.Resources.Get;

public class Response
{
    public int Id { get; set; }
    public int ReferenceId { get; set; }
    public string Name { get; set; } = null!;
    public string LocalizedName { get; set; } = null!;

    /// <summary>
    /// DB Value (Tiptap string).
    /// </summary>
    [JsonIgnore]
    public string ContentValue { get; set; } = null!;

    /// <summary>
    /// Converted object from Tiptap.
    /// </summary>
    [JsonIgnore]
    public object ContentObject { get; set; } = null!;

    // Manually serialize the content in order to return a string instead of an object
    // which allows auto-generated clients to work correctly.
    [JsonConverter(typeof(JsonUtilities.RawJsonConverter))]
    public string Content => JsonUtilities.DefaultSerialize(ContentObject);

    public ResourceTypeMetadata Grouping { get; set; } = null!;
    public ResourceContentLanguage Language { get; set; } = null!;
}

public class ResourceTypeMetadata
{
    public ResourceType Type { get; init; }

    public string Name { get; set; } = null!;

    [JsonIgnore]
    public ResourceContentMediaType MediaTypeValue { get; set; }

    public string MediaType => MediaTypeValue.ToString();

    [JsonConverter(typeof(JsonUtilities.RawJsonConverter))]
    public required string? LicenseInfo { get; init; }
}

public class ResourceContentLanguage
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public ScriptDirection ScriptDirection { get; set; }
}