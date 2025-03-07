namespace Aquifer.Common.Messages.Models;

public sealed record UploadResourceContentAudioMessage(
    int UploadId,
    int ResourceContentId,
    string TempContainerSourceBlobName,
    string CdnContainerTargetBlobName);