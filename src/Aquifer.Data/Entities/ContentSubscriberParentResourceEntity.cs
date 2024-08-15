using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[PrimaryKey(nameof(ContentSubscriberId), nameof(ParentResourceId))]
public class ContentSubscriberParentResourceEntity
{
    public int ContentSubscriberId { get; set; }
    public int ParentResourceId { get; set; }

    public ContentSubscriberEntity ContentSubscriber { get; set; } = null!;
    public ParentResourceEntity ParentResource { get; set; } = null!;
}