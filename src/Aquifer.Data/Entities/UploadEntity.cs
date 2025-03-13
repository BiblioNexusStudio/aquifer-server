using System.ComponentModel.DataAnnotations;
using Aquifer.Data.EventHandlers;

namespace Aquifer.Data.Entities;

public class UploadEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }

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