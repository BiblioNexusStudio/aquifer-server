using Aquifer.Data.Entities;
using System.Text.Json.Serialization;

namespace Aquifer.API.Modules.AdminResources.ResourcesList;

public record ResourceListItemResponse
{
    public string EnglishLabel { get; set; } = null!;
    public string ParentResourceName { get; set; } = null!;
    public ResourceContentStatus Status { get; set; }
    public IEnumerable<ResourceListItemForLanguageResponse> ContentIdsWithLanguageIds { get; set; } = null!;
}

public record ResourceListItemForLanguageResponse
{
    public int ContentId { get; set; }
    public int LanguageId { get; set; }
}

public record BaseResourceResponse
{
    public int ContentId { get; set; }
    public string DisplayName { get; set; } = null!;
    public string ParentResourceName { get; set; } = null!;

    [JsonIgnore]
    public DateTime? HistoryCreated { get; set; }

    public int WordCount { get; set; }
}

public record ResourceAssignedToSelfResponse : BaseResourceResponse
{
    public string? AssignedUserName { get; set; }
    public int DaysSinceAssignment => HistoryCreated is null ? 0 : (DateTime.UtcNow - HistoryCreated.Value).Days;
}

public record ResourcePendingReviewResponse : BaseResourceResponse
{
    public string? AssignedUserName { get; set; }
    public int DaysSinceStatusChange => HistoryCreated is null ? 0 : (DateTime.UtcNow - HistoryCreated.Value).Days;
}

public record ResourceListRequest(int Skip, int Take, int LanguageId, int ParentResourceId, string? Query)
    : ResourceListCountRequest(LanguageId, ParentResourceId, Query);

public record ResourceListCountRequest(int LanguageId, int ParentResourceId, string? Query);