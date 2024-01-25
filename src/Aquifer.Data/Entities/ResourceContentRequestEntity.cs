namespace Aquifer.Data.Entities;

public class ResourceContentRequestEntity
{
    public int Id { get; set; }
    public int ResourceContentId { get; set; }
    public string IpAddress { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;
}