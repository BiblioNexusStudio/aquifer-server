namespace Aquifer.API.Data.Entities;

public class BibleEntity
{
    public int Id { get; set; }
    public int LanguageId { get; set; }
    public string Name { get; set; } = null!;

    [CreatedDate]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [UpdatedDate]
    public DateTime Updated { get; set; } = DateTime.UtcNow;

    public ICollection<VerseResourceEntity> Verses { get; set; } = new List<VerseResourceEntity>();
}
