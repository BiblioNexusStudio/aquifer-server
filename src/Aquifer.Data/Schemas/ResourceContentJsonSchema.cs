namespace Aquifer.Data.Schemas;

public class ResourceContentUrlJsonSchema
{
    public string? Url { get; set; }
}

public class ResourceContentAudioJsonSchema
{
    public ResourceContentAudioUrlJsonSchema? Webm { get; set; }
    public ResourceContentAudioUrlJsonSchema? Mp3 { get; set; }
}

public class ResourceContentAudioUrlJsonSchema
{
    public string? Url { get; set; }
    public int? Size { get; set; }
    public IReadOnlyList<ResourceContentAudioStepJsonSchema>? Steps { get; set; }
}

public class ResourceContentAudioStepJsonSchema
{
    public int? StepNumber { get; set; }
    public string? File { get; set; }
    public string? Url { get; set; }
}

public class ResourceContentVideoJsonSchema
{
    public string? ThumbnailUrl { get; set; }
}