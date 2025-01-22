using Aquifer.API.Common.Dtos;
using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Resources.Content.AssignedToOwnCompany.List;

public class Response
{
    public required int Id { get; init; }
    public required string EnglishLabel { get; init; }
    public required string ParentResourceName { get; init; }
    public required string LanguageEnglishDisplay { get; init; }
    public required int? WordCount { get; init; }
    public required string ProjectName { get; init; }
    public required int SortOrder { get; init; }
    public required ResourceContentStatus StatusValue { get; init; }
    public required string StatusDisplayName { get; init; }
    public required int? DaysUntilProjectDeadline { get; init; }
    public required UserDto AssignedUser { get; init; }
    public required int? DaysSinceContentUpdated { get; init; }
    public required UserDto? LastAssignedUser { get; init; }
    public required bool HasAudio { get; init; }
    public required bool HasUnresolvedCommentThreads { get; init; }
}