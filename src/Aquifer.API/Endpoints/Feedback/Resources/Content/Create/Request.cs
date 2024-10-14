using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Feedback.Resources.Content.Create;

public record Request
{
    public int ContentId { get; set; }
    public int? Version { get; set; }
    public string? ContactValue { get; set; }
    public FeedbackContactType? ContactType { get; set; }
    public string? Feedback { get; set; }
    public FeedbackType FeedbackType { get; set; }
    public byte UserRating { get; set; }
}