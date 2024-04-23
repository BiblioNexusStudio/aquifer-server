namespace Aquifer.API.Endpoints.Bibles.Texts.Get;

public class Request
{
    public int BibleId { get; set; }
    public int? BookNumber { get; set; }
    public string? BookCode { get; set; }
    public int StartChapter { get; set; }
    public int StartVerse { get; set; }
    public int EndChapter { get; set; } = 999;
    public int EndVerse { get; set; } = 999;
}