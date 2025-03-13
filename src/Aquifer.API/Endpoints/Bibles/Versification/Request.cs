namespace Aquifer.API.Endpoints.Bibles.Versification;

public class Request
{
    public int TargetBibleId { get; set; }
    public int BookId { get; set; }
    public int? StartChapter { get; set; }
    public int? EndChapter { get; set; }
    public int? StartVerse { get; set; }
    public int? EndVerse { get; set; }
}