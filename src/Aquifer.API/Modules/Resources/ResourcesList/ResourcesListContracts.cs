using Aquifer.Data.Entities;

namespace Aquifer.API.Modules.Resources.ResourcesList;

public record ResourceListItemResponse(int Id, string Name, string ParentResourceName, ResourceContentStatus Status);

public record ResourceListRequest(int Skip, int Take, int LanguageId, int ParentResourceId, string? Query)
    : ResourceListCountRequest(LanguageId, ParentResourceId, Query);

public record ResourceListCountRequest(int LanguageId, int ParentResourceId, string? Query);