namespace Aquifer.API.Endpoints.Resources.Content.Uploads.Get;

public sealed class Request
{
    public int ResourceContentId { get; init; }
    public int UploadId { get; init; }
}