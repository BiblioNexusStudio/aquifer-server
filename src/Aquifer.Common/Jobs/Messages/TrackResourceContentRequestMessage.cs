namespace Aquifer.Common.Jobs.Messages;

public record TrackResourceContentRequestMessage
{
    public required string IpAddress { get; set; }
    public required IEnumerable<int> ResourceContentIds { get; set; }
    public string? Source { get; set; }
    public string? SubscriptionName { get; set; }
    public string? EndpointId { get; set; }
    public string? UserId { get; set; }
}