namespace Aquifer.API.Endpoints.Bibles.Alignments.Greek.Book.Chapter;

public record Request
{
    public int BibleId { get; set; }
    public string BookCode { get; set; } = null!;
    public int Chapter { get; set; }
}