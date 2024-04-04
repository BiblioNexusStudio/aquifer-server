using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.ParentResources.List;

public class Response
{
    public List<ParentResourceEntity> ParentResources { get; set; } = [];
}