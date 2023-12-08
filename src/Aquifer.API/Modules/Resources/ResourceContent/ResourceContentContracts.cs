using Aquifer.Data.Entities;

namespace Aquifer.API.Modules.Resources.ResourceContent;

public class ResourceContentStatusDto
{
    public ResourceContentStatus Status { get; init; }
    public string DisplayName { get; init; } = null!;
}
