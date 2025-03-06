namespace Aquifer.API.Endpoints.Resources.Content.Uploads.Get;

public sealed class Response
{
    public required int ResourceContentId { get; init; }
    public required int UploadId { get; init; }
    public required UploadStatus Status { get; init; }

    // TODO This should probably move to the DB
    public enum UploadStatus
    {
        None = 0,
        Pending = 1,
        Completed = 2,
    }
}