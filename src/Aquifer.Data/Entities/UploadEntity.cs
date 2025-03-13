using System.ComponentModel.DataAnnotations;
using Aquifer.Data.EventHandlers;

namespace Aquifer.Data.Entities;

public class UploadEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }

    public string BlobName { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public long FileSize { get; set; }
    public ResourceContentEntity? ResourceContent { get; set; }
    public int? ResourceContentId { get; set; }
    public int? StepNumber { get; set; }
    public UserEntity StartedByUser { get; set; } = null!;
    public int StartedByUserId { get; set; }
    public UploadStatus Status { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}

public enum UploadStatus
{
    None = 0,

    [Display(Name = "Pending")]
    Pending = 1,

    [Display(Name = "Processing")]
    Processing = 2,

    [Display(Name = "Completed")]
    Completed = 3,

    [Display(Name = "Failed")]
    Failed = 4,
}