using Aquifer.Data.Enums;
using Aquifer.Data.EventHandlers;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[Index(nameof(ResourceContentVersionId))]
public class ResourceContentVersionMachineTranslationEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }
    public int ResourceContentVersionId { get; set; }
    public MachineTranslationSourceId SourceId { get; set; }
    public string? DisplayName { get; set; }
    public string Content { get; set; } = null!; // JSON
    public int UserId { get; set; }
    public byte UserRating { get; set; }
    public bool ImproveClarity { get; set; }
    public bool ImproveTone { get; set; }
    public bool ImproveConsistency { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public ResourceContentVersionEntity ResourceContentVersion { get; set; } = null!;
    public UserEntity User { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}