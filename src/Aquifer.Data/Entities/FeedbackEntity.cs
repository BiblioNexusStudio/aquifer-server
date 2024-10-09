namespace Aquifer.Data.Entities;

public class FeedbackEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string? UserId { get; set; }
    public string? Phone { get; set; }
    public string Feedback { get; set; } = null!;
    public FeedbackType FeedbackType { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;
}

public enum FeedbackType
{
    None = 0,
    WellContactForm = 1,
    WellContactPrompt = 2
}