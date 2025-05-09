using System.ComponentModel.DataAnnotations;
using Aquifer.Data.EventHandlers;

namespace Aquifer.Data.Entities;

public class ParentResourceEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }
    public string ShortName { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public string LicenseInfo { get; set; } = null!;
    public ResourceTypeComplexityLevel ComplexityLevel { get; set; }
    public ResourceType ResourceType { get; set; }
    public bool Enabled { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public ICollection<ParentResourceLocalizationEntity> Localizations { get; set; } = [];

    public bool? ForMarketing { get; set; }
    public bool AllowCommunityReview { get; set; }

    [MaxLength(128)]
    public string? SliCategory { get; set; }

    public int? SliLevel { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}

public enum ResourceTypeComplexityLevel
{
    None = 0,
    Basic = 1,
    Advanced = 2,
}

public enum ResourceType
{
    None = 0,

    [Display(Name = "Translation Guides")]
    Guide = 1,

    [Display(Name = "Bible Dictionaries")]
    Dictionary = 2,

    [Display(Name = "Study Notes")]
    StudyNotes = 3,

    [Display(Name = "Images")]
    Images = 4,

    [Display(Name = "Videos")]
    Videos = 5,
}