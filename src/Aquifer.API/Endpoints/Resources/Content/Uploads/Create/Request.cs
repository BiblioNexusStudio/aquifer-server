namespace Aquifer.API.Endpoints.Resources.Content.Uploads.Create;

public sealed class Request
{
    public int ResourceContentId { get; init; }
    public IFormFile File { get; init; } = null!;
}