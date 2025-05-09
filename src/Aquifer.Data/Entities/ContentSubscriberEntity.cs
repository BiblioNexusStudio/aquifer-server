using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using Aquifer.Data.EventHandlers;

namespace Aquifer.Data.Entities;

public class ContentSubscriberEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }

    [MaxLength(64)]
    public string UnsubscribeId { get; set; } = RandomNumberGenerator.GetHexString(64);

    [MaxLength(256)]
    public string Name { get; set; } = null!;

    [MaxLength(256)]
    public string Email { get; set; } = null!;

    [MaxLength(256)]
    public string? Organization { get; set; }

    public bool GetNewsletter { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public bool Enabled { get; set; }

    public ICollection<ContentSubscriberLanguageEntity> ContentSubscriberLanguages { get; set; } = [];
    public ICollection<ContentSubscriberParentResourceEntity> ContentSubscriberParentResources { get; set; } = [];

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}