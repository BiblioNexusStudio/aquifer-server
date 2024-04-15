using Aquifer.Data.Enums;

namespace Aquifer.API.Endpoints.Bibles.Language.Book.Text.Get;

public record Request
{
    public int LanguageId { get; set; }
    public BookId BookId { get; set; }
}