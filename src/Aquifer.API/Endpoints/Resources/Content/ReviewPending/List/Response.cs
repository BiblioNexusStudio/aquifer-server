using System.Text.Json.Serialization;

namespace Aquifer.API.Endpoints.Resources.Content.ReviewPending.List;

public class Response
{
    public required int Id { get; set; }
    public required string EnglishLabel { get; set; }
    public required string ParentResourceName { get; set; }
    public required string LanguageEnglishDisplay { get; set; }

    public int DaysSinceStatusChange => HistoryCreated is null ? 0 : (DateTime.UtcNow - HistoryCreated.Value).Days;

    public required int? WordCount { get; set; }

    [JsonIgnore]
    public DateTime? HistoryCreated { get; set; }
}