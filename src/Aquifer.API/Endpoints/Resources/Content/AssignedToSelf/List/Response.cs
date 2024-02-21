using System.Text.Json.Serialization;

namespace Aquifer.API.Endpoints.Resources.Content.AssignedToSelf.List;

public record Response
{
    public required int Id { get; set; }
    public required string EnglishLabel { get; set; }
    public required string ParentResourceName { get; set; }
    public required string LanguageEnglishDisplay { get; set; }
    public required int? WordCount { get; set; }
    public required string Status { get; set; }

    public int? DaysUntilProjectDeadline => ProjectProjectedDeliveryDate == null
        ? null
        : (ProjectProjectedDeliveryDate.Value.ToDateTime(new TimeOnly(23, 59)) - DateTime.UtcNow).Days;

    public required string? ProjectName { get; set; }

    public int DaysSinceAssignment => HistoryCreated is null ? 0 : (DateTime.UtcNow - HistoryCreated.Value).Days;

    [JsonIgnore]
    public DateTime? HistoryCreated { get; set; }

    [JsonIgnore]
    public DateOnly? ProjectProjectedDeliveryDate { get; set; }
}