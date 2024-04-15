namespace Aquifer.API.Endpoints.BibleBooks.List;

public record Response
{
    public required int Id { get; set; }
    public required string Name { get; init; }
    public required string Code { get; init; }
}