namespace Aquifer.API.Endpoints.Bibles.Language.Book.Text.Get;

public record Request
{
    public int LanguageId { get; set; }
    public string BookCode { get; set; } = null!;
}