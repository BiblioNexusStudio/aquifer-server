using System.Text.Json.Serialization;
using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Resources.Content.AssignedToSelf.List;

public record Response
{
    public required int Id { get; set; }
    public required string EnglishLabel { get; set; }
    public required string ParentResourceName { get; set; }
    public required string LanguageEnglishDisplay { get; set; }
    public required int? WordCount { get; set; }
    public required string Status { get; set; }

    public int DaysSinceAssignment => HistoryCreated is null ? 0 : (DateTime.UtcNow - HistoryCreated.Value).Days;

    public int? DaysUntilProjectDeadline => ProjectEntity?.ProjectedDeliveryDate == null
        ? null
        : (ProjectEntity.ProjectedDeliveryDate.Value.ToDateTime(new TimeOnly(23, 59)) - DateTime.UtcNow).Days;

    public string? ProjectName => ProjectEntity?.Name;

    [JsonIgnore]
    public ProjectEntity? ProjectEntity { get; set; }

    [JsonIgnore]
    public DateTime? HistoryCreated { get; set; }
}