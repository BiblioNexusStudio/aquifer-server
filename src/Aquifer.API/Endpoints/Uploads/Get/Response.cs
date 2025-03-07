using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Uploads.Get;

public sealed class Response
{
    public required int UploadId { get; init; }
    public required UploadStatus Status { get; init; }
    public required DateTime Created { get; init; }
}