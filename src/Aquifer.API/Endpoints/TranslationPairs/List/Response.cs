namespace Aquifer.API.Endpoints.TranslationPairs.List;

public class Response
{
    public int LanguageId { get; set; }
    public int TranslationPairId { get; set; }
    public string LanguageEnglishDisplay { get; set; } = null!;
    public string TranslationPairKey { get; set; } = null!;
    public string TranslationPairValue { get; set; } = null!;
}