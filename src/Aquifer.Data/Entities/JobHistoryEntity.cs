using System.ComponentModel.DataAnnotations;
using Aquifer.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[Index(nameof(JobId), IsUnique = true)]
public class JobHistoryEntity
{
    public int Id { get; set; }

    public JobId JobId { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime LastProcessed { get; set; } = DateTime.UtcNow;
}