namespace Aquifer.API.Modules.Languages;

public class LanguageResponse
{
    public int Id { get; set; }
    public required string Iso6393Code { get; set; }
    public required string EnglishDisplay { get; set; }
}