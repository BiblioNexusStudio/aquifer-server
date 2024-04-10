using Aquifer.Data.EventHandlers;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[PrimaryKey(nameof(ParentResourceId), nameof(LanguageId))]
public class ParentResourceLocalizationEntity : IHasUpdatedTimestamp
{
    public int ParentResourceId { get; set; }
    public int LanguageId { get; set; }
    public string DisplayName { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public ParentResourceEntity ParentResource { get; set; } = null!;
    public LanguageEntity Language { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}