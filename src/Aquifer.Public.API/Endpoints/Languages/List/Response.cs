using Aquifer.Data.Entities;

namespace Aquifer.Public.API.Endpoints.Languages.List;

public record Response
{
    public int Id { get; init; }
    public string Code { get; init; } = null!;
    public string EnglishDisplay { get; init; } = null!;
    public string LocalizedDisplay { get; init; } = null!;
    public ScriptDirection ScriptDirection { get; init; }
}