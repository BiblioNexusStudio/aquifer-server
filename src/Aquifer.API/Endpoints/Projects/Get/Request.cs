namespace Aquifer.API.Endpoints.Projects.Get;

public record Request
{
    public int ProjectId { get; init; }
}