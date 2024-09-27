using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Languages.List;

public record Response
{
    public required int Id { get; set; }
    public required string Iso6393Code { get; set; }
    public required string EnglishDisplay { get; set; }
    public required string DisplayName { get; set; }
    public required bool Enabled { get; set; }
    public required ScriptDirection ScriptDirection { get; set; }
}