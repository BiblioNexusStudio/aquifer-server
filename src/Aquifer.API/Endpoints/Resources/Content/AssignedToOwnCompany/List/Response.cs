using System.Text.Json.Serialization;
using Aquifer.API.Common.Dtos;

namespace Aquifer.API.Endpoints.Resources.Content.AssignedToOwnCompany.List;

public class Response
{
    public required int Id { get; set; }
    public required string EnglishLabel { get; set; }
    public required string ParentResourceName { get; set; }
    public required string LanguageEnglishDisplay { get; set; }
    public required UserDto AssignedUser { get; set; }
    public required int? WordCount { get; set; }
    public required string Status { get; set; }

    public int? DaysUntilProjectDeadline => ProjectProjectedDeliveryDate.HasValue
        ? (ProjectProjectedDeliveryDate.Value.ToDateTime(new TimeOnly(23, 59)) - DateTime.UtcNow).Days
        : null;

    public required string? ProjectName { get; set; }

    [JsonIgnore]
    public DateOnly? ProjectProjectedDeliveryDate { get; set; }
}