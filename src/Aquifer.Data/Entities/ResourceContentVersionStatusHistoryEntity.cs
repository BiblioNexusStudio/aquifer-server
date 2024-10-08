using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[Index(nameof(ChangedByUserId), nameof(Created))]
public class ResourceContentVersionStatusHistoryEntity
{
    public int Id { get; set; }

    public int ResourceContentVersionId { get; set; }
    public ResourceContentVersionEntity ResourceContentVersion { get; set; } = null!;

    public ResourceContentStatus Status { get; set; }

    public int ChangedByUserId { get; set; }
    public UserEntity ChangedByUser { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;
}