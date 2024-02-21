using System.Text.Json.Serialization;

namespace Aquifer.API.Endpoints.Resources.Content.List.AssignedToSelf;

public record Response
{
    public required int ContentId { get; set; }
    public required string DisplayName { get; set; }
    public required string ParentResourceName { get; set; }
    public required string LanguageEnglishDisplay { get; set; }

    [JsonIgnore]
    public DateTime? HistoryCreated { get; set; }

    public int DaysSinceAssignment => HistoryCreated is null ? 0 : (DateTime.UtcNow - HistoryCreated.Value).Days;

    public required int? WordCount { get; set; }
    public required string Status { get; set; }
}