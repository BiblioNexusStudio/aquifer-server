using System.ComponentModel.DataAnnotations;
using Aquifer.Data.EventHandlers;

namespace Aquifer.Data.Entities;

public class ContentSubscriberEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }

    [MaxLength(256)]
    public string Name { get; set; } = null!;

    [MaxLength(256)]
    public string Email { get; set; } = null!;

    [MaxLength(256)]
    public string? Organization { get; set; }

    public bool GetNewsletter { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public ICollection<ContentSubscriberLanguageEntity> ContentSubscriberLanguages { get; set; } = [];
    public ICollection<ContentSubscriberParentResourceEntity> ContentSubscriberParentResources { get; set; } = [];

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}