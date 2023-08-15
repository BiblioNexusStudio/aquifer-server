namespace Aquifer.API.Modules.Resources;

public class ResourceContentResponse
{
    public int LanguageId { get; set; }
    public string DisplayName { get; set; } = null!;
    public string? Summary { get; set; }
    public object? Content { get; set; }
    public int ContentSizeKb { get; set; }
    public ResourceContentResponseParent Parent { get; set; } = null!;
}

public class ResourceContentResponseParent
{
    public int Type { get; set; }
    public int MediaType { get; set; }
    public string EnglishLabel { get; set; } = null!;
    public string? Tag { get; set; }
}