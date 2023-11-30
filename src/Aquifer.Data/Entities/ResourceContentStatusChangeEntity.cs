namespace Aquifer.Data.Entities;

public class ResourceContentStatusChangeEntity
{
    public int Id { get; set; }

    public int ResourceContentId { get; set; }
    public ResourceContentEntity ResourceContent { get; set; } = null!;

    public ResourceContentStatus FromStatus { get; set; }
    public ResourceContentStatus ToStatus { get; set; }

    public int UserId { get; set; }
    public UserEntity User { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;
}
