using Aquifer.Data.Enums;

namespace Aquifer.API.Endpoints.Resources.ParentResources.Statuses.Get;

public class Response
{
    public required string Book { get; set; }
    public required ParentResourceStatus Status { get; set; }
}