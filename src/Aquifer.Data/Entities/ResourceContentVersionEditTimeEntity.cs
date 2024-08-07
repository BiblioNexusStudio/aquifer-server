namespace Aquifer.Data.Entities;

public class ResourceContentVersionEditTimeEntity
{
    public int Id { get; set; }
    public int ResourceContentVersionId { get; set; }
    public int UserId { get; set; }
    public DateTime Created { get; set; }

    public UserEntity User { get; set; } = null!;
    public ResourceContentVersionEntity ResourceContentVersion { get; set; } = null!;
}