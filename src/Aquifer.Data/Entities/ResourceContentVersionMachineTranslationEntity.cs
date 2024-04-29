using Aquifer.Data.Enums;

namespace Aquifer.Data.Entities;

public class ResourceContentVersionMachineTranslationEntity
{
    public MachineTranslationSourceId? MachineTranslationSourceId { get; set; }
    public string? MachineTranslationContent { get; set; } // JSON
    public int? MachineTranslationUserId { get; set; }
    public DateTime? MachineTranslationCreated { get; set; }
    public byte? MachineTranslationUserRating { get; set; }
}