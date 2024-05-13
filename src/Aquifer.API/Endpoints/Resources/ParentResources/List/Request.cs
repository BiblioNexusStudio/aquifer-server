using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Resources.ParentResources.List;

public class Request
{
    public ResourceType ResourceType { get; set; } = ResourceType.None;
    public int LanguageId { get; set; } = 1;
}