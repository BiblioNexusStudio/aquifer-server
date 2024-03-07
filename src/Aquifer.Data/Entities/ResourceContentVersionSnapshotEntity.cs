namespace Aquifer.Data.Entities;

public class ResourceContentVersionSnapshotEntity
{
    public int Id { get; set; }
    public int ResourceContentVersionId { get; set; }
    public string DisplayName { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int? WordCount { get; set; }
    public int? UserId { get; set; }
    public ResourceContentStatus Status { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public ResourceContentVersionEntity ResourceContentVersion { get; set; } = null!;
    public UserEntity? User { get; set; }
}