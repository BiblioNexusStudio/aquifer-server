namespace Aquifer.API.Modules.Resources;

public class ResourceContentUrlJsonSchema
{
    public string Url { get; set; } = null!;
}

public class ResourceContentAudioJsonSchema
{
    public ResourceContentUrlJsonSchema Webm { get; set; } = null!;
    public ResourceContentUrlJsonSchema Mp3 { get; set; } = null!;
}