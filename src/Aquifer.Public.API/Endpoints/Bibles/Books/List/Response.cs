namespace Aquifer.Public.API.Endpoints.Bibles.Books.List;

public record Response
{
    public required string Name { get; init; }
    public required string Code { get; init; }
}