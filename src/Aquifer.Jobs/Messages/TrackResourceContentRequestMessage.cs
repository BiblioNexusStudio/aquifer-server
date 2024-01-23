namespace Aquifer.Jobs.Messages;

public record TrackResourceContentRequestMessage
{
    public required string IpAddress { get; set; }
    public required IEnumerable<int> ResourceContentIds { get; set; }
}
