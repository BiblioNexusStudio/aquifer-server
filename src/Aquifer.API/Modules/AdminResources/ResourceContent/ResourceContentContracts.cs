using Aquifer.Data.Entities;

namespace Aquifer.API.Modules.AdminResources.ResourceContent;

public class ResourceContentStatusResponse
{
    public ResourceContentStatus Status { get; init; }
    public string DisplayName { get; init; } = null!;
}