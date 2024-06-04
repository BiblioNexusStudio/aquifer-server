using System.Text.Json.Serialization;
using Aquifer.Common.Extensions;
using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Resources.Content.AssignedToSelf.List;

public record Response
{
    public required int Id { get; set; }
    public required string EnglishLabel { get; set; }
    public required string ParentResourceName { get; set; }
    public required string LanguageEnglishDisplay { get; set; }
    public required int? WordCount { get; set; }
    public required ResourceContentStatus StatusValue { get; set; }
    public required int SortOrder { get; set; }
    public string? ProjectName { get; set; }

    public string Status => StatusValue.GetDisplayName();
    public string StatusDisplayName => StatusValue.GetDisplayName();

    public int DaysSinceAssignment => (DateTime.UtcNow - HistoryCreated).Days;

    public int? DaysUntilProjectDeadline => ProjectedDeliveryDate == null
        ? null
        : (ProjectedDeliveryDate.Value.ToDateTime(new TimeOnly(23, 59)) - DateTime.UtcNow).Days;

    public int? DaysSinceContentUpdated => ContentUpdated == null ? null : (DateTime.UtcNow - (DateTime)ContentUpdated).Days;

    [JsonIgnore]
    public DateTime? ContentUpdated { get; set; }

    [JsonIgnore]
    public DateOnly? ProjectedDeliveryDate { get; set; }

    [JsonIgnore]
    public DateTime HistoryCreated { get; set; }
}