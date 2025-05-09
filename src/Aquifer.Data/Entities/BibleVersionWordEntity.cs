using Aquifer.Data.EventHandlers;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[Index(
    nameof(BibleId),
    nameof(WordIdentifier),
    IsUnique = true)]
public class BibleVersionWordEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }
    public int BibleId { get; set; }
    public long WordIdentifier { get; set; }
    public bool IsPunctuation { get; set; }
    public string Text { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public BibleEntity Bible { get; set; } = null!;

    public ICollection<BibleVersionWordGroupWordEntity> BibleVersionWordGroupWords { get; set; } = [];

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}