using System.Text.Json.Serialization;
using Aquifer.API.Common.Dtos;
using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Resources.Content.AssignedToOwnCompany.List;

public class Response
{
    public required int Id { get; set; }
    public required string EnglishLabel { get; set; }
    public required string ParentResourceName { get; set; }
    public required string LanguageEnglishDisplay { get; set; }
    public required UserDto AssignedUser { get; set; }
    public required int? WordCount { get; set; }

    public int? DaysUntilProjectDeadline => ProjectEntity?.ProjectedDeliveryDate == null
        ? null
        : (ProjectEntity.ProjectedDeliveryDate.Value.ToDateTime(new TimeOnly(23, 59)) - DateTime.UtcNow).Days;

    public string? ProjectName => ProjectEntity?.Name;

    [JsonIgnore]
    public ProjectEntity? ProjectEntity { get; set; }
}