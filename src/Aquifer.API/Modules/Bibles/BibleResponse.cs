namespace Aquifer.API.Modules.Bibles;

public class BibleBookResponse
{
    public int LanguageId { get; set; }
    public string Name { get; set; } = null!;

    public IEnumerable<BibleBookResponseContent> Contents { get; set; } =
        new List<BibleBookResponseContent>();
}

public class BibleBookResponseContent
{
    public int BookId { get; set; }
    public string DisplayName { get; set; } = null!;
    public string TextUrl { get; set; } = null!;
    public object? AudioUrls { get; set; }
    public int TextSize { get; set; }
    public int AudioSize { get; set; }
}
