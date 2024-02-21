using System.Text.Json.Serialization;
using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Resources.Content.List.AssignedToSelf;

public record Response
{
    public required int ContentId { get; set; }
    public required string EnglishLabel { get; set; }
    public required string ParentResourceName { get; set; }
    public required string LanguageEnglishDisplay { get; set; }

    [JsonIgnore]
    public DateTime? HistoryCreated { get; set; }

    [JsonIgnore]
    public ProjectEntity? Project { get; set; }

    public int? DaysUntilDeadline => Project is null || !Project.ProjectedDeliveryDate.HasValue
        ? null
        : (Project.ProjectedDeliveryDate.Value.ToDateTime(new TimeOnly(23, 59)) - DateTime.UtcNow).Days;

    public string? ProjectName => Project?.Name;

    public int DaysSinceAssignment => HistoryCreated is null ? 0 : (DateTime.UtcNow - HistoryCreated.Value).Days;

    public required int? WordCount { get; set; }
    public required string Status { get; set; }
}