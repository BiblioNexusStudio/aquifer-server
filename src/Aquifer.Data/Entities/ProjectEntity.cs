using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.Data.Entities;

[EntityTypeConfiguration(typeof(ProjectEntityConfiguration))]
public class ProjectEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int LanguageId { get; set; }
    public LanguageEntity Language { get; set; } = null!;
    public int ProjectManagerUserId { get; set; }
    public UserEntity ProjectManagerUser { get; set; } = null!;
    public int CompanyId { get; set; }
    public CompanyEntity Company { get; set; } = null!;
    public int PlatformId { get; set; }
    public ProjectPlatformEntity Platform { get; set; } = null!;
    public int? CompanyLeadUserId { get; set; }
    public UserEntity? CompanyLeadUser { get; set; }
    public int SourceWordCount { get; set; }
    public int? EffectiveWordCount { get; set; }
    public int? QuotedCostCents { get; set; }
    public DateTime? Started { get; set; }
    public DateOnly? ProjectedDeliveryDate { get; set; }
    public DateOnly? ActualDeliveryDate { get; set; }
    public DateOnly? ProjectedPublishDate { get; set; }
    public DateOnly? ActualPublishDate { get; set; }

    public ICollection<ResourceContentEntity> ResourceContents { get; set; } = new List<ResourceContentEntity>();

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}

public class ProjectEntityConfiguration : IEntityTypeConfiguration<ProjectEntity>
{
    public void Configure(EntityTypeBuilder<ProjectEntity> builder)
    {
        builder
            .HasMany(x => x.ResourceContents)
            .WithMany(y => y.Projects)
            .UsingEntity(
                "ProjectResourceContents",
                l => l.HasOne(typeof(ResourceContentEntity)).WithMany().HasForeignKey("ResourceContentId")
                    .HasPrincipalKey(nameof(ResourceContentEntity.Id)).OnDelete(DeleteBehavior.Restrict),
                r => r.HasOne(typeof(ProjectEntity)).WithMany().HasForeignKey("ProjectId").HasPrincipalKey(nameof(ProjectEntity.Id))
                    .OnDelete(DeleteBehavior.Restrict),
                j => j.HasKey("ProjectId", "ResourceContentId"));
    }
}