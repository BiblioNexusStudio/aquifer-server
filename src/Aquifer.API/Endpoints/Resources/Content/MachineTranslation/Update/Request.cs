namespace Aquifer.API.Endpoints.Resources.Content.MachineTranslation.Update;

public class Request
{
    public int ResourceContentVersionId { get; set; }
    public byte? UserRating { get; set; }
    public bool? ImproveClarity { get; set; }
    public bool? ImproveConsistency { get; set; }
    public bool? ImproveTone { get; set; }
}