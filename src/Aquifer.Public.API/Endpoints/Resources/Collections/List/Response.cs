using Aquifer.Data.Entities;

namespace Aquifer.Public.API.Endpoints.Resources.Collections.List;

public sealed class Response
{
    public required string Code { get; init; }
    public required string DisplayName { get; init; }
    public required string ShortName { get; init; }
    public required ResourceType ResourceType { get; init; }
}