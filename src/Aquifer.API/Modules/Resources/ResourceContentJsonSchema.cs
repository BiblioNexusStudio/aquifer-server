namespace Aquifer.API.Modules.Resources;

public class ResourceContentUrlJsonSchema
{
    public string? Url { get; set; }
}

public class ResourceContentAudioJsonSchema
{
    public ResourceContentUrlJsonSchema? Webm { get; set; }
    public ResourceContentUrlJsonSchema? Mp3 { get; set; }
}

public class ResourceContentVideoJsonSchema
{
    public string? ThumbnailUrl { get; set; }
}