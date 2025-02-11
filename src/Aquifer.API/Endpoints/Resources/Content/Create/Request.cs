namespace Aquifer.API.Endpoints.Resources.Content.Create;

public class Request
{
    public int LanguageId { get; set; }
    public int ParentResourceId { get; set; }
    public string EnglishLabel { get; set; } = null!;
    public string LanguageTitle { get; set; } = null!;
}