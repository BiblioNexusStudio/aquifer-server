namespace Aquifer.API.Endpoints.Resources.Content.Uploads.Create;

public sealed class Response
{
    public required int ResourceContentId { get; init; }
    public required int UploadId { get; init; }
}