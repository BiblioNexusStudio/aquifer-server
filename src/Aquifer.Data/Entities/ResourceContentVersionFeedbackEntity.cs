namespace Aquifer.Data.Entities;

public class ResourceContentVersionFeedbackEntity
{
    public int Id { get; set; }
    public int ResourceContentVersionId { get; set; }
    public string? ContactValue { get; set; }
    public FeedbackContactType? ContactType { get; set; }
    public string? UserId { get; set; }
    public string? Feedback { get; set; }
    public byte UserRating { get; set; }

    public ResourceContentVersionEntity ResourceContentVersion { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;
}

public enum FeedbackContactType
{
    None = 0,
    Email = 1,
    Phone = 2,
    WhatsApp = 3,
    Signal = 4,
    Other = 5,
}