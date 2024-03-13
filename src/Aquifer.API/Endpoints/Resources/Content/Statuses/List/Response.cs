using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Resources.Content.Statuses.List;

public record Response
{
    public required string DisplayName { get; set; }
    public required ResourceContentStatus Status { get; set; }
}