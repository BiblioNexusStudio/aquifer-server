using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[PrimaryKey(nameof(PassageId), nameof(ResourceId))]
public class PassageResourceEntity
{
    public int PassageId { get; set; }
    public int ResourceId { get; set; }

    public PassageEntity Passage { get; set; } = null!;

    public ResourceEntity Resource { get; set; } = null!;
}