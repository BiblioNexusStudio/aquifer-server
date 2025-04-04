using Aquifer.Data.EventHandlers;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[Index(nameof(Name), IsUnique = true)]
public class ProjectEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public int? SourceLanguageId { get; set; }
    public LanguageEntity? SourceLanguage { get; set; }

    /// <summary>
    /// AKA TargetLanguageId
    /// </summary>
    public int LanguageId { get; set; }

    /// <summary>
    /// AKA TargetLanguage
    /// </summary>
    public LanguageEntity Language { get; set; } = null!;

    public int ProjectManagerUserId { get; set; }
    public UserEntity ProjectManagerUser { get; set; } = null!;

    public int CompanyId { get; set; }
    public CompanyEntity Company { get; set; } = null!;

    public int ProjectPlatformId { get; set; } = 1; // Should always be Aquifer now
    public ProjectPlatformEntity ProjectPlatform { get; set; } = null!;

    public int? CompanyLeadUserId { get; set; }
    public UserEntity? CompanyLeadUser { get; set; }

    public int SourceWordCount { get; set; }
    public int? EffectiveWordCount { get; set; }

    [Precision(18, 2)]
    public decimal? QuotedCost { get; set; }

    public DateTime? Started { get; set; }
    public DateOnly? ProjectedDeliveryDate { get; set; }
    public DateOnly? ActualDeliveryDate { get; set; }
    public DateOnly? ProjectedPublishDate { get; set; }
    public DateOnly? ActualPublishDate { get; set; }

    public ICollection<ProjectResourceContentEntity> ProjectResourceContents { get; set; } = [];

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}