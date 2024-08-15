using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[PrimaryKey(nameof(ContentSubscriberId), nameof(LanguageId))]
public class ContentSubscriberLanguageEntity
{
    public int ContentSubscriberId { get; set; }
    public int LanguageId { get; set; }

    public ContentSubscriberEntity ContentSubscriber { get; set; } = null!;
    public LanguageEntity Language { get; set; } = null!;
}