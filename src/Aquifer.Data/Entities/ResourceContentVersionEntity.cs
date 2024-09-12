using Aquifer.Data.EventHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.Data.Entities;

[EntityTypeConfiguration(typeof(ResourceContentVersionEntityConfiguration))]
public class ResourceContentVersionEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }

    public int ResourceContentId { get; set; }
    public ResourceContentEntity ResourceContent { get; set; } = null!;

    public int Version { get; set; }
    public bool IsDraft { get; set; }
    public bool IsPublished { get; set; }
    public string DisplayName { get; set; } = null!;
    public string Content { get; set; } = null!; // JSON
    public int ContentSize { get; set; }
    public int? WordCount { get; set; }
    public int? SourceWordCount { get; set; }
    public int? InlineMediaSize { get; set; }
    public int? AssignedUserId { get; set; }
    public ResourceContentVersionReviewLevel ReviewLevel { get; set; }
    public UserEntity? AssignedUser { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public ICollection<ResourceContentVersionMachineTranslationEntity> MachineTranslations { get; set; } = [];

    public IEnumerable<ResourceContentVersionStatusHistoryEntity> ResourceContentVersionStatusHistories { get; set; } =
        new List<ResourceContentVersionStatusHistoryEntity>();

    public IEnumerable<ResourceContentVersionAssignedUserHistoryEntity> ResourceContentVersionAssignedUserHistories { get; set; } =
        new List<ResourceContentVersionAssignedUserHistoryEntity>();

    public ICollection<ResourceContentVersionSnapshotEntity> ResourceContentVersionSnapshots { get; set; } =
        new List<ResourceContentVersionSnapshotEntity>();

    public ICollection<ResourceContentVersionCommentThreadEntity> CommentThreads { get; set; } = [];

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}

public class ResourceContentVersionEntityConfiguration : IEntityTypeConfiguration<ResourceContentVersionEntity>
{
    public void Configure(EntityTypeBuilder<ResourceContentVersionEntity> builder)
    {
        builder.ToTable(b =>
            b.HasCheckConstraint("CK_ResourceContentVersions_IsPublishedOrIsDraftNotBoth", "IsPublished = 0 OR IsDraft = 0"));

        builder.HasIndex(x => new { x.ResourceContentId, x.Version })
            .IsUnique();

        builder.HasIndex(x => x.AssignedUserId)
            .IncludeProperties(x => new { x.ResourceContentId, x.SourceWordCount });

        builder.HasIndex(x => x.ResourceContentId)
            .HasFilter($"{nameof(ResourceContentVersionEntity.IsDraft)} = 1")
            .HasDatabaseName("IX_ResourceContentVersions_ResourceContentId_IsDraft")
            .IsUnique();

        // This is applied by manually adding a CreateIndex to the generated migration file.
        // Unfortunately EF has a limitation around having two filter indexes on the same column.
        // Note: this index is not in the .Designer or snapshot files because it would get removed
        //       every time a new migration is generated.
        //
        // builder
        //     .HasIndex(x => x.ResourceContentId)
        //     .HasFilter($"{nameof(ResourceContentVersionEntity.IsPublished)} = 1")
        //     .HasDatabaseName("IX_ResourceContentVersions_ResourceContentId_IsPublished")
        //     .IsUnique();
    }
}

public enum ResourceContentVersionReviewLevel
{
    None = 0,
    Community = 1,
    Professional = 2
}