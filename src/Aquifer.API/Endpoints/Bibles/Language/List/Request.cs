namespace Aquifer.API.Endpoints.Bibles.Language.List;

public record Request
{
    public int LanguageId { get; set; }
    public bool RestrictedLicense { get; set; }
}