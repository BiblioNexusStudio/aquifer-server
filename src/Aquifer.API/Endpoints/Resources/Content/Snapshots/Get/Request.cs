namespace Aquifer.API.Endpoints.Resources.Content.Snapshots.Get;

public record Request
{
    public int SnapshotId { get; set; }
}