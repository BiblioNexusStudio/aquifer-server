namespace Aquifer.Common.Jobs.Messages;

public record TrackResourceContentRequestMessage
{
    public required string IpAddress { get; set; }
    public required IEnumerable<int> ResourceContentIds { get; set; }
    public required string? Source { get; set; }
    public required string? SubscriptionName { get; set; }
    public required string? EndpointId { get; set; }
    public required string? UserId { get; set; }
}