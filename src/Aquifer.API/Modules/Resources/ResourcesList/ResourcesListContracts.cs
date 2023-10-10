using Aquifer.Data.Entities;

namespace Aquifer.API.Modules.Resources.ResourcesList;

public record ResourceListItemResponse(int Id, string Name, string Type, ResourceContentStatus Status);

public record ResourceListRequest(int Skip, int Take, string? Query);