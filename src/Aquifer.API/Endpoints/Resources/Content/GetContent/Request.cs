namespace Aquifer.API.Endpoints.Resources.Content.GetContent;

public class Request
{
    public int ResourceContentId { get; set; }
    public string AudioType { get; set; } = "webm";
}