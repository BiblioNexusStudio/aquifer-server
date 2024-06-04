using System.Text.Json.Serialization;
using Aquifer.API.Common.Dtos;

namespace Aquifer.API.Endpoints.Resources.Content.AssignedToOwnCompany.List;

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

    public UserDto AssignedUser => new() { Id = UserId, Name = $"{UserFirstName} {UserLastName}" };

    public int? DaysSinceContentEdit => ContentEdited == null ? null : (DateTime.UtcNow - (DateTime)ContentEdited).Days;

    [JsonIgnore]
    public DateTime? ContentEdited { get; set; }

    [JsonIgnore]
    public DateOnly? ProjectProjectedDeliveryDate { get; set; }

    [JsonIgnore]
    public string UserFirstName { get; set; } = null!;

    [JsonIgnore]
    public string UserLastName { get; set; } = null!;

    [JsonIgnore]
    public int UserId { get; set; }
}