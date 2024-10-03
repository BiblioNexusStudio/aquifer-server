using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.Data.Entities;

[PrimaryKey(nameof(ProjectId), nameof(ResourceContentId))]
[Index(nameof(ResourceContentId), IsUnique = true)]
[EntityTypeConfiguration(typeof(ProjectResourceContentEntityConfiguration))]
public class ProjectResourceContentEntity
{
    public int ProjectId { get; set; }
    public ProjectEntity Project { get; set; } = null!;
    public int ResourceContentId { get; set; }
    public ResourceContentEntity ResourceContent { get; set; } = null!;
}

public class ProjectResourceContentEntityConfiguration
    : IEntityTypeConfiguration<ProjectResourceContentEntity>
{
    public void Configure(EntityTypeBuilder<ProjectResourceContentEntity> builder)
    {
        builder
            .HasOne(x => x.Project)
            .WithMany(x => x.ProjectResourceContents)
            .HasForeignKey(nameof(ProjectResourceContentEntity.ProjectId))
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.ResourceContent)
            .WithMany(x => x.ProjectResourceContents)
            .HasForeignKey(nameof(ProjectResourceContentEntity.ResourceContentId))
            .OnDelete(DeleteBehavior.Restrict);
    }
}