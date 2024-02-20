using Aquifer.Data.EventHandlers;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[Index(nameof(BibleId),
    nameof(WordIdentifier),
    IsUnique = true)]
public class BibleVersionWordEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }
    public int BibleId { get; set; }
    public int WordIdentifier { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;

    public BibleEntity Bible { get; set; } = null!;

    public ICollection<NewTestamentAlignmentEntity> NewTestamentAlignments { get; set; } = new List<NewTestamentAlignmentEntity>();
}
