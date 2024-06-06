using System.Text.Json.Serialization;

namespace Aquifer.API.Endpoints.Resources.Content.ToAssign.List;

public class Response
{
    public required int Id { get; set; }
    public required string EnglishLabel { get; set; }
    public required string ParentResourceName { get; set; }
    public required string LanguageEnglishDisplay { get; set; }
    public required int? WordCount { get; set; }
    public required string ProjectName { get; set; }
    public required int SortOrder { get; set; }

    public int? DaysUntilProjectDeadline =>
        ProjectProjectedDeliveryDate == null
            ? null
            : (ProjectProjectedDeliveryDate.Value.ToDateTime(new TimeOnly(23, 59)) - DateTime.UtcNow).Days;

    [JsonIgnore]
    public DateOnly? ProjectProjectedDeliveryDate { get; set; }
}