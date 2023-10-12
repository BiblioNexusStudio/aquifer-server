using Aquifer.Data.Entities;

namespace Aquifer.API.Modules.Resources.ResourcesList;

public record ResourceListItemResponse(int Id, string Name, string Type, ResourceContentStatus Status);

public record ResourceListRequest(int Skip, int Take, int LanguageId, int ResourceTypeId, string? Query)
    : ResourceListCountRequest(LanguageId, ResourceTypeId, Query);

public record ResourceListCountRequest(int LanguageId, int ResourceTypeId, string? Query);