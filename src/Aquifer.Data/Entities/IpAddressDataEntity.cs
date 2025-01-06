using System.ComponentModel.DataAnnotations;

namespace Aquifer.Data.Entities;

public class IpAddressDataEntity
{
    [Key, MaxLength(64)]
    public string IpAddress { get; set; } = null!;

    [MaxLength(256)]
    public string? City { get; set; }

    [MaxLength(256)]
    public string? Region { get; set; }

    [MaxLength(256)]
    public string? Country { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;
}