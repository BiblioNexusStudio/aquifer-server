using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Data.Entities;

[PrimaryKey(nameof(VerseId), nameof(ResourceId))]
public class VerseResourceEntity
{
    public int VerseId { get; set; }
    public int ResourceId { get; set; }
    
    public ICollection<VerseEntity> Verses { get; set; } = null!;
    public ResourceEntity Resource { get; set; } = null!;
}
