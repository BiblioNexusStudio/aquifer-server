namespace Aquifer.API.Data.Entities;

public class VerseEntity
{
    public int Id { get; set; }
    public ICollection<VerseResourceEntity> VerseResources { get; set; } = new List<VerseResourceEntity>();
    public ICollection<VerseContentEntity> VerseContents { get; set; } = new List<VerseContentEntity>();
}