using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[Index(nameof(Created))]
public class ResourceContentRequestEntity
{
    public int Id { get; set; }
    public int ResourceContentId { get; set; }

    [MaxLength(64)]
    public string IpAddress { get; set; } = null!;

    public string? SubscriptionName { get; set; }
    public string? EndpointId { get; set; }
    public string? UserId { get; set; }
    public string? Source { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    // [ForeignKey(nameof(IpAddress))]
    // public IpAddressData IpAddressData { get; set; } = null!;
}