namespace Aquifer.API.Endpoints.Bibles.Book.Get;

public record Request
{
    public int BibleId { get; set; }
    public string BookCode { get; set; } = null!;
}