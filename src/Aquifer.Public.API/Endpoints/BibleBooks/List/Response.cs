using Aquifer.Data.Enums;

namespace Aquifer.Public.API.Endpoints.BibleBooks.List;

public record Response
{
    public BookId Number { get; init; }
    public required string Name { get; init; }
    public required string Identifier { get; init; }
}