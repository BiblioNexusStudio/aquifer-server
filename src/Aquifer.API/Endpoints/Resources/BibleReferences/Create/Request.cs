namespace Aquifer.API.Endpoints.Resources.BibleReferences.Create;

public record Request
{
    public int ResourceContentId { get; set; }
    public int StartVerseId { get; set; }
    public int EndVerseId { get; set; }
}