using System.ComponentModel.DataAnnotations;
using Aquifer.Data.EventHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.Data.Entities;

[EntityTypeConfiguration(typeof(ResourceContentEntityConfiguration))]
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

    public ICollection<ResourceContentVersionEntity> Versions { get; set; } = [];

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public LanguageEntity Language { get; set; } = null!;
    public ResourceEntity Resource { get; set; } = null!;

    public ICollection<ProjectResourceContentEntity> ProjectResourceContents { get; set; } = [];
    public int Id { get; set; }
    public ResourceContentStatus Status { get; set; }

    public string? ExternalVersion { get; set; }

    public DateTime? ContentUpdated { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}

public class ResourceContentEntityConfiguration : IEntityTypeConfiguration<ResourceContentEntity>
{
    public void Configure(EntityTypeBuilder<ResourceContentEntity> builder)
    {
        builder.HasIndex(e => new { e.LanguageId, e.MediaType })
            .IncludeProperties(e => new { e.Created, e.ResourceId, e.Status });

        builder.HasIndex(e => new { e.Status })
            .IncludeProperties(e => new { e.ContentUpdated, e.LanguageId, e.ResourceId });
    }
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

    [Display(Name = "Aquiferize - Publisher Review")]
    AquiferizePublisherReview = 5,

    [Display(Name = "Translation - Not Started")]
    TranslationNotStarted = 6,

    [Display(Name = "Translation - In Progress")]
    TranslationInProgress = 7,

    [Display(Name = "Translation - Review Pending")]
    TranslationReviewPending = 8,

    [Display(Name = "Translation - Publisher Review")]
    TranslationPublisherReview = 9,

    [Display(Name = "On Hold")]
    OnHold = 10,

    [Display(Name = "Aquiferize - Manager Review")]
    AquiferizeManagerReview = 11,

    [Display(Name = "Translation - Manager Review")]
    TranslationManagerReview = 12,

    [Display(Name = "Translation - Not Applicable Review")]
    TranslationNotApplicable = 13,

    [Display(Name = "Complete - Not Applicable")]
    CompleteNotApplicable = 14
}