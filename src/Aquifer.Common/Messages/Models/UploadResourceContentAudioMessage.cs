namespace Aquifer.Common.Messages.Models;

public sealed record UploadResourceContentAudioMessage(
    int UploadId,
    int ResourceContentId,
    int? StepNumber,
    string TempUploadBlobName,
    int StartedByUserId);