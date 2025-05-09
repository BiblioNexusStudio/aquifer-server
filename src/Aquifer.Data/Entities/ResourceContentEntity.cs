using System.ComponentModel.DataAnnotations;
using Aquifer.Data.EventHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.Data.Entities;

[EntityTypeConfiguration(typeof(ResourceContentEntityConfiguration))]
[Index(nameof(ResourceId), nameof(LanguageId), nameof(MediaType), IsUnique = true)]
public class ResourceContentEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }

    public int ResourceId { get; set; }
    public ResourceEntity Resource { get; set; } = null!;

    public int SourceLanguageId { get; set; }
    public LanguageEntity SourceLanguage { get; set; } = null!;

    /// <summary>
    /// AKA TargetLanguageId
    /// </summary>
    public int LanguageId { get; set; }

    /// <summary>
    /// AKA TargetLanguage
    /// </summary>
    public LanguageEntity Language { get; set; } = null!;

    public bool Trusted { get; set; }
    public ResourceContentMediaType MediaType { get; set; }
    public ResourceContentStatus Status { get; set; }
    public string? ExternalVersion { get; set; }
    public DateTime? ContentUpdated { get; set; }

    [MaxLength(256)]
    public string? NotApplicableReason { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public ICollection<ResourceContentVersionEntity> Versions { get; set; } = [];
    public ICollection<ProjectResourceContentEntity> ProjectResourceContents { get; set; } = [];

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}

public class ResourceContentEntityConfiguration : IEntityTypeConfiguration<ResourceContentEntity>
{
    public void Configure(EntityTypeBuilder<ResourceContentEntity> builder)
    {
        builder.HasIndex(e => new
            {
                e.LanguageId,
                e.MediaType,
                e.Status,
            })
            .IncludeProperties(e => new
            {
                e.SourceLanguageId,
                e.Created,
                e.ResourceId,
                e.Updated,
                e.ContentUpdated,
            });

        builder.HasIndex(e => new { e.Status })
            .IncludeProperties(e => new
            {
                e.ContentUpdated,
                e.SourceLanguageId,
                e.LanguageId,
                e.ResourceId,
            });

        // This necessary in order to prevent cascading delete cycles.
        builder
            .HasOne(rc => rc.SourceLanguage)
            .WithMany()
            .HasForeignKey(rc => rc.SourceLanguageId)
            .OnDelete(DeleteBehavior.NoAction);
        builder
            .HasOne(rc => rc.Language)
            .WithMany()
            .HasForeignKey(rc => rc.LanguageId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

public enum ResourceContentMediaType
{
    None = 0,
    Text = 1,
    Audio = 2,
    Video = 3,
    Image = 4,
}

public enum ResourceContentStatus
{
    None = 0,

    [Display(Name = "New")]
    New = 1,

    [Display(Name = "Aquiferize - Editor Review")]
    AquiferizeEditorReview = 2,

    [Display(Name = "Complete")]
    Complete = 3,

    [Display(Name = "Aquiferize - Review Pending")]
    AquiferizeReviewPending = 4,

    [Display(Name = "Aquiferize - Publisher Review")]
    AquiferizePublisherReview = 5,

    [Display(Name = "Translation - Awaiting AI Draft")]
    TranslationAwaitingAiDraft = 6,

    [Display(Name = "Translation - Editor Review")]
    TranslationEditorReview = 7,

    [Display(Name = "Translation - Review Pending")]
    TranslationReviewPending = 8,

    [Display(Name = "Translation - Publisher Review")]
    TranslationPublisherReview = 9,

    [Display(Name = "On Hold")]
    OnHold = 10,

    [Display(Name = "Aquiferize - Company Review")]
    AquiferizeCompanyReview = 11,

    [Display(Name = "Translation - Company Review")]
    TranslationCompanyReview = 12,

    [Display(Name = "Translation - Not Applicable Review")]
    TranslationNotApplicable = 13,

    [Display(Name = "Complete - Not Applicable")]
    CompleteNotApplicable = 14,

    [Display(Name = "Translation - AI Draft Complete")]
    TranslationAiDraftComplete = 15,

    [Display(Name = "Aquiferize - Awaiting AI Draft")]
    AquiferizeAwaitingAiDraft = 16,

    [Display(Name = "Aquiferize - AI Draft Complete")]
    AquiferizeAiDraftComplete = 17,
}