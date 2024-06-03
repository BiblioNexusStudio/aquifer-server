using System.Text.Json.Serialization;

namespace Aquifer.API.Endpoints.Resources.Content.ReviewPending.List;

public class Response
{
    public required int Id { get; set; }
    public required string EnglishLabel { get; set; }
    public required string ParentResourceName { get; set; }
    public required string LanguageEnglishDisplay { get; set; }
    public required int SortOrder { get; set; }

    public int DaysSinceStatusChange => (DateTime.UtcNow - LastStatusUpdate).Days;

    public required int? WordCount { get; set; }

    [JsonIgnore]
    public DateTime LastStatusUpdate { get; set; }
}