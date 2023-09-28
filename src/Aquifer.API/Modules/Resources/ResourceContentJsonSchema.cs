namespace Aquifer.API.Modules.Resources;

public class ResourceContentUrlJsonSchema
{
    public string Url { get; set; } = null!;
}

public class ResourceContentAudioJsonSchema
{
    public ResourceContentAudioTypeJsonSchema Webm { get; set; } = null!;
    public ResourceContentAudioTypeJsonSchema Mp3 { get; set; } = null!;
}

public class ResourceContentAudioTypeJsonSchema
{
    public string Url { get; set; } = null!;
}