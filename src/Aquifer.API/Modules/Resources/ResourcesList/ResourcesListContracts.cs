using Aquifer.Data.Entities;

namespace Aquifer.API.Modules.Resources.ResourcesList;

public record ResourceListItemDto
{
    public string EnglishLabel { get; set; } = null!;
    public string ParentResourceName { get; set; } = null!;
    public ResourceContentStatus Status { get; set; }
    public IEnumerable<ResourceListItemForLanguageDto> ContentIdsWithLanguageIds { get; set; } = null!;
}

public record ResourceListItemForLanguageDto
{
    public int ContentId { get; set; }
    public int LanguageId { get; set; }
}

public record ResourceListRequest(int Skip, int Take, int LanguageId, int ParentResourceId, string? Query)
    : ResourceListCountRequest(LanguageId, ParentResourceId, Query);

public record ResourceListCountRequest(int LanguageId, int ParentResourceId, string? Query);
