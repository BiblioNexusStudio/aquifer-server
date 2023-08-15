namespace Aquifer.API.Modules.Resources;

public class AvailableContentResponse
{
    public List<AvailableContentResponseBible> Bibles { get; set; } = new();
    public List<AvailableContentResponseResourceContent> Resources { get; set; } = new();
}

public class AvailableContentResponseBible
{
    public int LanguageId { get; set; }
    public string Name { get; set; } = null!;

    public IEnumerable<AvailableContentResponseBibleContent> Contents { get; set; } =
        new List<AvailableContentResponseBibleContent>();
}

public class AvailableContentResponseBibleContent
{
    public int BookId { get; set; }
    public string DisplayName { get; set; } = null!;
    public string TextUrl { get; set; } = null!;
    public object? AudioUrls { get; set; }
    public int TextSizeKb { get; set; }
    public int AudioSizeKb { get; set; }
}

public class AvailableContentResponseResourceParent
{
    public int Type { get; set; }
    public int MediaType { get; set; }
    public string EnglishLabel { get; set; } = null!;
    public string? Tag { get; set; }
}

public class AvailableContentResponseResourceContent
{
    public int LanguageId { get; set; }
    public string DisplayName { get; set; } = null!;
    public string? Summary { get; set; }
    public object? Content { get; set; }
    public int ContentSizeKb { get; set; }
    public AvailableContentResponseResourceParent Parent { get; set; } = null!;
}