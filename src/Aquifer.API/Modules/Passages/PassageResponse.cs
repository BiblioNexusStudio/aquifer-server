namespace Aquifer.API.Modules.Passages;

public class PassageResponse
{
    public int StartVerseId { get; set; }
    public int EndVerseId { get; set; }
    public List<PassageResourceResponse> Resources { get; set; } = new();
}

public class PassageResourceResponse
{
    public string DisplayName { get; set; } = null!;
    public object? Content { get; set; } = null!;
}