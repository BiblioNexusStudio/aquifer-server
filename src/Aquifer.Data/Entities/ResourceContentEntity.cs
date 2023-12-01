using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[Index(nameof(ResourceId), nameof(LanguageId), nameof(MediaType), IsUnique = true)]
public class ResourceContentEntity
{
    public int Id { get; set; }
    public int ResourceId { get; set; }
    public int LanguageId { get; set; }
    public string DisplayName { get; set; } = null!;
    public string? Summary { get; set; }
    public int Version { get; set; }
    public ResourceContentStatus Status { get; set; }
    public bool Enabled { get; set; }
    public bool Trusted { get; set; }
    public bool Published { get; set; }
    public string Content { get; set; } = null!; // JSON
    public int ContentSize { get; set; }
    public ResourceContentMediaType MediaType { get; set; }

    public int? AssignedUserId { get; set; }
    public UserEntity? AssignedUser { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;

    public LanguageEntity Language { get; set; } = null!;
    public ResourceEntity Resource { get; set; } = null!;
}

[JsonConverter(typeof(JsonConverter))]
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
    AquiferizeNotStarted = 1,
    AquiferizeInProgress = 2,
    Complete = 3,
    AquiferizeInReview = 4,
    TranslateNotStarted = 5,
    TranslateDrafting = 6,
    TranslateEditing = 7,
    TranslateReviewing = 8,
    OnHold = 9
}
