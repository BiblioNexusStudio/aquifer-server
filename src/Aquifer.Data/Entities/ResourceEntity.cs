using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.Data.Entities;

[Index(nameof(Type),
    nameof(MediaType),
    nameof(EnglishLabel),
    IsUnique = true)]
[EntityTypeConfiguration(typeof(ResourceEntityConfiguration))]
public class ResourceEntity
{
    public int Id { get; set; }
    public ResourceEntityType Type { get; set; }
    public ResourceEntityMediaType MediaType { get; set; }
    public string EnglishLabel { get; set; } = null!;
    public string? Tag { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;

    public ICollection<VerseResourceEntity> VerseResources { get; set; } =
        new List<VerseResourceEntity>();

    public ICollection<PassageResourceEntity> PassageResources { get; set; } =
        new List<PassageResourceEntity>();

    public ICollection<ResourceContentEntity> ResourceContents { get; set; } =
        new List<ResourceContentEntity>();

    public ICollection<ResourceEntity> SupportingResources { get; set; } =
        new List<ResourceEntity>();

    public ICollection<ResourceEntity> ResourcesSupported { get; set; } =
        new List<ResourceEntity>();
}

public class ResourceEntityConfiguration : IEntityTypeConfiguration<ResourceEntity>
{
    public void Configure(EntityTypeBuilder<ResourceEntity> builder)
    {
        builder.HasMany(e => e.SupportingResources)
            .WithMany(j => j.ResourcesSupported)
            .UsingEntity("SupportingResources",
                j => j
                    .HasOne(typeof(ResourceEntity))
                    .WithMany()
                    .HasForeignKey("SupportingResourceId"),
                j => j
                    .HasOne(typeof(ResourceEntity))
                    .WithMany()
                    .HasForeignKey("ParentResourceId"));
    }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ResourceEntityType
{
    None = 0,
    CBBTER = 1,
    TyndaleBibleDictionary = 2,
    GenericImage = 3,
}

public enum ResourceEntityMediaType
{
    None = 0,
    Text = 1,
    Audio = 2,
    Video = 3,
    Image = 4
}