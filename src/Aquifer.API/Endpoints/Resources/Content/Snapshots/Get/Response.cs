using System.Text.Json.Serialization;
using Aquifer.Common.Utilities;

namespace Aquifer.API.Endpoints.Resources.Content.Snapshots.Get;

public record Response
{
    public required int Id { get; set; }
    public required string DisplayName { get; set; }
    public required int? WordCount { get; set; }
    public required DateTime Created { get; set; }
    public required string? AssignedUserName { get; set; }
    public required string Status { get; set; }

    [JsonIgnore]
    public string ContentValue { get; set; } = null!;

    public object Content => JsonUtilities.DefaultDeserialize(ContentValue);
}