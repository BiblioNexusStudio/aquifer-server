namespace Aquifer.Public.API.Endpoints.Resources.Get.AvailableLanguages;

public class Response
{
    public int ContentId { get; set; }
    public string ContentDisplayName { get; set; } = null!;
    public int LanguageId { get; set; }
    public string LanguageDisplayName { get; set; } = null!;
    public string LanguageEnglishDisplayName { get; set; } = null!;
    public string LanguageCode { get; set; } = null!;
}