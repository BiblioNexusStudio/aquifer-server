namespace Aquifer.API.Endpoints.Resources.Content.SendForReview;

public record Request
{
    public int? ContentId { get; set; }
    public List<int>? ContentIds { get; set; }
}