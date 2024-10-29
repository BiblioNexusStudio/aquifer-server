using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Resources.Content.Status.Get;

public class Response
{
    public required ResourceContentStatus Status { get; set; }
}