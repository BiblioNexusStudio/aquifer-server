namespace Aquifer.Common.Jobs.Messages;

public record TrackResourceContentRequestMessage
{
    public required string IpAddress { get; set; }
    public required IEnumerable<int> ResourceContentIds { get; set; }
    // Don't put required on these because they can get serialized and stripped off,
    // Then when something else goes to serialize it again it will blow up.
    public string? Source { get; set; }
    public string? SubscriptionName { get; set; }
    public string? EndpointId { get; set; }
    public string? UserId { get; set; }
}