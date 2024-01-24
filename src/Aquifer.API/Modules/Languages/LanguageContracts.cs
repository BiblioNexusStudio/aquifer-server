using Aquifer.Data.Entities;

namespace Aquifer.API.Modules.Languages;

public record LanguageResponse(int Id, string Iso6393Code, string EnglishDisplay, ScriptDirection ScriptDirection);