namespace Aquifer.API.Endpoints.Resources.Content.CreateTranslation;

public record Request
{
    public int LanguageId { get; set; }
    public int BaseContentId { get; set; }
    public bool UseDraft { get; set; }
}