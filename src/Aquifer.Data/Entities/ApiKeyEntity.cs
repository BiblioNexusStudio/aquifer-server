using System.ComponentModel.DataAnnotations;
using Aquifer.Data.EventHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aquifer.Data.Entities;

[Index(nameof(ApiKey), IsUnique = true), EntityTypeConfiguration(typeof(ApiKeyEntityConfiguration))]
public class ApiKeyEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }

    [MaxLength(64)]
    public string ApiKey { get; set; } = null!;

    public ApiKeyScope Scope { get; set; }

    [MaxLength(64)]
    public string? Organization { get; set; }

    [MaxLength(64)]
    public string? ContactName { get; set; }

    [MaxLength(64)]
    public string? Email { get; set; }

    [MaxLength(32)]
    public string? Phone { get; set; }

    [MaxLength(256)]
    public string? UseCase { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}

public enum ApiKeyScope
{
    None = 0,
    All = 1,
    InternalApi = 2,
    PublicApi = 3
}

public class ApiKeyEntityConfiguration : IEntityTypeConfiguration<ApiKeyEntity>
{
    public void Configure(EntityTypeBuilder<ApiKeyEntity> builder)
    {
        builder.ToTable(t => t.HasCheckConstraint("CK_Must_Provide_Contact_Info", "ContactName IS NOT NULL OR Organization IS NOT NULL"));
        builder.HasIndex(x => x.ApiKey).IsUnique();
    }
}