namespace Aquifer.API.Endpoints.TranslationPairs.List;

public class Response
{
    public string LanguageId { get; set; } = null!;
    public int TranslationPairId { get; set; }
    public string LanguageEnglishDisplay { get; set; } = null!;
    public string TranslationPairKey { get; set; } = null!;
    public string TranslationPairValue { get; set; } = null!;
}