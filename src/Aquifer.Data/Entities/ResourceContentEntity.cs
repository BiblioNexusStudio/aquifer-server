using System.ComponentModel.DataAnnotations;
using Aquifer.Data.EventHandlers;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[Index(nameof(ResourceId),
    nameof(LanguageId),
    nameof(MediaType),
    IsUnique = true)]
public class ResourceContentEntity : IHasUpdatedTimestamp
{
    public int ResourceId { get; set; }
    public int LanguageId { get; set; }
    public bool Trusted { get; set; }
    public ResourceContentMediaType MediaType { get; set; }

    public ICollection<ResourceContentVersionEntity> Versions { get; set; } =
        new List<ResourceContentVersionEntity>();

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public LanguageEntity Language { get; set; } = null!;
    public ResourceEntity Resource { get; set; } = null!;

    public ICollection<ProjectEntity> Projects { get; set; } = new List<ProjectEntity>();
    public int Id { get; set; }
    public ResourceContentStatus Status { get; set; }

    public DateTime? ContentUpdated { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}

public enum ResourceContentMediaType
{
    None = 0,
    Text = 1,
    Audio = 2,
    Video = 3,
    Image = 4
}

public enum ResourceContentStatus
{
    None = 0,

    [Display(Name = "New")]
    New = 1,

    [Display(Name = "Aquiferize - In Progress")]
    AquiferizeInProgress = 2,

    [Display(Name = "Complete")]
    Complete = 3,

    [Display(Name = "Aquiferize - Review Pending")]
    AquiferizeReviewPending = 4,

    [Display(Name = "Aquiferize - In Review")]
    AquiferizeInReview = 5,

    [Display(Name = "Translation - Not Started")]
    TranslationNotStarted = 6,

    [Display(Name = "Translation - In Progress")]
    TranslationInProgress = 7,

    [Display(Name = "Translation - Review Pending")]
    TranslationReviewPending = 8,

    [Display(Name = "Translation - In Review")]
    TranslationInReview = 9,

    [Display(Name = "On Hold")]
    OnHold = 10,

    [Display(Name = "Aquiferize - Manager Review")]
    AquiferizeManagerReview = 11,

    [Display(Name = "Translation - Manager Review")]
    TranslationManagerReview = 12
}