namespace Aquifer.API.Endpoints.Resources.Content.NotApplicable.Update;

public class Request
{
    public int ContentId { get; set; }
    public string NotApplicableReason { get; set; } = null!;
}