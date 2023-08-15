namespace Aquifer.API.Modules.Languages;

public class LanguageResponse
{
    public int Id { get; set; }
    public string Iso6393Code { get; set; } = null!;
    public string EnglishDisplay { get; set; } = null!;
}