namespace Aquifer.API.Endpoints.Feedback.Resources.Content.Create;

public record Request
{
    public int ContentId { get; set; }
    public int? Version { get; set; }
    public string? ContactValue { get; set; }
    public string? ContactType { get; set; }
    public string? Feedback { get; set; }
    public byte UserRating { get; set; }
}