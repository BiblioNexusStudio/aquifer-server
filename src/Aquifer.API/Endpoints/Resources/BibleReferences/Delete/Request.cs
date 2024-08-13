namespace Aquifer.API.Endpoints.Resources.BibleReferences.Delete;

public record Request
{
    public int ResourceContentId { get; set; }
    public int StartVerseId { get; set; }
    public int EndVerseId { get; set; }
}