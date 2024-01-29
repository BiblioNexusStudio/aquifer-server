using Aquifer.Data.Entities;

namespace Aquifer.API.Modules.Languages;

public record LanguageResponse
{
    public int Id { get; set; }
    public required string Iso6393Code { get; set; }
    public required string EnglishDisplay { get; set; }
    public required string DisplayName { get; set; }
    public bool Enabled { get; set; }
    public ScriptDirection ScriptDirection { get; set; }
}