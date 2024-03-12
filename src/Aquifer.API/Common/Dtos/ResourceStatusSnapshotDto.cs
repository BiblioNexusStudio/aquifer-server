using Aquifer.Data.Entities;

namespace Aquifer.API.Common.Dtos;

public class ResourceStatusSnapshotDto
{
    public required ResourceContentStatus Status { get; set; }
    public required ResourceContentVersionSnapshotEntity? Snapshot { get; set; }

    public required ResourceContentVersionEntity ResourceContentVersion { get; set; }
}