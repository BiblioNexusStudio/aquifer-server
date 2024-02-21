using System.Text.Json.Serialization;
using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Resources.Content.List.AssignedToOwnCompany;

public record Response
{
    public required int ContentId { get; set; }
    public required string EnglishLabel { get; set; }
    public required string ParentResourceName { get; set; }
    public required string LanguageEnglishDisplay { get; set; }
    public required UserResponse AssignedUser { get; set; }

    [JsonIgnore]
    public ProjectEntity? Project { get; set; }

    public int? DaysUntilDeadline => Project is null || !Project.ProjectedDeliveryDate.HasValue
        ? null
        : (Project.ProjectedDeliveryDate.Value.ToDateTime(new TimeOnly(23, 59)) - DateTime.UtcNow).Days;

    public string? ProjectName => Project?.Name;

    public required int? WordCount { get; set; }
    public required string Status { get; set; }
}

public class UserResponse
{
    public required UserEntity User { get; set; }
    public string Name => $"{User.FirstName} {User.LastName}";
    public int Id => User.Id;
}