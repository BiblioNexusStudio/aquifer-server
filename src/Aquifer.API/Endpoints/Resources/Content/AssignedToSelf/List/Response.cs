using Aquifer.API.Common.Dtos;
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
    public required string? ProjectName { get; set; }
    public required string Status { get; set; }
    public required string StatusDisplayName { get; set; }
    public required int DaysSinceAssignment { get; set; }
    public required int? DaysUntilProjectDeadline { get; set; }
    public required int? DaysSinceContentUpdated { get; set; }
    public required UserDto? LastAssignedUser { get; set; }
}