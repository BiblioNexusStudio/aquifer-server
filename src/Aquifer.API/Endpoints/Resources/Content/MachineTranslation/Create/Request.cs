using Aquifer.Data.Enums;

namespace Aquifer.API.Endpoints.Resources.Content.MachineTranslation.Create;

public class Request
{
    public int ResourceContentVersionId { get; set; }
    public string Content { get; set; } = null!;
    public MachineTranslationSourceId SourceId { get; set; }
}